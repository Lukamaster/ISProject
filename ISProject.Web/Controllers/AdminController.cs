using ISProject.Domain.DTO;
using ISProject.Domain.Identity;
using ISProject.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ISProject.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly UserManager<MusicStoreUser> _userManager;
        private readonly IMusicRecordService _musicRecordService;
        private readonly IOrderService _orderService;

        public AdminController(UserManager<MusicStoreUser> userManager, IMusicRecordService musicRecordService, IOrderService orderService)
        {
            _userManager = userManager;
            _musicRecordService = musicRecordService;
            _orderService = orderService;
        }

        [HttpPost("import-users")]
        public async Task<IActionResult> ImportUsers(List<UserImportDTO> users)
        {
            if(users != null && users.Any())
            {
                foreach (var user in users)
                {
                    var userExists = await _userManager.FindByEmailAsync(user.Email);

                    if (userExists == null)
                    {
                        var newUser = new MusicStoreUser
                        {
                            FirstName = user.FirstName,
                            LastName = user.LastName,
                            Address = user.Address,
                            Email = user.Email,
                            UserName = user.Email
                        };

                        var result = await _userManager.CreateAsync(newUser, user.Password);

                        if (result.Succeeded)
                        {
                            await _userManager.AddToRoleAsync(newUser, user.Role);
                        }
                    }
                }
            }

            return Ok();
        }

        [HttpGet("users")]
        public async Task<IActionResult> ExportAllUsers()
        {
            var users = await _userManager.Users.ToListAsync();
            return Ok(users);
        }

        [HttpGet("products")]
        public async Task<IActionResult> ExportAllProducts()
        {
            var products = await _musicRecordService.GetAll();
            return Ok(products);
        }

        [HttpDelete("products/{Id}")]
        public async Task<IActionResult> DeleteProductAdmin(string Id)
        {
            await _musicRecordService.DeleteRecordAsync(Guid.Parse(Id));
            return Ok();
        }

        [HttpGet("orders")]
        public async Task<IActionResult> ExportAllOrders()
        {
            var orders  = await _orderService.GetOrders();
            return Ok(orders);
        }
    }
}

