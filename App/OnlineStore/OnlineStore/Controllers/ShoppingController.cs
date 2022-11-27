using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace OnlineStore.Controllers
{
    public class ShoppingController: Controller
    {
        [HttpGet]
        public IActionResult ShowProducts(int idCategory)
        {
            
        }

        [HttpGet]
        public IActionResult ShowProduct(int idProduct)
        {
            
        }
    }
}
