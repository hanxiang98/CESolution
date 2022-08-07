using CE.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CE.Service.Interface
{
    public interface IOrderService
    {
        public Task<List<OrderModel>> GetInProgressOrdersAsync();
        public Task<List<ProductSoldModel>> GetTopFiveProductsSold(List<OrderModel> orderList);
        public Task<dynamic> GetProductByMerchantProductNo(string merchantProductNo);
        public Task UpdateProductStock(string merchantProductNo);
    }
}
