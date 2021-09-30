using DealerTrack.DealManagement.Application.Features.Deals.Commands;
using DealerTrack.DealManagement.Application.Features.Deals.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DealerTrack.DealManagement.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DealController : ControllerBase
    {
        private readonly IMediator _mediator;
        public DealController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet(Name = "GetAllDeal")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<DealListVm>>> GetAllDeals()
        {
            var dtos = await _mediator.Send(new GetDealsListQuery());
            return Ok(dtos);
        }

        [HttpGet(Name = "GetMostPopularVehicles")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<VehicleDTO>>> GetMostPopularVehicles()
        {
            var dtos = await _mediator.Send(new GetMostPopularVehiclesQuery());
            return Ok(dtos);
        }

        [HttpPost(Name = "AddDeal")]
        public async Task<ActionResult<CreateDealCommandResponse>> Create([FromForm] CreateDealCommand createDealCommand)
        {
            var response = await _mediator.Send(createDealCommand);
            return Ok(response);
        }
    }
}
