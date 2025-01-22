using CURDAPI.Models;
using E_Commerce_Application_Backend_Module.Models.Repo;
using E_Commerce_Backend_System.Models.Data;
using E_Commerce_Backend_System.Models.Entityes;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;


namespace E_Commerce_Backend_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
         AppDbContext context;


   
        private readonly IAuthService _authService;

        public UsersController(AppDbContext context, IAuthService authService)
        {
           this.context = context;
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(Users user)
        {
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);
            context.Users.Add(user);
            await context.SaveChangesAsync();
            return Ok("User registered successfully");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Users user)
        {
            var existingUser = await context.Users.FirstOrDefaultAsync(u => u.Username == user.Username);
            if (existingUser == null || !BCrypt.Net.BCrypt.Verify(user.PasswordHash, existingUser.PasswordHash))
                return Unauthorized("Invalid credentials");

            var token = _authService.GenerateToken(existingUser);
            return Ok(new { token });
        }
    }
}
