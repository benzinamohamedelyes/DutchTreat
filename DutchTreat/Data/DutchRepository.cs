using DutchTreat.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DutchTreat.Data
{
    public class DutchRepository : IDutchRepository
    {
        private readonly DutchContext _context;
        private readonly ILogger<DutchRepository> _logger;

        public DutchRepository(DutchContext context, ILogger<DutchRepository> logger)
        {
            this._context = context;
            this._logger = logger;
        }

        public IEnumerable<Order> GetAllOrders(bool includeAllItems = true)
        {
            try
            {
                _logger.LogInformation("Get all Orders was called");
                if (includeAllItems)
                    return _context.Orders.Include(o => o.Items).ThenInclude(i => i.Product).ToList();
                else
                    return _context.Orders.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get all the Orders: {ex}");
                throw ex;
            }
        }
        public Order GetOrderById(int id)
        {
            try
            {
                _logger.LogInformation($"Get Order by Id {id}");
                return _context.Orders.Include(o => o.Items).ThenInclude(i => i.Product).FirstOrDefault(o => o.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get Order by Id {id}: { ex}");
                throw ex;
            }
        }
        public IEnumerable<Product> GetAllProducts()
        {
            try
            {
                _logger.LogInformation("Get all products was called");
                return _context.Products.OrderBy(p => p.Title).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get all the producuts: {ex}");
                throw ex;
            }
        }
        public IEnumerable<Product> GetProductsByCategoty(string category)
        {
            try
            {
                return _context.Products.Where(p => p.Category == category).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get the produchts by category: {ex}");
                throw ex;
            }
        }
        public bool SaveAll()
        {
            try
            {
                return _context.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed save: {ex}");
                throw ex;
            }

        }

        public void AddEntity(object model)
        {
            _context.Add(model);
        }

        IEnumerable<Order> IDutchRepository.GetAllOrdersByUser(string name, bool includeAllItems)
        {
            try
            {
                _logger.LogInformation("Get all Orders was called");
                if (includeAllItems)
                    return _context.Orders.Where(o => o.User.UserName == name).Include(o => o.Items).ThenInclude(i => i.Product).ToList();
                else
                    return _context.Orders.Where(o => o.User.UserName == name).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get all the Orders: {ex}");
                throw ex;
            }
        }

        Order IDutchRepository.GetOrderById(string name, int orderId)
        {
            try
            {
                _logger.LogInformation($"Get Order by Id {orderId}");
                return _context.Orders.Include(o => o.Items).ThenInclude(i => i.Product).FirstOrDefault(o => o.User.UserName == name && o.Id == orderId);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get Order by Id {orderId}: { ex}");
                throw ex;
            }
        }
    }
}
