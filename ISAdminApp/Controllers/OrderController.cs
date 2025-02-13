using ISAdminApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ISAdminApp.Controllers
{
    public class OrderController : Controller
    {
        public async Task<ActionResult> Index()
        {
            List<Order> orders = new List<Order>();

            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync("https://localhost:7032/api/admin/orders");
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var jsonOrders = JArray.Parse(jsonString);

                    foreach (var jsonOrder in jsonOrders)
                    {
                        var order = new Order
                        {
                            Id = (string?)jsonOrder["id"] ?? string.Empty,
                            OwnerId = (string?)jsonOrder["ownerId"] ?? string.Empty,
                            MusicRecordsInOrder = new List<Product>()
                        };

                        var musicRecordsInOrder = jsonOrder["musicRecordsInOrder"];
                        if (musicRecordsInOrder != null)
                        {
                            foreach (var jsonMusicRecordInOrder in musicRecordsInOrder)
                            {
                                var jsonProduct = jsonMusicRecordInOrder["musicRecord"];
                                if (jsonProduct != null)
                                {
                                    var product = new Product
                                    {
                                        Id = (string?)jsonProduct["id"] ?? string.Empty,
                                        Title = (string?)jsonProduct["title"] ?? string.Empty,
                                        Description = (string?)jsonProduct["description"] ?? string.Empty,
                                        Artist = (string?)jsonProduct["artist"] ?? string.Empty,
                                        Price = (double?)jsonProduct["price"] ?? 0.0,
                                        Volume = (int?)jsonProduct["volume"] ?? 0,
                                        InStock = (bool?)jsonProduct["inStock"] ?? false,
                                        ImageURL = (string?)jsonProduct["imageURL"] ?? string.Empty
                                    };

                                    order.MusicRecordsInOrder.Add(product);
                                }
                            }
                        }

                        orders.Add(order);
                    }
                }
            }

            return View(orders);
        }

        public async Task<ActionResult> Details(Guid id)
        {
            Order? order = null;

            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync($"https://localhost:7032/api/admin/orders/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var jsonOrder = JObject.Parse(jsonString);

                    order = new Order
                    {
                        Id = (string?)jsonOrder["id"] ?? string.Empty,
                        OwnerId = (string?)jsonOrder["ownerId"] ?? string.Empty,
                        OwnerFirstName = (string?)jsonOrder["owner"]?["firstName"] ?? string.Empty,
                        OwnerLastName = (string?)jsonOrder["owner"]?["lastName"] ?? string.Empty,
                        OwnerAddress = (string?)jsonOrder["owner"]?["address"] ?? string.Empty,
                        MusicRecordsInOrder = new List<Product>()
                    };

                    var musicRecordsInOrder = jsonOrder["musicRecordsInOrder"];
                    if (musicRecordsInOrder != null)
                    {
                        foreach (var jsonMusicRecordInOrder in musicRecordsInOrder)
                        {
                            var jsonProduct = jsonMusicRecordInOrder["musicRecord"];
                            if (jsonProduct != null)
                            {
                                var product = new Product
                                {
                                    Id = (string?)jsonProduct["id"] ?? string.Empty,
                                    Title = (string?)jsonProduct["title"] ?? string.Empty,
                                    Description = (string?)jsonProduct["description"] ?? string.Empty,
                                    Artist = (string?)jsonProduct["artist"] ?? string.Empty,
                                    Price = (double?)jsonProduct["price"] ?? 0.0,
                                    Volume = (int?)jsonProduct["volume"] ?? 0,
                                    InStock = (bool?)jsonProduct["inStock"] ?? false,
                                    ImageURL = (string?)jsonProduct["imageURL"] ?? string.Empty
                                };

                                order.MusicRecordsInOrder.Add(product);
                            }
                        }
                    }
                }
            }

            return View(order);
        }
    }
}
