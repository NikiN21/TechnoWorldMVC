﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechnoWorld.Domain;

namespace TechnoWorld.Abstractions
{
    public interface ICategoryService
    {
        List<Category> GetCategories();
        Category GetCategoryById(int categoryId);
        List<Product> GetProductsByCategory(int categoryId);
    }
}
