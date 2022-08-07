using CE.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MVC.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using CE.Service.Models;
using CE.Service.Interface;

namespace MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IOrderService _orderService;

        public HomeController(ILogger<HomeController> logger
            , IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task<IActionResult> IndexAsync()
        {
            List<OrderModel> orders = await _orderService.GetInProgressOrdersAsync();
            var productsolds = await _orderService.GetTopFiveProductsSold(orders);

            ViewBag.Message = productsolds;
            return View();
        }

        public async Task<IActionResult> UpdateStockAsync(string productNo)
        {
            ViewBag.Message = productNo;
            await _orderService.UpdateProductStock(productNo); 

            return View();
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
