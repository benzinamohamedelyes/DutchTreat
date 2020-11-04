using DutchTreat.Data.Entities;
using System.Collections.Generic;
using System.Linq;

namespace DutchTreat.Data
{
    public interface IDutchRepository
    {
        IEnumerable<Product> GetAllProducts();
        IEnumerable<Product> GetProductsByCategoty(string category);
        IEnumerable<Order> GetAllOrdersByUser(string name, bool includeAllItems);
        IEnumerable<Order> GetAllOrders(bool includeAllItems);
        Order GetOrderById(string name, int orderId);
        void AddEntity(object model);
        bool SaveAll();

    }
}