using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using TestCode_FE.Models;

namespace TestCode_FE.Controllers
{
    public class BpkbController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;

        public BpkbController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = _clientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:7049/api/Bpkb");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadFromJsonAsync<JsonElement>();
                var bpkbs = JsonSerializer.Deserialize<List<BpkbModel>>(json);
                return View(bpkbs);
            }

            return View(new List<BpkbModel>());
        }

        public async Task<IActionResult> Details(string id)
        {
            if (id == null) return NotFound();

            var client = _clientFactory.CreateClient();
            var response = await client.GetAsync($"https://localhost:7049/api/Bpkb/{id}");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadFromJsonAsync<JsonElement>();
                var bpkb = JsonSerializer.Deserialize<BpkbModel>(json);
                return View(bpkb);
            }

            return NotFound();
        }

        public IActionResult Create()
        {
            var model = new BpkbModel();
            var locationsJson = HttpContext.Session.GetString("StorageLocations");
            if (!string.IsNullOrEmpty(locationsJson))
            {
                var locations = JsonSerializer.Deserialize<List<StorageLocationModel>>(locationsJson);
                model.Locations = locations.ConvertAll(location => new SelectListItem
                {
                    Value = location.location_id,
                    Text = location.location_name
                });
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(BpkbModel bpkb)
        {
            var userName = HttpContext.Session.GetString("UserName");
            bpkb.created_by = userName;
            bpkb.last_updated_by = userName;
            bpkb.created_on = DateTime.Now;
            bpkb.last_updated_on = DateTime.Now;

            var client = _clientFactory.CreateClient();
            var json = JsonSerializer.Serialize(bpkb);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("https://localhost:7049/api/Bpkb", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(bpkb);
        }

        public async Task<IActionResult> Edit(string id)
        {
            if (id == null) return NotFound();

            var client = _clientFactory.CreateClient();
            var response = await client.GetAsync($"https://localhost:7049/api/Bpkb/{id}");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadFromJsonAsync<JsonElement>();
                var bpkb = JsonSerializer.Deserialize<BpkbModel>(json);

                var locationsJson = HttpContext.Session.GetString("StorageLocations");
                if (!string.IsNullOrEmpty(locationsJson))
                {
                    var locations = JsonSerializer.Deserialize<List<StorageLocationModel>>(locationsJson);
                    bpkb.Locations = locations.ConvertAll(location => new SelectListItem
                    {
                        Value = location.location_id,
                        Text = location.location_name
                    });
                }

                return View(bpkb);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, BpkbModel bpkb)
        {
            var userName = HttpContext.Session.GetString("UserName");
            bpkb.last_updated_by = userName;
            bpkb.agreement_number = id;

            var client = _clientFactory.CreateClient();
            var json = JsonSerializer.Serialize(bpkb);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"https://localhost:7049/api/Bpkb/{id}", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index","Bpkb");
            }


            return View(bpkb);
        }

        public async Task<IActionResult> Delete(string id)
        {
            if (id == null) return NotFound();

            var client = _clientFactory.CreateClient();
            var response = await client.GetAsync($"https://localhost:7049/api/Bpkb/{id}");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadFromJsonAsync<JsonElement>();
                var bpkb = JsonSerializer.Deserialize<BpkbModel>(json);
                return View(bpkb);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id, BpkbModel bpkb)
        {
            var userName = HttpContext.Session.GetString("UserName");
            bpkb.last_updated_by = userName;
            bpkb.agreement_number = id;

            var client = _clientFactory.CreateClient();

            var response = await client.DeleteAsync($"https://localhost:7049/api/Bpkb/{id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Bpkb");
            }


            return View(bpkb);
        }
    }
}
