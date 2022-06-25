using AutoMapper;
using OnlineStore.BLL.DTO;
using OnlineStore.WEB.Models;

namespace OnlineStore.WEB.Infrastructure
{
    public class MappingProfileVM : Profile
    {
        public MappingProfileVM()
        {
            CreateMap<AddressDTO, AddressVM>();
            CreateMap<BasketDTO, BasketVM>();
            CreateMap<BrandDTO, BrandVM>();
            CreateMap<CategoryDTO, CategoryVM>();
            CreateMap<MonitorDatabaseDTO, MonitorDatabaseVM>();
            CreateMap<PermissionDTO, PermissionVM>();
            CreateMap<PictureDTO, PictureVM>();
            CreateMap<ProductDTO, ProductVM>();
            CreateMap<ProductCharacteristicValueDTO, ProductCharacteristicValueVM>();
            CreateMap<СharacteristicDTO, СharacteristicVM>();
            CreateMap<CharacteristicValueDTO, CharacteristicValueVM>();
            CreateMap<PurchaseHistoryDTO, PurchaseHistoryVM>();
            CreateMap<RoleDTO, RoleVM>();
            CreateMap<RolesPermissionDTO, RolesPermissionVM>();
            CreateMap<StoreDTO, StoreVM>();
            CreateMap<StoresProductDTO, StoresProductVM>();
            CreateMap<StoresUserDTO, StoresUserVM>();
            CreateMap<UserDTO, UserVM>();
        }
    }
}
