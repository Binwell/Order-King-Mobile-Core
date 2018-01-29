using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OrderKingCoreDemo.DAL.Helpers;

namespace OrderKingCoreDemo.DAL.DataServices.Mock
{
	public class BaseMockDataService
	{
		protected bool UseDelay { get; set; } =

#if DEBUG
			false;
#else
			true;
#endif
		static readonly Random Randomizer = new Random(DateTime.UtcNow.Millisecond);

		protected async Task<RequestResult<T>> GetMockData<T>(string fileName) where T : class
		{
			if (UseDelay) await Delay();

			try
			{
				var data = JsonConvert.DeserializeObject<T>(DataTools.GetFileContent(fileName));
				return new RequestResult<T>(data, RequestStatus.Ok);
			}
			catch (Exception e)
			{
				return new RequestResult<T>(default(T), RequestStatus.InternalServerError, e.Message);
			}
		}

		protected async Task<RequestResult<List<T>>> GetMockDataList<T>(string fileName) where T : class
		{
			var result = await GetMockData<ValueList<T>>(fileName).ConfigureAwait(false);
			return result.IsValid
				? new RequestResult<List<T>>(result.Data.Values, result.Status, result.Message)
				: new RequestResult<List<T>>(null, result.Status, result.Message);
		}


		protected async Task<RequestResult<List<T>>> GetMockDataListFromCsv<T>(string fileName) where T : class
		{
			var result = DataTools.ParseCsvWithHeaders<T>(fileName, ',');
			return new RequestResult<List<T>>(result, result != null ? RequestStatus.Ok : RequestStatus.SerializationError);
		}
		
		static Task Delay() {
			return Task.Delay(Randomizer.Next(100, 1000));
		}

		class ValueList<T> {
			public List<T> Values { get; set; }
		}
	}
}