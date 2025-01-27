using AutoMapper;
using EntityLayer.DTOs.CarDTOs;
using EntityLayer.DTOs.UserDtos;
using EntityLayer.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Mapper
{
    public class Automapper : Profile
    {
        public Automapper()
        {
            CreateMap<CarAddModel, Car>().ReverseMap(); ;
            CreateMap<CarViewModel, Car>().ReverseMap(); ;
            CreateMap<UserList, User>().ReverseMap();
        }

    }
}