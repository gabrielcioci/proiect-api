using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace backend_II.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]

    public class AuthController : Controller
    {
        private readonly IJwtAuthManager jwtAuthManager;

        public AuthController(IJwtAuthManager jwtAuthManager)
        {
            this.jwtAuthManager = jwtAuthManager;
        }

        
        public IActionResult Index()
        {
            return View();
        }

        // GET: api/auth
        [AllowAnonymous]
        [HttpPost("auth")]
        public IActionResult Authenticate([FromBody] UserCredentials userCredentials)
        {
            var token = jwtAuthManager.Authenticate
                (userCredentials.Email, userCredentials.Password);
            if (token == null)
                return Unauthorized();
            return Ok(token);
        }
    }
}
