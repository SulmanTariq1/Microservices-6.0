using System.Runtime.InteropServices;

namespace ECommerece.Api.Products.DB.Profiles
{
    public class ProductProfile:AutoMapper.Profile
    {
        public ProductProfile()
        {
            CreateMap<DB.Product, Models.Product>();
        }
    }
}
