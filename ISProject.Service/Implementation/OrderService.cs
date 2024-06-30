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

        public Order GetOrderDetails(BaseEntity entity)
        {
            return _orderRepository.GetOrderDetails(entity);
        }

        public List<Order> GetOrders()
        {
            return _orderRepository.GetAll().ToList();
        }
    }
}
