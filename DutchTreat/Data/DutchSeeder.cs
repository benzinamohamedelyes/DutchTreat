using DutchTreat.Data.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DutchTreat.Data
{
    public class DutchSeeder
    {
        private readonly DutchContext _context;
        private readonly IWebHostEnvironment _hosting;

        public DutchSeeder(DutchContext context, IWebHostEnvironment hosting)
        {
            this._context = context;
            this._hosting = hosting;
        }
        public void Seed()
        {
            _context.Database.EnsureCreated();
            if(!_context.Products.Any())
            {
                var filePath = Path.Combine(_hosting.ContentRootPath,"Data/art.json");
                var json = File.ReadAllText(filePath);
                var products = JsonConvert.DeserializeObject<IEnumerable<Product>>(json);
                _context.Products.AddRange(products);
                var order = _context.Orders.FirstOrDefault(o => o.Id == 1);
                if(order != null)
                {
                    new OrderItem
                    {
                        Product = products.First(),
                        Quantity = 5,
                        UnitPrice = products.First().Price
                    };
                }
                _context.SaveChanges();
            }
        }
    }
}
