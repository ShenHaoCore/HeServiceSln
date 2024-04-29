using AutoMapper;
using He.UserService.Model.DTO;
using He.UserService.Model.Entity;

namespace He.UserService.Model.Common
{
    /// <summary>
    /// 
    /// </summary>
    public class AutoMapperProfile : Profile
    {
        /// <summary>
        /// 
        /// </summary>
        public AutoMapperProfile()
        {
            CreateMap<IdcardCreate, t_IdentityCard>();
            //CreateMap<AddressUpdate, t_Address>();
        }
    }
}
