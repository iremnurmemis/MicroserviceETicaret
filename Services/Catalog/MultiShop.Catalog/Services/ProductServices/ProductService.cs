using AutoMapper;
using MongoDB.Driver;
using MultiShop.Catalog.Dtos.ProductDtos;
using MultiShop.Catalog.Entities;
using MultiShop.Catalog.Settings;
using static MongoDB.Driver.WriteConcern;

namespace MultiShop.Catalog.Services.ProductServices
{
    public class ProductService : IProductService
    {
        private readonly IMapper _mapper;
        private readonly IMongoCollection<Product> _productCollection;
        private readonly IMongoCollection<Category> _categoryCollection;

        public ProductService(IMapper mapper,IDatabaseSettings _databaseSettings)
        {
            var client=new MongoClient(_databaseSettings.ConnectionString);
            var database=client.GetDatabase(_databaseSettings.DatabaseName);    
            _productCollection=database.GetCollection<Product>(_databaseSettings.ProductCollectionName);
            _categoryCollection=database.GetCollection<Category>(_databaseSettings.CategoryCollectionName);
            _mapper=mapper;
        }
        public async Task CreateProductAsync(CreateProductDto createProductDto)
        {
            var value= _mapper.Map<Product>(createProductDto);
            await _productCollection.InsertOneAsync(value);
        }

        public async Task DeleteProductAsync(string id)
        {
            await _productCollection.DeleteOneAsync(x => x.ProductID == id);
        }

        public async Task<List<ResultProductDto>> GetAllProductsAsync()
        {
            var values=await _productCollection.Find(x=>true).ToListAsync();
            return _mapper.Map<List<ResultProductDto>>(values);
        }

        public async Task<GetByIdProductDto> GetByIdProductyAsync(string id)
        {
            var value = await _productCollection.Find(x => x.ProductID==id).FirstOrDefaultAsync();
            return _mapper.Map<GetByIdProductDto>(value);
        }

        public async Task<List<ResultProductswithCategoryDto>> GetProductsWithCategoryAsync()
        {
            var values=await _productCollection.Find(x=>true).ToListAsync();
            foreach(var value in values)
            {
                value.Category = await _categoryCollection.Find(x => x.CategoryID ==value.CategoryId).FirstAsync();
            }

            return _mapper.Map<List<ResultProductswithCategoryDto>>(values);

        }

        public async Task<List<ResultProductswithCategoryDto>> GetProductsWithCategoryByCategoryIdAsync(string categoryId)
        {
           var values=await _productCollection.Find(x=>x.CategoryId==categoryId).ToListAsync();
            foreach (var value in values)
            {
                value.Category=await _categoryCollection.Find<Category>(x=>x.CategoryID==categoryId).FirstOrDefaultAsync();
            }
            return _mapper.Map<List<ResultProductswithCategoryDto>>(values);
        }

        public async Task UpdateProductAsync(UpdateProductDto updateProductDto)
        {
            var value=_mapper.Map<Product>(updateProductDto);
            await _productCollection.FindOneAndReplaceAsync(x => x.ProductID == updateProductDto.ProductID,value);
        }
    }
}
