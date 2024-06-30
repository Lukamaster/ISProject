using ISProject.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISProject.Service.Interface
{
    public interface IOrderService
    {
        List<Order> GetOrders();
        Order GetOrderDetails(BaseEntity entity);
    }
}
