using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using TechnoWorld.Entities;

namespace TechnoWorld.Abstractions
{
    public interface IBrandService
    {
        List<Brand> GetBrands();
        Brand GetBrandById(int brandId);
        List<Product> GetProductsByBrand(int brandId);
    }
}
