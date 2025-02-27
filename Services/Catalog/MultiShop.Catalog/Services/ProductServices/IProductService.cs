﻿using MultiShop.Catalog.Dtos.ProductDtos;

namespace MultiShop.Catalog.Services.ProductServices
{
    public interface IProductService
    {
        Task<List<ResultProductDto>> GetAllProductsAsync(); 
        Task<List<ResultProductswithCategoryDto>> GetProductsWithCategoryAsync();
        Task CreateProductAsync(CreateProductDto createProductDto);
        Task UpdateProductAsync(UpdateProductDto updateProductDto);
        Task DeleteProductAsync(string id);
        Task<GetByIdProductDto> GetByIdProductyAsync(string id);
        Task <List<ResultProductswithCategoryDto>> GetProductsWithCategoryByCategoryIdAsync(string categoryId);
    }
}
