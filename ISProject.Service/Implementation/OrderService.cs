using ISProject.Domain;
using ISProject.Domain.Identity;
using ISProject.Repository.Interface;
using ISProject.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISProject.Service.Implementation
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IShoppingCartRepository _shoppingCartRepository;

        public OrderService(IOrderRepository orderRepository, IShoppingCartRepository shoppingCartRepository)
        {
            _orderRepository = orderRepository;
            _shoppingCartRepository = shoppingCartRepository;
        }

        public async Task<Order> CreateOrder(MusicStoreUser user, Guid cartId)
        {
            var cart = await _shoppingCartRepository.GetCart(cartId);
            var order = await _orderRepository.CreateOrder(user, cart);
            return order;
        }

        public async Task DeleteOrder(Guid Id)
        {
            await _orderRepository.DeleteOrder(Id);
        }

        public async Task<Order> GetOrderDetails(Guid Id)
        {
            var order = await _orderRepository.GetOrderDetails(Id);
            return order;
        }

        public async Task<List<Order>> GetOrders()
        {
            var orders = await _orderRepository.GetAll();
            return orders;
        }
    }
}
