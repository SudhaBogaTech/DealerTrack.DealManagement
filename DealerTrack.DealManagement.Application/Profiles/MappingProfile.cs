using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using DealerTrack.DealManagement.Application.Features.Dealerships.Commands;
using DealerTrack.DealManagement.Application.Features.Deals.Commands;
using DealerTrack.DealManagement.Application.Features.Deals.Queries;
using DealerTrack.DealManagement.Application.Features.Vehicles.Commands;
using DealerTrack.DealManagement.Model.Entities;

namespace DealerTrack.DealManagement.Application.Profiles
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<Deal, DealListVm>().ReverseMap();
            CreateMap<Deal, CreateDealCommand>().ReverseMap();
            CreateMap<Dealership, DealershipDTO>().ReverseMap();
            CreateMap<Dealership, VehicleDTO>().ReverseMap();
            CreateMap<Deal, DealListVm>().ReverseMap();
            CreateMap<VehicleStats, VehicleDTO > ().ReverseMap();

            CreateMap<Dealership, CreateDealershipCommand>().ReverseMap();
            CreateMap<Dealership, CreateDealershipDto>().ReverseMap();

            CreateMap<Vehicle, CreateVehicleCommand>().ReverseMap();
            CreateMap<Vehicle, CreateVehicleDto>().ReverseMap();

        }
    }
}
