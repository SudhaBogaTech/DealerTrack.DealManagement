using DealerTrack.DealManagement.Application.Features.Deals.Commands;
using DealerTrack.DealManagement.Model.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DealerTrack.DealManagement.Application.Contracts.Infrastructure
{
    public interface IFileImporter
    {
        Task<List<CreateDealDTO>> ImportFile(IFormFile file);
    }
}
