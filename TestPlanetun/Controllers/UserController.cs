using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TestPlanetun.Context;
using TestPlanetun.Models;
using TestPlanetun.Models.ViewModels;
using Microsoft.Extensions.Configuration;

namespace TestPlanetun.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly testPlanetunContext _context;
        private IConfiguration _config;

        public UserController(testPlanetunContext context, IConfiguration Configuration)
        {
            _context = context;
            _config = Configuration;
        }

        [HttpPost]
        public async Task<ActionResult<User>> Post(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                user = new
                {
                    id = user.Id,
                    name = user.Name,
                    login = user.Login,
                },
                token = GenerateToken()
            }) ;
        }

        [HttpPost,Route("login")]
        public async Task<ActionResult<User>> Login(LoginViewModel login)
        {
            var user = await _context.Users.Where(s => s.Login == login.Login && s.Password == login.Password).FirstOrDefaultAsync();

            if (user == null)
                return Unauthorized();

            return Ok(new
            {
                user = new
                {
                    id = user.Id,
                    name = user.Name,
                    login = user.Login,
                },
                token = GenerateToken()
            });
        }

        private string GenerateToken()
        {
            var _secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var _issuer = _config["Jwt:Issuer"];
            var _audience = _config["Jwt:Audience"];

            var signinCredentials = new SigningCredentials(_secretKey, SecurityAlgorithms.HmacSha256);

            var tokeOptions = new JwtSecurityToken(
                issuer: _issuer,
                audience: _audience,
                claims: new List<Claim>(),
                expires: DateTime.Now.AddHours(2),
                signingCredentials: signinCredentials);

            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);

            return tokenString;
        }
    }
}
