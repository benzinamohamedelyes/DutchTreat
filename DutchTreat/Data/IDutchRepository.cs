using DutchTreat.Data.Entities;
using System.Collections.Generic;
using System.Linq;

namespace DutchTreat.Data
{
    public interface IDutchRepository
    {
        IEnumerable<Product> GetAllProducts();
        IEnumerable<Product> GetProductsByCategoty(string category);
        bool SaveAll();
        IEnumerable<Order> GetAllOrders(bool includeAllItems);
        Order GetOrderById(int id);
        void AddEntity(object model);
    }
}