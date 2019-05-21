using System.Collections.Generic;
using System.Threading.Tasks;
using OrderKingCoreDemo.DAL.DataObjects;
using OrderKingCoreDemo.DAL.DataServices;
using OrderKingCoreDemo.Helpers;

namespace OrderKingCoreDemo.BL.ViewModels.Hotel
{
    public class HotelInfoViewModel: BaseViewModel {

	    string _hotelId;

	    public HotelObject Hotel 
	    {
		    get => Get<HotelObject>();
		    set => Set(value);
	    }

	    public override void OnSetNavigationParams(Dictionary<string, object> navigationParams) {
		    base.OnSetNavigationParams(navigationParams);
		    navigationParams.TryGetValue("hotelId", out _hotelId);
	    }

	    protected override async Task LoadDataAsync() {
		    if (!IsConnected) {
			    State = PageState.NoInternet;
			    return;
		    }

		    var result = await DataServices.Hotel.GetHotelInfo(_hotelId, CancellationToken);
		    Hotel = result.Data;
	    }
    }
}
