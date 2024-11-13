using ISProject.Domain;
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

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
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
