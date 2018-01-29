//  Based on https://github.com/paulcbetts/refit/issues/258#issuecomment-243394076
//  Special thanks to Ahmed Aderopo from Binwell Ltd.

//  Distributed under Apache 2.0 Licence: http://www.apache.org/licenses/LICENSE-2.0

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OrderKingCoreDemo.DAL.Helpers
{
	internal class HttpLoggingHandler : DelegatingHandler
	{
		static int _requestId;

		readonly string[] _types = {@"html", @"text", @"xml", @"json", @"txt", @"x-www-form-urlencoded"};

		public HttpLoggingHandler(HttpMessageHandler innerHandler)
			: base(innerHandler)
		{
		}

		public HttpLoggingHandler()
		{
		}

		public static HttpMessageHandler GetHandler(HttpMessageHandler handler, bool useLogging = false)
		{
#if DEBUG
			return useLogging ? new HttpLoggingHandler(handler) : handler;
#else
			return handler;
#endif
		}

		static int GetRequestId()
		{
			return ++_requestId;
		}

		protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			var id = GetRequestId();
			var msg = $@"[{id} - Request]";

			var outputBuilder = new StringBuilder();
			await AppendRequestInfo(outputBuilder, msg, request);

			var start = DateTime.Now;
			HttpResponseMessage response;
			try
			{
				response = await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
			}
			catch (Exception e) {
				var endCatched = DateTime.Now;
				AppendRequestDuration(outputBuilder, msg, endCatched, start);
				outputBuilder.AppendLine($@"{msg}=========Request exception=========");
				outputBuilder.AppendLine($@"{msg} {e}");
				Console.WriteLine(outputBuilder.ToString());
				throw;
			}
			var end = DateTime.Now;

			AppendRequestDuration(outputBuilder, msg, end, start);

			msg = $@"[{id} - Response]";
			await AppendResponseInfo(outputBuilder, msg, response, request);

			Console.WriteLine(outputBuilder.ToString());

			return response;
		}

		async Task AppendResponseInfo(StringBuilder outputBuilder, string msg, HttpResponseMessage response, HttpRequestMessage req)
		{
			outputBuilder.AppendLine($@"{msg}=========Start=========");

			var resp = response;
			outputBuilder.AppendLine($@"{msg} {req.RequestUri.Scheme.ToUpper()}/{resp.Version} {(int) resp.StatusCode} {resp.ReasonPhrase}");

			foreach (var header in resp.Headers)
				outputBuilder.AppendLine($@"{msg} {header.Key}: {string.Join(@", ", header.Value)}");

			if (resp.Content != null)
			{
				foreach (var header in resp.Content.Headers)
					outputBuilder.AppendLine($@"{msg} {header.Key}: {string.Join(@", ", header.Value)}");

				if (resp.Content is StringContent || IsTextBasedContentType(resp.Headers) || IsTextBasedContentType(resp.Content.Headers))
				{
					var start = DateTime.Now;

					var result = await resp.Content.ReadAsStringAsync();
					result = result?.Substring(0, Math.Min(2 * 1024, result.Length)); // Reduce length of visible content

					var end = DateTime.Now;

					outputBuilder.AppendLine($@"{msg} Content:");
					outputBuilder.AppendLine($@"{msg} {result}");
					outputBuilder.AppendLine($@"{msg} Duration: {end - start}");
				}
			}

			outputBuilder.AppendLine($@"{msg}==========End==========");
		}

		static void AppendRequestDuration(StringBuilder outputBuilder, string msg, DateTime end, DateTime start)
		{
			outputBuilder.AppendLine($@"{msg} Duration: {end - start}");
			outputBuilder.AppendLine($@"{msg}==========End==========");
			outputBuilder.AppendLine();
		}

		async Task AppendRequestInfo(StringBuilder outputBuilder, string msg, HttpRequestMessage req)
		{
			outputBuilder.AppendLine();
			outputBuilder.AppendLine($@"{msg}========Start==========");
			outputBuilder.AppendLine($@"{msg} {req.Method} {req.RequestUri.PathAndQuery} {req.RequestUri.Scheme}/{req.Version}");
			outputBuilder.AppendLine($@"{msg} Host: {req.RequestUri.Scheme}://{req.RequestUri.Host}");
			outputBuilder.AppendLine($@"{msg} Uri: {req.RequestUri}");

			foreach (var header in req.Headers)
				outputBuilder.AppendLine($@"{msg} {header.Key}: {string.Join(@", ", header.Value)}");

			if (req.Content != null)
			{
				foreach (var header in req.Content.Headers)
					outputBuilder.AppendLine($@"{msg} {header.Key}: {string.Join(@", ", header.Value)}");

				if (req.Content is StringContent || IsTextBasedContentType(req.Headers) || IsTextBasedContentType(req.Content.Headers))
				{
					var result = await req.Content.ReadAsStringAsync();
					result = result?.Substring(0, Math.Min(2 * 1024, result.Length)); // Reduce length of visible content
					if (!string.IsNullOrEmpty(result))
					{
						outputBuilder.AppendLine($@"{msg} Content (2 KB max to display):");
						outputBuilder.AppendLine($@"{msg} {WebUtility.UrlDecode(result)}");
					}
				}
			}
		}

		bool IsTextBasedContentType(HttpHeaders headers)
		{
			IEnumerable<string> values;
			if (!headers.TryGetValues(@"Content-Type", out values))
				return false;
			var header = string.Join(@" ", values).ToLowerInvariant();

			return _types.Any(t => header.Contains(t));
		}
	}
}