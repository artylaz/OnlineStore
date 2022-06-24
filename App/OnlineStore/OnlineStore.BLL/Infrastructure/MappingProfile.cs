using AutoMapper;
using OnlineStore.BLL.DTO;
using OnlineStore.DAL.Entities;

namespace OnlineStore.BLL.Infrastructure
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Address, AddressDTO>();
            CreateMap<Basket, BasketDTO>();
            CreateMap<Brand, BrandDTO>();
            CreateMap<Category, CategoryDTO>();
            CreateMap<MonitorDatabase, MonitorDatabaseDTO>();
            CreateMap<Permission, PermissionDTO>();
            CreateMap<Picture, PictureDTO>();
            CreateMap<Product, ProductDTO>();
            CreateMap<ProductInfo, ProductInfoDTO>();
            CreateMap<PurchaseHistory, PurchaseHistoryDTO>();
            CreateMap<Role, RoleDTO>();
            CreateMap<RolesPermission, RolesPermissionDTO>();
            CreateMap<Store, StoreDTO>();
            CreateMap<StoresProduct, StoresProductDTO>();
            CreateMap<StoresUser, StoresUserDTO>();
            CreateMap<User, UserDTO>();
        }
    }
}
