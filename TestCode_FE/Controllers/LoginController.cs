using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using TestCode_FE.Models;

namespace TestCode_FE.Controllers
{
    public class LoginController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;

        public LoginController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        [HttpPost]
        public async Task<IActionResult> LoginSubmit(UserModel model)
        {
            if (ModelState.IsValid)
            {
                var client = _clientFactory.CreateClient();

                var json = JsonSerializer.Serialize(new
                {
                    user_name = model.UserName,
                    password = model.Password
                });

                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync("https://localhost:7049/api/User/authenticate", content);

                if (response.IsSuccessStatusCode)
                {
                    HttpContext.Session.SetString("UserName", model.UserName);

                    var json2 = await response.Content.ReadFromJsonAsync<JsonElement>();
                    var locations = JsonSerializer.Deserialize<LoginResponse>(json2);
                    HttpContext.Session.SetString("StorageLocations", JsonSerializer.Serialize(locations.storageLocations));


                    return RedirectToAction("Index", "Home");
                }
                else
                {

                    TempData["Message"] = "Invalid username or password";
                }
            }
            
            return RedirectToAction("Login", "Index");
        }

        public IActionResult Login()
        {
            ViewData["ErrorMessage"] = TempData["Message"] as string;
            return View();
        }
    }
}
