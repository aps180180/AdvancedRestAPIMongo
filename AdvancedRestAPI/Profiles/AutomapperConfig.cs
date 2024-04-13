using AdvancedRestAPI.DTOs;
using AdvancedRestAPI.Models;
using AutoMapper;

namespace AdvancedRestAPI.Profiles
{
    public class AutomapperConfig:Profile
    {
        public AutomapperConfig() 
        {
            CreateMap<User, UserDTO>().ReverseMap();
           
        }
    }
}
