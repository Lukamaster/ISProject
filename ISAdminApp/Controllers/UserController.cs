using ISAdminApp.Models;
using ISProject.Domain.Identity;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;

namespace ISAdminApp.Controllers
{
    public class UserController : Controller
    {
        public async Task<IActionResult> Index()
        {
            List<ApplicationUser> users = new List<ApplicationUser>();

            using (var client = new HttpClient())
            {
                var response = await client.GetAsync("https://localhost:7032/api/admin/users");
                if (response.IsSuccessStatusCode)
                {
                    var apiUsers = await response.Content.ReadFromJsonAsync<List<MusicStoreUser>>();
                    if (apiUsers != null)
                    {
                        users = apiUsers.Select(user => new ApplicationUser
                        {
                            FirstName = user.FirstName,
                            LastName = user.LastName,
                            Address = user.Address,
                            Email = user.Email,
                            Password = user.PasswordHash
                        }).ToList();
                    }
                }
                else
                {
                    return StatusCode((int)response.StatusCode, response.ReasonPhrase);
                }
            }

            return View(users);
        }

        [HttpPost]
        public async Task<IActionResult> Import(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("File not selected");
            }

            var users = new List<ApplicationUser>();

            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                using (var package = new ExcelPackage(stream))
                {
                    var worksheet = package.Workbook.Worksheets[0];
                    var rowCount = worksheet.Dimension.Rows;

                    for (int row = 2; row <= rowCount; row++)
                    {
                        var user = new ApplicationUser
                        {
                            FirstName = worksheet.Cells[row, 1].Text,
                            LastName = worksheet.Cells[row, 2].Text,
                            Address = worksheet.Cells[row, 3].Text,
                            Email = worksheet.Cells[row, 4].Text,
                            Password = worksheet.Cells[row, 5].Text,
                            Role = worksheet.Cells[row, 6].Text
                        };
                        users.Add(user);
                    }
                }
            }

            using (var client = new HttpClient())
            {
                var response = await client.PostAsJsonAsync("https://localhost:7032/api/admin/import-users", users);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return StatusCode((int)response.StatusCode, response.ReasonPhrase);
                }
            }
        }
    }
}
