using DealerTrack.DealManagement.Application.Contracts.Persistence;
using DealerTrack.DealManagement.Application.Features.Deals.Queries;
using DealerTrack.DealManagement.Model.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace DealerTrack.DealManagement.UnitTests.Mocks
{
    public class RepositoryMocks
    {


        public static Mock<IDealRepository> GetDealRepository()
        {
            var dealsList = new List<DealListVm>
            {
                new DealListVm {DealershipName = "test inc",
                           Vehicle = "Honda Civic 2017",
                           Price = 300,
                           DealNumber = 12345,
                           CustomerName = "test user",
                           Date = DateTime.Today
                         }
            };
           
            var mockDealRepository = new Mock<IDealRepository>();
            mockDealRepository.Setup(repo => repo.GetAllDeals()).ReturnsAsync(dealsList);

            Action<Deal> action = (deal) =>
            {
                dealsList.Add(new DealListVm() { DealNumber = deal.DealNumber });
            };

            //mockDealRepository.Setup(repo => repo.AddNewDeal(It.IsAny<Deal>())).ReturnsAsync(
            //   (Deal deal) =>
            //   {
            //       dealsList.Add(new DealListVm() {DealNumber = deal.DealNumber });
            //       return deal;
            //   });

            mockDealRepository.Setup(repo => repo.AddNewDeal(It.IsAny<Deal>())).Callback(action);

            return mockDealRepository;
        }
    }
}
