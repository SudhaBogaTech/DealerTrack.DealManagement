using AutoMapper;
using DealerTrack.DealManagement.Application.Contracts.Infrastructure;
using DealerTrack.DealManagement.Application.Contracts.Persistence;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Runtime.Loader;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DealerTrack.DealManagement.Application.Features.Deals.Commands
{
    public class CreateDealCommandHandler: IRequestHandler<CreateDealCommand, CreateDealCommandResponse>
    {
        private readonly IDealRepository _dealRepository;
        //private readonly IFileReaderFactory _fileReaderFactory;

        private readonly IMapper _mapper;        
        private readonly ILogger<CreateDealCommandHandler> _logger;

        public CreateDealCommandHandler(IMapper mapper, IDealRepository dealRepository,  ILogger<CreateDealCommandHandler> logger)
        {
            _mapper = mapper;
            _dealRepository = dealRepository;
           // _fileReaderFactory = fileReaderFactory;
            _logger = logger;
        }

        public async Task<CreateDealCommandResponse> Handle(CreateDealCommand request, CancellationToken cancellationToken)
        {
            var createDealCommandResponse = new CreateDealCommandResponse();
            
            try
            {
                foreach (var file in request.files)
                {
                    var fileType = Path.GetExtension(file.FileName).ToLower().Remove(0, 1);
                    var assemblies = Directory
                    .GetFiles(System.AppDomain.CurrentDomain.BaseDirectory, "DealerTrack.DealManagement.Infrastructure.dll", SearchOption.TopDirectoryOnly)
                    .Select(AssemblyLoadContext.Default.LoadFromAssemblyPath)
                    .ToList();
                    var configuration = new ContainerConfiguration()
                        .WithAssemblies(assemblies);
                    using (var container = configuration.CreateContainer())
                    {
                        var fileImporter = container.GetExports<IFileImporter>(fileType).FirstOrDefault();
                        if (fileImporter == null)
                            throw new InvalidDataException($"File Type: {fileType} is not supported");
                        else
                        {
                            var dealsList = fileImporter.ImportFile(file);
                            foreach (var deal in dealsList.Result)
                            {
                                var newDeal = deal.Transform();
                                try
                                {
                                    _dealRepository.AddNewDeal(newDeal);
                                    //await _dealRepository.AddAsync(newDeal);
                                }
                                catch(Exception ex)
                                {
                                    _logger.LogError($"Error while creating deal: {ex.Message}");
                                }
                            }
                        }
                    }
                        
                    
                     
                   
                }
                createDealCommandResponse.Success = true;
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error while creating deal: {ex.Message}");
                createDealCommandResponse.Success = false;
            }
            return createDealCommandResponse;
        }
    }
}
