using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using ISProject.Domain;

namespace ISProject.Web.Controllers
{
    public class PartnerStoreController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public PartnerStoreController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            string albumsApiUrl = "https://musicstoreweb20250211174318.azurewebsites.net/api/admin/albums";
            string artistsApiUrl = "https://musicstoreweb20250211174318.azurewebsites.net/api/admin/artists";

            try
            {
                var client = _httpClientFactory.CreateClient();

                HttpResponseMessage albumsResponse = await client.GetAsync(albumsApiUrl);
                albumsResponse.EnsureSuccessStatusCode();
                string albumsJson = await albumsResponse.Content.ReadAsStringAsync();
                var albums = JsonSerializer.Deserialize<List<PartnerStoreAlbum>>(albumsJson, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                HttpResponseMessage artistsResponse = await client.GetAsync(artistsApiUrl);
                artistsResponse.EnsureSuccessStatusCode();
                string artistsJson = await artistsResponse.Content.ReadAsStringAsync();
                var artists = JsonSerializer.Deserialize<List<PartnerStoreArtist>>(artistsJson, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return View((albums, artists));
            }
            catch (HttpRequestException ex)
            {
                ViewBag.Error = "Error fetching data from API: " + ex.Message;
                return View((new List<PartnerStoreAlbum>(), new List<PartnerStoreArtist>()));
            }
        }
    }
}
