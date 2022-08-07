using CE.Service.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.Text;
using CE.Service.Interface;

namespace CE.Service
{
    public class OrderService: IOrderService
    {
        const string api_key = "541b989ef78ccb1bad630ea5b85c6ebff9ca3322";
        private readonly HttpClient httpClient;

        public OrderService(){
            this.httpClient = new HttpClient();
        }


        public async Task<List<OrderModel>> GetInProgressOrdersAsync()
        {
            dynamic obj;

            // Fetch orders from the api call
            using (var response = await httpClient.GetAsync("https://api-dev.channelengine.net/api/v2/orders?statuses=IN_PROGRESS&apikey="+ api_key))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                obj = JsonConvert.DeserializeObject(apiResponse);
            }

            List<OrderModel> orderList = new List<OrderModel>();

            // Add Order Line data into OrderList
            foreach (var order in obj.Content)
            {
                JArray lines = (JArray)order["Lines"];                
                IList<OrderModel> order_lines = lines.ToObject<List<OrderModel>>();
                orderList.AddRange(order_lines);
            }

            return orderList;
        }


        public async Task<List<ProductSoldModel>> GetTopFiveProductsSold(List<OrderModel> orderList)
        {
            List<ProductSoldModel> productList = new List<ProductSoldModel>();

            foreach (var order in orderList)
            {
                //Find existing product in the productList
                var product = productList.Find(x => x.MerchantProductNo == order.MerchantProductNo);

                if(product is null)
                {
                    ProductSoldModel model = new ProductSoldModel();
                    model.GTIN = order.Gtin;
                    model.MerchantProductNo = order.MerchantProductNo;
                    model.TotalQuantity += order.Quantity;
                    model.ProductName = (await GetProductByMerchantProductNo(model.MerchantProductNo)).Name;
                    model.Stock = (await GetProductByMerchantProductNo(model.MerchantProductNo)).Stock;
                    productList.Add(model);
                }
                else
                {
                    product.TotalQuantity += order.Quantity;
                }
            }

            // Select Top 5, sort by totalQuantity Desc
            var result = productList.OrderByDescending(x => x.TotalQuantity).Take(5).ToList();

            return result;
        }

        public async Task<dynamic> GetProductByMerchantProductNo(string merchantProductNo)
        {
            dynamic obj;

            // Fetch orders from the api call
            using (var response = await httpClient.GetAsync("https://api-dev.channelengine.net/api/v2/products/" + merchantProductNo + "?apikey=" + api_key))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                obj = JsonConvert.DeserializeObject(apiResponse);
            }

            return obj.Content;
        }

        public async Task UpdateProductStock(string merchantProductNo)
        {
            var product = await GetProductByMerchantProductNo(merchantProductNo);

            product.Stock = 25;

            dynamic[] requestObj = new dynamic[1];

            requestObj[0] = product;

            string jsonString = JsonConvert.SerializeObject(requestObj);
            
            var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
            var response = httpClient.PostAsync("https://api-dev.channelengine.net/api/v2/products?ignoreStock=false&apikey=" + api_key, content);
            var result = response.Result;

            result.EnsureSuccessStatusCode();
            return;
        }

        

    }
}
