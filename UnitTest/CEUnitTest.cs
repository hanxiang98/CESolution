using CE.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using UnitTest;

namespace CE.Service.Tests
{
    [TestClass()]
    public class CEUnitTest
    {
        [TestMethod()]
        public async Task GetTopFiveProductsSoldTestAsync()
        {
            TestData test = new TestData();
            var testData = test.GetTestData();

            // Act
            OrderService service = new OrderService();
            var top5Products = await service.GetTopFiveProductsSold(testData);

            // Assert
            Assert.AreEqual("6", top5Products[0].MerchantProductNo, "First element in top 5 product list is not correct.");
            Assert.AreEqual("5", top5Products[1].MerchantProductNo, "Second element in top 5 product list is not correct.");
            Assert.AreEqual("4", top5Products[2].MerchantProductNo, "Third element in top 5 product list is not correct.");
            Assert.AreEqual("3", top5Products[3].MerchantProductNo, "Fourth element in top 5 product list is not correct.");
            Assert.AreEqual("2", top5Products[4].MerchantProductNo, "Fifth element in top 5 product list is not correct.");
        }
    }
}
