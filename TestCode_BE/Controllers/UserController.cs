using Microsoft.AspNetCore.Mvc;
using TestCode_BE.Models;

namespace TestCode_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly DataContext _context;

        public UserController(DataContext context)
        {
            _context = context;
        }

        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] User login)
        {
            var user = _context.Users.FirstOrDefault(u => u.user_name == login.user_name && u.password == login.password);

            if (user == null)
            {
                return Unauthorized(new { message = "Invalid credentials" });
            }

            if (!user.is_active)
            {
                return Unauthorized(new { message = "User is inactive" });
            }

            var storageLocations = _context.StorageLocations.ToList();

            return Ok(new
            {
                message = "Login successful",
                storageLocations
            });
        }
    }
}
