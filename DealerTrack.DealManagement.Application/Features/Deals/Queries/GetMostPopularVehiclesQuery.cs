using DealerTrack.DealManagement.Model.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace DealerTrack.DealManagement.Application.Features.Deals.Queries
{
    public class GetMostPopularVehiclesQuery : IRequest<List<VehicleDTO>>
    {

    }
}
