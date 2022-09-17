using AutoMapper;
using RestaurantManage.Models;
using RestaurantManage.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantManage.Mapper
{
    public class AutoMapper:Profile
    {
        public AutoMapper()
        {
            CreateMap<Customer, CustomerModelView>().ReverseMap();
            CreateMap<Restaurantmenu,RestaurantmenuModelView>().ReverseMap();
            CreateMap<Restaurant,RestaurantModelView>().ReverseMap();
        }
    }
}
