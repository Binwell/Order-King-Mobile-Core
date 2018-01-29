/* MIT License

Copyright (c) 2018 Binwell https://binwell.com

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE. */

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using OrderKingCoreDemo.BL.ViewModels;
using OrderKingCoreDemo.Helpers;
using OrderKingCoreDemo.UI.Pages;
using Xamarin.Forms;

namespace OrderKingCoreDemo.UI {
	public sealed class NavigationService {
		static readonly Lazy<NavigationService> LazyInstance = new Lazy<NavigationService>(() => new NavigationService(), true);

		readonly ConcurrentStack<INavigation> _navigations = new ConcurrentStack<INavigation>();
		readonly Dictionary<string, Type> _pageTypes;
		readonly Dictionary<string, Type> _viewModelTypes;
		Application _app;

		// ignore all requests to avoid jittering while _isBusy == true
		volatile bool _isBusy;

		NavigationService() {
			_pageTypes = GetAssemblyPageTypes();
			_viewModelTypes = GetAssemblyViewModelTypes();
			MessagingCenter.Subscribe<MessageBus, NavigationPushInfo>(this, Consts.NavigationPushMessage, NavigationPushCallback);
			MessagingCenter.Subscribe<MessageBus, NavigationPopInfo>(this, Consts.NavigationPopMessage, NavigationPopCallback);
		}

		public static NavigationService Instance => LazyInstance.Value;

		public static void Init(Application app) {
			Instance.Initialize(app);
		}

		void Initialize(Application app) {
			_app = app;
		}

		public void SetMainMasterDetailPage(object masterName,
			object detailName,
			Dictionary<string, object> navParams = null,
			bool invokeOnMainThread = false) {
			if (_isBusy) return;

			if (string.IsNullOrEmpty(masterName?.ToString())) throw new ArgumentNullException(nameof(masterName));
			if (string.IsNullOrEmpty(detailName?.ToString())) throw new ArgumentNullException(nameof(detailName));

			Action setMainPage = () => {
				_isBusy = true;

				var masterDetailPage = new MasterDetailPage {
					Master = GetInitializedPage(masterName.ToString(), navParams: navParams,
						withBackButton: false, toTitle: masterName.ToString()),
					Detail = GetInitializedPage(detailName.ToString(),
						NavigationMode.Normal, navParams, true, false, false)
				};
				_app.MainPage = masterDetailPage;
				_navigations?.Clear();
				_navigations?.Push(masterDetailPage.Detail.Navigation);
				_isBusy = false;
			};
			if (invokeOnMainThread)
				Device.BeginInvokeOnMainThread(setMainPage);
			else setMainPage.Invoke();
		}

		public void SetMainPage(object page, Dictionary<string, object> navParams = null) {
			if (_isBusy) return;
			if (string.IsNullOrEmpty(page?.ToString())) throw new ArgumentNullException(nameof(page));

			_isBusy = true;
			var navigationPage = new NavigationPage(GetInitializedPage(page.ToString(), withBackButton: false, navParams: navParams));
			_app.MainPage = navigationPage;

			DisposePages(_navigations.ToArray());

			_navigations.Clear();
			_navigations.Push(navigationPage.Navigation);
			_isBusy = false;
		}

		void NavigationPushCallback(MessageBus bus, NavigationPushInfo navigationPushInfo) {
			if (_isBusy) return;

			if (navigationPushInfo == null) throw new ArgumentNullException(nameof(navigationPushInfo));

			if (string.IsNullOrEmpty(navigationPushInfo.To)) throw new FieldAccessException(@"'To' page value should be set");
			if (_app == null) throw new FieldAccessException(@"App property not set. Use Init() before navigation");
			if (_navigations.Count == 0) throw new FieldAccessException(@"Navigation is null. Add NavigationPage first");

			Device.BeginInvokeOnMainThread(async () => {
				_isBusy = true;

				try {
					await Push(navigationPushInfo);
				}
				catch (Exception e) {
					//LogService.WriteError(this, nameof(NavigationPushCallback), e);
				}

				_isBusy = false;
			});
		}

		void NavigationPopCallback(MessageBus bus, NavigationPopInfo navigationPopInfo) {
			if (navigationPopInfo == null) throw new ArgumentNullException(nameof(navigationPopInfo));

			if (!navigationPopInfo.Force && _isBusy) return;

			if (_app == null) throw new FieldAccessException(@"Application property not set. Use Init() before navigation");
			if (_navigations.Count == 0) throw new FieldAccessException(@"Navigation is null. Add NavigationPage first");

			Device.BeginInvokeOnMainThread(async () => {
				_isBusy = true;

				try {
					await Pop(navigationPopInfo);
				}
				catch (Exception) {
					// ignore
				}

				_isBusy = false;
			});
		}

		async Task SetRootDetailPage(MasterDetailPage masterDetailPage, Page newPage, bool withAnimation) {
			if (withAnimation) {
				newPage.Opacity = 0;
				if (masterDetailPage.Detail != null)
					await masterDetailPage.Detail.FadeTo(0);
			}

			var oldNavigation = masterDetailPage.Detail?.Navigation;
			masterDetailPage.Detail = newPage;
			DisposePages(oldNavigation);

			if (withAnimation)
				await newPage.FadeTo(1);
		}


		Task HandleCustomPushNavigation(MasterDetailPage masterDetailPage, NavigationPushInfo pushInfo) {
			// TODO: Implement your own navigation stack manipulation using pushInfo
			return Task.FromResult(0);
		}

		Task HandleCustomPopNavigation(NavigationPopInfo popInfo) {
			// TODO: Implement your own navigation stack manipulation using popInfo
			return Task.FromResult(0);
		}

		void DisposePages(params INavigation[] navigations) {
			if (navigations != null && navigations.Length > 0)
				foreach (var navigation in navigations) {
					DisposePages(navigation.NavigationStack?.ToArray());
					DisposePages(navigation.ModalStack?.ToArray());
				}
		}

		void DisposePages(params Page[] pages) {
			if (pages != null && pages.Length > 0)
				foreach (var page in pages)
					if (page is IDisposable disposablePage)
						disposablePage.Dispose();
		}

		#region NavigationService internals

		public async Task Push(NavigationPushInfo pushInfo) {
			var newPage = GetInitializedPage(pushInfo);
			INavigation navigation;

			var masterDetailPage = _app.MainPage as MasterDetailPage;
			if (masterDetailPage != null && masterDetailPage.IsPresented) {
				masterDetailPage.IsPresented = false;
				await Task.Delay(300);
			}

			switch (pushInfo.Mode) {
				case NavigationMode.Normal:
					if (_navigations.TryPeek(out navigation))
						await navigation.PushAsync(newPage, pushInfo.WithAnimation);
					break;

				case NavigationMode.Modal:
					if (_navigations.TryPeek(out navigation)) {
						await navigation.PushModalAsync(newPage, pushInfo.WithAnimation);

						if (newPage.Navigation != null && !_navigations.Contains(newPage.Navigation))
							_navigations.Push(newPage.Navigation);
					}
					break;

				case NavigationMode.RootPage:
					if (masterDetailPage != null)
						await SetRootDetailPage(masterDetailPage, newPage, pushInfo.WithAnimation);
					else
						_app.MainPage = newPage;

					DisposePages(_navigations.ToArray());

					_navigations.Clear();
					_navigations.Push(newPage.Navigation);
					break;

				case NavigationMode.Custom:
					await HandleCustomPushNavigation(masterDetailPage, pushInfo);
					break;

				default:
					throw new NotImplementedException();
			}
		}

		async Task Pop(NavigationPopInfo popInfo) {
			INavigation navigation;
			switch (popInfo.Mode) {
				case NavigationMode.Normal:
					if (_navigations.TryPeek(out navigation))
						await NormalPop(popInfo.WithAnimation, navigation);
					break;

				case NavigationMode.Modal:
					if (_navigations.TryPop(out navigation) && navigation.ModalStack.Count > 0)
						await ModalPop(popInfo.WithAnimation, navigation);
					break;

				case NavigationMode.RootPage:
					if (_navigations.TryPeek(out navigation)) {
						var pagesToDispose = navigation.NavigationStack?.Count > 0
							? navigation.NavigationStack.Where(p => p != navigation.NavigationStack[0]).ToArray()
							: null;
						await navigation.PopToRootAsync(popInfo.WithAnimation);
						DisposePages(pagesToDispose);
					}
					break;

				case NavigationMode.Custom:
					await HandleCustomPopNavigation(popInfo);
					break;

				default:
					throw new NotImplementedException();
			}
		}

		async Task ModalPop(bool withAnimation, INavigation navigation) {
			DisposePages(await navigation.PopModalAsync(withAnimation));
		}

		async Task NormalPop(bool withAnimation, INavigation navigation) {
			if (navigation.ModalStack.Count > 0 && navigation.NavigationStack.Count == 1) {
				if (_navigations.TryPop(out var tempNavigation) && tempNavigation.ModalStack.Count > 0)
					await ModalPop(withAnimation, tempNavigation);
			}
			else
				DisposePages(await navigation.PopAsync(withAnimation));
		}

		static string GetTypeBaseName(MemberInfo info) {
			if (info == null) throw new ArgumentNullException(nameof(info));
			return info.Name.Replace(@"Page", "").Replace(@"ViewModel", "");
		}

		static Dictionary<string, Type> GetAssemblyPageTypes() {
			return typeof(BasePage).GetTypeInfo().Assembly.DefinedTypes
				.Where(ti => ti.IsClass && !ti.IsAbstract && ti.Name.Contains(@"Page") && ti.BaseType.Name.Contains(nameof(BasePage)))
				.ToDictionary(GetTypeBaseName, ti => ti.AsType());
		}

		static Dictionary<string, Type> GetAssemblyViewModelTypes() {
			return typeof(BaseViewModel).GetTypeInfo().Assembly.DefinedTypes
				.Where(ti => ti.IsClass && !ti.IsAbstract && ti.Name.Contains(@"ViewModel") &&
				             ti.BaseType.Name.Contains(@"ViewModel"))
				.ToDictionary(GetTypeBaseName, ti => ti.AsType());
		}

		Page GetInitializedPage(string toName,
			NavigationMode mode = NavigationMode.Normal,
			Dictionary<string, object> navParams = null,
			bool newNavigationStack = false,
			bool withAnimation = true,
			bool withBackButton = true,
			string toTitle = null) {
			var page = GetPage(toName);
			var viewModel = GetViewModel(toName);
			viewModel.SetNavigationParams(navParams);
			page.SetViewModel(viewModel);

			if (!string.IsNullOrEmpty(toTitle)) page.Title = toTitle;

			return newNavigationStack
				? new NavigationPage(page)
				: (Page) page;
		}


		Page GetInitializedPage(NavigationPushInfo navigationPushInfo) {
			return GetInitializedPage(navigationPushInfo.To, navigationPushInfo.Mode, navigationPushInfo.NavigationParams,
				navigationPushInfo.NewNavigationStack, navigationPushInfo.WithAnimation,
				navigationPushInfo.WithBackButton, navigationPushInfo.ToTitle);
		}

		BasePage GetPage(string pageName) {
			if (!_pageTypes.ContainsKey(pageName)) throw new KeyNotFoundException($@"Page for {pageName} not found");
			BasePage page;
			try {
				var pageType = _pageTypes[pageName];
				var pageObject = Activator.CreateInstance(pageType);
				page = pageObject as BasePage;
			}
			catch (Exception e) {
				throw new TypeLoadException($@"Unable create instance for {pageName}Page", e);
			}

			return page;
		}

		BaseViewModel GetViewModel(string pageName) {
			if (!_viewModelTypes.ContainsKey(pageName)) throw new KeyNotFoundException($@"ViewModel for {pageName} not found");
			BaseViewModel viewModel;
			try {
				viewModel = Activator.CreateInstance(_viewModelTypes[pageName]) as BaseViewModel;
			}
			catch (Exception e) {
				throw new TypeLoadException($@"Unable create instance for {pageName}ViewModel", e);
			}

			return viewModel;
		}

		#endregion
	}

	public class NavigationPushInfo {
		public string From { get; set; }
		public string To { get; set; }
		public string ToTitle { get; set; }
		public Dictionary<string, object> NavigationParams { get; set; }
		public NavigationMode Mode { get; set; } = NavigationMode.Normal;
		public bool WithAnimation { get; set; } = true;
		public bool WithBackButton { get; set; } = true;
		public bool NewNavigationStack { get; set; }
	}

	public class NavigationPopInfo {
		public NavigationMode Mode { get; set; } = NavigationMode.Normal;
		public bool WithAnimation { get; set; } = true;
		public bool Force { get; set; }
	}
}