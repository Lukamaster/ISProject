using ISProject.Domain;
using ISProject.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISProject.Service.Interface
{
    public interface IShoppingCartService
    {
        Task<ShoppingCart> AddProductToShoppingCart(string userId, MusicRecord record);
        Task<ShoppingCart> GetShoppingCartDetails(string userId);
        Task DeleteFromShoppingCart(string userId, Guid productId);
        //Boolean orderProducts(string userId);
    }
}
