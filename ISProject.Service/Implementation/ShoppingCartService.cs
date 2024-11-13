using ISProject.Domain;
using ISProject.Domain.DTO;
using ISProject.Repository.Interface;
using ISProject.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISProject.Service.Implementation
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;

        public ShoppingCartService(IShoppingCartRepository shoppingCartRepository)
        {
            _shoppingCartRepository = shoppingCartRepository;
        }

        public async Task<ShoppingCart> AddProductToShoppingCart(string userId, MusicRecord record)
        {
            var cart = await _shoppingCartRepository.GetCartByUserId(userId);
            var updatedCart = await _shoppingCartRepository.AddProductToCart(cart, record);
            return updatedCart;
        }

        public async Task DeleteFromShoppingCart(string userId, Guid productId)
        {
            var cart = await _shoppingCartRepository.GetCartByUserId(userId);
            await _shoppingCartRepository.RemoveProductFromCart(cart.Id, productId);
        }

        public async Task<ShoppingCart> GetShoppingCartDetails(string userId)
        {
            var cart = await _shoppingCartRepository.GetCartByUserId(userId);
            return cart;
        }
    }
}
