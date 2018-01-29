using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using OrderKingCoreDemo.DAL.DataObjects;

namespace OrderKingCoreDemo.DAL.DataServices
{
    public interface IFitnessDataService
    {
        Task<RequestResult<List<ServiceObject>>> GetAllFitness(string hotelId, CancellationToken cts);
        Task<RequestResult<ServiceObject>> GetFitness(string fitnessId, CancellationToken cts);
        Task<RequestResult> FitnessOrder(string fitnessId, DateTime dateTime, string token, CancellationToken cts);
	    Task<RequestResult<ServiceOrderObject>> GetOrder(string orderId, string token, CancellationToken cts);
    }
}