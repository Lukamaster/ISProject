using ISProject.Domain;
using ISProject.Domain.Identity;
using ISProject.Repository;
using Microsoft.AspNetCore.Identity;

public class AccountService
{
    private readonly UserManager<MusicStoreUser> _userManager;
    private readonly ApplicationDbContext _context;

    public AccountService(UserManager<MusicStoreUser> userManager, ApplicationDbContext context)
    {
        _userManager = userManager;
        _context = context;
    }

    public async Task<IdentityResult> RegisterUserAsync(MusicStoreUser user, string password)
    {
        // Create the user
        var result = await _userManager.CreateAsync(user, password);
        if (result.Succeeded)
        {
            // Create a ShoppingCart for the new user
            var shoppingCart = new ShoppingCart
            {
                OwnerId = user.Id,
                Owner = user
            };

            // Save the ShoppingCart to the database
            _context.ShoppingCarts.Add(shoppingCart);
            await _context.SaveChangesAsync();

            // Assign the ShoppingCart to the user
            user.UserCart = shoppingCart;
            await _userManager.UpdateAsync(user);
        }

        return result;
    }
}
