using ISProject.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISProject.Repository.Interface
{
    public interface IShoppingCartRepository
    {
        Task<ShoppingCart> GetCart(Guid Id);
        Task<ShoppingCart> GetCartByUserId(string userId);
        Task<ShoppingCart> AddProductToCart(ShoppingCart cart, MusicRecord record);
        Task RemoveProductFromCart(Guid cartId, Guid productId);
        Task ClearCartAfterOrder(ShoppingCart cart);
    }
}
