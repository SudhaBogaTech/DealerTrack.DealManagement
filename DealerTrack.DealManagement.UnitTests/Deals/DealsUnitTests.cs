using AutoMapper;
using Moq;
using System;
using Xunit;
using DealerTrack.DealManagement.Application.Contracts.Persistence;
using DealerTrack.DealManagement.UnitTests.Mocks;
using DealerTrack.DealManagement.Model.Entities;
using DealerTrack.DealManagement.Application.Profiles;
using DealerTrack.DealManagement.Application.Features.Deals.Commands;
using Microsoft.Extensions.Logging;
using System.IO;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading;
using Microsoft.AspNetCore.Http.Internal;
using Shouldly;
using System.Threading.Tasks;
using System.Text;
using System.Linq;

namespace DealerTrack.DealManagement.UnitTests
{
    public class DealsUnitTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IDealRepository> _mockDealRepository;
        private readonly Mock<ILogger<CreateDealCommandHandler>> _mockLogger;
        public DealsUnitTests()
        {
            _mockDealRepository = RepositoryMocks.GetDealRepository();
            _mockLogger = new Mock<ILogger<CreateDealCommandHandler>>();
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _mapper = configurationProvider.CreateMapper();
        }

        [Fact]
        public async Task VerifyDealNumberIsAddedToDeals()
        {
            // precondition
            var allDeals = await _mockDealRepository.Object.GetAllDeals();
            allDeals.Count.ShouldBe(1, "Deals repository already has one deal");

            // arrange
            var handler = new CreateDealCommandHandler(_mapper, _mockDealRepository.Object, _mockLogger.Object);
            var csvLines = new StringBuilder();
            csvLines.AppendLine("DealNumber,CustomerName,DealershipName,Vehicle,Price,Date") ;
            csvLines.AppendLine("3333,Milli Fulton, Sun of Saskatoon,,429,");
            var fileName = "test.csv";
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(csvLines.ToString());
            writer.Flush();
            stream.Position = 0;

            //create FormFile with desired data
            IFormFile file = new FormFile(stream, 0, stream.Length, "id_from_form", fileName);

            // act
            await handler.Handle(new CreateDealCommand() { files  = new List<IFormFile> { file} }, CancellationToken.None);

            // assert
            allDeals = await _mockDealRepository.Object.GetAllDeals();
            allDeals.Count.ShouldBe(2, "one more deal is just added");           
        }
    }
}

