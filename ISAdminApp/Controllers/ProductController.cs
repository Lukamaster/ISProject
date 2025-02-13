using ClosedXML.Excel;
using ISAdminApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;


namespace ISAdminApp.Controllers
{
    public class ProductController : Controller
    {
        // GET: ProductController
        public ActionResult Index()
        {
            List<Product> products = new List<Product>();

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var response = client.GetAsync("https://localhost:7032/api/admin/products").Result;
                    response.EnsureSuccessStatusCode(); // Ensure the request was successful

                    var jsonString = response.Content.ReadAsStringAsync().Result;

                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true,
                        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
                    };

                    products = JsonSerializer.Deserialize<List<Product>>(jsonString, options);
                }
            }
            catch (Exception ex)
            {
                // Log the exception (you can use a logging framework here)
                Console.WriteLine($"Error: {ex.Message}");
            }

            return View(products);
        }

        public ActionResult ExportProductsToSpreadSheet()
        {
            List<Product> products = new List<Product>();

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var response = client.GetAsync("https://localhost:7032/api/admin/products").Result;
                    response.EnsureSuccessStatusCode(); // Ensure the request was successful

                    var jsonString = response.Content.ReadAsStringAsync().Result;

                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true,
                        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
                    };

                    products = JsonSerializer.Deserialize<List<Product>>(jsonString, options);
                }
            }
            catch (Exception ex)
            {
                // Log the exception (you can use a logging framework here)
                Console.WriteLine($"Error: {ex.Message}");
            }

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Products");
                var currentRow = 1;

                worksheet.Cell(currentRow, 1).Value = "Id";
                worksheet.Cell(currentRow, 2).Value = "Name";
                worksheet.Cell(currentRow, 3).Value = "Description";
                worksheet.Cell(currentRow, 4).Value = "Artist";
                worksheet.Cell(currentRow, 5).Value = "Volume";
                worksheet.Cell(currentRow, 6).Value = "Price";

                foreach (var product in products)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = product.Id;
                    worksheet.Cell(currentRow, 2).Value = product.Title;
                    worksheet.Cell(currentRow, 3).Value = product.Description;
                    worksheet.Cell(currentRow, 4).Value = product.Artist;
                    worksheet.Cell(currentRow, 5).Value = product.Volume;
                    worksheet.Cell(currentRow, 6).Value = product.Price;
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Products.xlsx");
                }
            }
        }

        // GET: ProductController/Details/5
        public ActionResult Details(Product prod)
        {
            return View(prod);
        }

        // GET: ProductController/Delete/5
        public ActionResult Delete(int id)
        {
            return View(id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var response = await client.DeleteAsync($"https://localhost:44300/api/products/{id}");
                    response.EnsureSuccessStatusCode();
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
