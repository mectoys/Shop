using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Web.Data;
using Shop.Web.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Web.Controllers.API
{
    [Authorize]
    public class OrdersController:Controller
    {
        private readonly IOrderRepository orderRepository;
        private readonly IProductRepository productRepository;

        public OrdersController(IOrderRepository orderRepository, IProductRepository productRepository  )
        {
            this.orderRepository = orderRepository;
            this.productRepository = productRepository;
        }

        public async Task<IActionResult> Index()
        {
            var model = await orderRepository.GetOrdersAsync(this.User.Identity.Name);
            return View(model);
        }
        public async Task<IActionResult> Create()
        {
            var model = await this.orderRepository.GetDetailTempsAsync(this.User.Identity.Name);
            return this.View(model);
        }

    }
}
