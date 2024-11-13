using ISProject.Domain;
using ISProject.Domain.Identity;
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
                 .Include(o => o.Owner)
                 .ToListAsync() ??
                 throw new KeyNotFoundException("No orders found");
        }

        public async Task<Order?> GetOrderDetails(Guid Id)
        {
            return await Orders
                .Where(o => o.Id == Id)
                .Include(o => o.MusicRecordsInOrder)
                    .ThenInclude(ro => ro.MusicRecord)
                .Include(o => o.Owner)
                .FirstOrDefaultAsync() ??
                throw new KeyNotFoundException("Order not found");
        }

        public async Task DeleteOrder(Guid Id)
        {
            var order = await Orders
                .Where(o => o.Id == Id)
                .FirstOrDefaultAsync()
                ?? throw new Exception("Order not found");

            Orders.Remove(order);
            await _context.SaveChangesAsync();
        }

        public async Task<Order> CreateOrder(MusicStoreUser user, ShoppingCart cart)
        {
            var order = new Order
            {
                Owner = user,
                OwnerId = user.Id,
                MusicRecordsInOrder = cart.MusicRecordsInShoppingCart
                    .Select(r => new MusicRecordInOrder
                    {
                        MusicRecord = r.MusicRecord,
                        Quantity = r.Quantity
                    }).ToList()
            };

            _context.Orders.Add(order);
            cart.MusicRecordsInShoppingCart = null;
            await _context.SaveChangesAsync();
            return order;
        }
    }
}
