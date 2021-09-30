using DealerTrack.DealManagement.Application.Features.Dealerships.Commands;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace DealerTrack.DealManagement.Application.Features.Deals.Commands
{
    public class CreateDealCommand: IRequest<CreateDealCommandResponse>
    {
        public List<IFormFile> files { get; set; }
    }
}
