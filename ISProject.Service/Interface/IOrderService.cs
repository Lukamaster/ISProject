using ISProject.Domain;
using ISProject.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISProject.Service.Interface
{
    public interface IOrderService
    {
        Task<List<Order>> GetOrders();
        Task<Order> GetOrderDetails(Guid Id);
        Task DeleteOrder(Guid Id);
        Task<Order> CreateOrder(MusicStoreUser user, Guid cartId);
    }
}
