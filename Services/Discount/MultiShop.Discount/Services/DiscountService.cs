using Dapper;
using MultiShop.Discount.Context;
using MultiShop.Discount.Dtos;

namespace MultiShop.Discount.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly DapperContext _context;

        public DiscountService(DapperContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateCouponAsync(CreateDiscountCouponDto createCouponDto)
        {
            try
            {
                string query = "insert into Coupons (Code, Rate, IsActive, ValidDate) values (@code, @rate, @isActive, @validDate)";
                var parameters = new DynamicParameters();
                parameters.Add("@code", createCouponDto.Code);
                parameters.Add("@rate", createCouponDto.Rate);
                parameters.Add("@isActive", createCouponDto.IsActive);
                parameters.Add("@validDate", createCouponDto.ValidDate);

                using (var connection = _context.CreateConnection())
                {
                    var result = await connection.ExecuteAsync(query, parameters);
                    return result > 0;
                }
            }
            catch (Exception ex)
            { 
                return false;
            }
        }



        public async Task DeleteCouponAsync(int id)
        {
            string query = "Delete From Coupons Where CouponId=@couponId ";
            var parameters = new DynamicParameters();
            parameters.Add("@couponId", id);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }

        }

        public async Task<List<ResultDiscountCouponDto>> GetAllCouponAsync()
        {
            string query = " Select * From Coupons ";
           
            using (var connection = _context.CreateConnection())
            {
                var values=await connection.QueryAsync<ResultDiscountCouponDto>(query);
                return values.ToList();
            }

        }

        public async Task<GetByIdDiscountCouponDto> GetByIdCouponAsync(int id)
        {
            string query = " Select * From Coupons Where CouponId=@couponId";
            var parameters = new DynamicParameters();
            parameters.Add("@couponId", id);

            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryFirstOrDefaultAsync<GetByIdDiscountCouponDto>(query,parameters);
                return values;
            }

        }

        public async Task UpdateCouponAsync(UpdateDiscountCouponDto updateCouponDto)
        {
            string query = "UPDATE Coupons SET Code=@code, Rate=@rate, IsActive=@isActive, ValidDate=@validDate WHERE CouponId=@couponId";
            var parameters = new DynamicParameters();
            parameters.Add("@code", updateCouponDto.Code);
            parameters.Add("@rate", updateCouponDto.Rate);
            parameters.Add("@isActive", updateCouponDto.IsActive);
            parameters.Add("@validDate", updateCouponDto.ValidDate);
            parameters.Add("@couponId", updateCouponDto.CouponId);

            using (var connection = _context.CreateConnection())
            {
                // Güncelleme işlemi gerçekleşip gerçekleşmediğini kontrol et
                var rowsAffected = await connection.ExecuteAsync(query, parameters);
                if (rowsAffected == 0)
                {
                    throw new Exception("Güncelleme yapılacak kupon bulunamadı.");
                }
            }
        }

    }
}
