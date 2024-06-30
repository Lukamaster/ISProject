using ISProject.Domain;
using ISProject.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISProject.Repository.Implementation
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;
        public DbSet<Order> Orders;

        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
            Orders = context.Set<Order>();
        }

        public ICollection<Order> GetAll()
        {
            return Orders.
                Include(z => z.MusicRecordsInOrder).
                Include("MusicRecordsInOrder.MusicRecord").
                Include("MusicRecordsInOrder.Quantity").
                ToList();
        }

        public Order? GetOrderDetails(BaseEntity entity)
        {
            return Orders.
                Include(z => z.MusicRecordsInOrder).
                Include("MusicRecordsInOrder.MusicRecord").
                Include("MusicRecordsInOrder.Quantity").
                SingleOrDefaultAsync(z => z.Id == entity.Id).Result;
        }
    }
}
