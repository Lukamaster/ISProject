using ISProject.Domain;
using ISProject.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISProject.Repository.Interface
{
    public interface IOrderRepository
    {
        Task<List<Order>> GetAll();
        Task<Order> GetOrderDetails(Guid Id);
        Task DeleteOrder(Guid Id);
        Task<Order> CreateOrder(MusicStoreUser user, ShoppingCart cart);
    }
}
