using ISProject.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISProject.Repository.Interface
{
    public interface IOrderRepository
    {
        ICollection<Order> GetAll();
        Order? GetOrderDetails(BaseEntity entity);
    }
}
