using DutchTreat.Data.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<StoreUser> _userManager;

        public DutchSeeder(DutchContext context, IWebHostEnvironment hosting, UserManager<StoreUser> userManager)
        {
            this._context = context;
            this._hosting = hosting;
            _userManager = userManager;
        }
        public async Task SeedAsync()
        {
            _context.Database.EnsureCreated();
            StoreUser user = await _userManager.FindByEmailAsync("elyes@dutchtreat.com");
            if (user == null)
            {
                user = new StoreUser
                {
                    FirstName = "elyes",
                    LastName = "ben zina",
                    UserName = "elyes@dutchtreat.com",
                    Email = "elyes@dutchtreat.com"
                };
                var result = await _userManager.CreateAsync(user, "P@ssw0rd!");
                if(result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create new user in seeder");
                }
            }
            if (!_context.Products.Any())
            {
                var filePath = Path.Combine(_hosting.ContentRootPath, "Data/art.json");
                var json = File.ReadAllText(filePath);
                var products = JsonConvert.DeserializeObject<IEnumerable<Product>>(json);
                _context.Products.AddRange(products);
                var order = _context.Orders.FirstOrDefault(o => o.Id == 1);
                if (order != null)
                {
                    order.User = user;
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
