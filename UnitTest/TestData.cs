using CE.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest
{
    public class TestData
    {
        public TestData()
        {

        }

        public List<OrderModel> GetTestData()
        {
            List<OrderModel> orderList = new List<OrderModel>();

            OrderModel model = new OrderModel();
            model.Gtin = "1";
            model.Description = "testing Description";
            model.ChannelProductNo = "testing ChannelProductNo";
            model.MerchantProductNo = "1";
            model.Quantity = 25;
            orderList.Add(model);


            model = new OrderModel();
            model.Gtin = "2";
            model.Description = "testing Description";
            model.ChannelProductNo = "testing ChannelProductNo";
            model.MerchantProductNo = "2";
            model.Quantity = 26;
            orderList.Add(model);


            model = new OrderModel();
            model.Gtin = "3";
            model.Description = "testing Description";
            model.ChannelProductNo = "testing ChannelProductNo";
            model.MerchantProductNo = "3";
            model.Quantity = 27;
            orderList.Add(model);


            model = new OrderModel();
            model.Gtin = "4";
            model.Description = "testing Description";
            model.ChannelProductNo = "testing ChannelProductNo";
            model.MerchantProductNo = "4";
            model.Quantity = 28;
            orderList.Add(model);


            model = new OrderModel();
            model.Gtin = "5";
            model.Description = "testing Description";
            model.ChannelProductNo = "testing ChannelProductNo";
            model.MerchantProductNo = "5";
            model.Quantity = 29;
            orderList.Add(model);


            model = new OrderModel();
            model.Gtin = "6";
            model.Description = "testing Description";
            model.ChannelProductNo = "testing ChannelProductNo";
            model.MerchantProductNo = "6";
            model.Quantity = 30;
            orderList.Add(model);

            return orderList;
        }
    }
}
