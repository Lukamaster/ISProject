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
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly ApplicationDbContext _context;

        public ShoppingCartRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ShoppingCart> AddProductToCart(ShoppingCart cart, MusicRecord record)
        {
            if (cart != null && record != null)
            {
                if (cart.MusicRecordsInShoppingCart != null && cart.MusicRecordsInShoppingCart.Any(r => r.MusicRecordId == record.Id))
                {
                    var recordInCart = cart.MusicRecordsInShoppingCart.FirstOrDefault(r => r.MusicRecordId == record.Id);
                    recordInCart.Quantity++;
                }
                else
                {
                    var addedRecord = new MusicRecordInShoppingCart
                    {
                        MusicRecordId = record.Id,
                        ShoppingCartId = cart.Id,
                        MusicRecord = record,
                        ShoppingCart = cart
                    };

                    _context.MusicRecordsInShoppingCart.Add(addedRecord);
                }
                await _context.SaveChangesAsync();
                return cart;
            }
            else
            {
                throw new KeyNotFoundException("Cart or Product not found");
            }
        }

        public async Task ClearCartAfterOrder(ShoppingCart cart)
        {
            cart.MusicRecordsInShoppingCart = null;
        }

        public async Task<ShoppingCart> GetCart(Guid Id)
        {
            var cart = await _context.ShoppingCarts
                .Where(c => c.Id == Id)
                .Include(c => c.MusicRecordsInShoppingCart)
                .ThenInclude(r => r.MusicRecord)
                .FirstOrDefaultAsync() ?? throw new KeyNotFoundException("Cart not found");
            return cart;
        }

        public async Task<ShoppingCart> GetCartByUserId(string userId)
        {
            var cart = await _context.ShoppingCarts
                .Where(c => c.OwnerId == userId)
                .Include(c => c.MusicRecordsInShoppingCart)
                .ThenInclude(r => r.MusicRecord)
                .FirstOrDefaultAsync() ?? throw new KeyNotFoundException("Cart not found");
            return cart;
        }

        public async Task RemoveProductFromCart(Guid cartId, Guid productId)
        {
            var record = await _context.MusicRecordsInShoppingCart
                .Where(r => r.ShoppingCartId == cartId && r.MusicRecordId == productId)
                .FirstOrDefaultAsync() ?? throw new KeyNotFoundException("Record not found");

            _context.MusicRecordsInShoppingCart.Remove(record);
            await _context.SaveChangesAsync();
        }
    }
}
