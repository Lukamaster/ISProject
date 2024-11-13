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

        public async Task<List<Order>> GetAll()
        {
            return await Orders
                .Include(o => o.MusicRecordsInOrder)
                    .ThenInclude(ro => ro.MusicRecord)
                 .Include(o => o.MusicRecordsInOrder)
                 .ThenInclude(ro => ro.Quantity)
                 .ToListAsync();
        }

        public async Task<Order?> GetOrderDetails(Guid Id)
        {
            return await Orders
                .Where(o => o.Id == Id)
                .Include(o => o.MusicRecordsInOrder)
                    .ThenInclude(ro => ro.MusicRecord)
                 .Include(o => o.MusicRecordsInOrder)
                 .ThenInclude(ro => ro.Quantity)
                 .FirstOrDefaultAsync();
        }

        public async Task DeleteOrder(Guid Id)
        {
            var order = await Orders
                .Where(o => o.Id == Id)
                .FirstOrDefaultAsync();
            Orders.Remove(order);
            await _context.SaveChangesAsync();
        }
    }
}
