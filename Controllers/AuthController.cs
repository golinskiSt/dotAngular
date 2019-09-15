using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WebApp1.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace WebApp1.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<UserModel> _userManager;
        private readonly SignInManager<UserModel> _signInManager;

        public AuthController(UserManager<UserModel> userManager, SignInManager<UserModel> signInManager)
        {
            this._signInManager = signInManager;
            this._userManager = userManager;
        }

        // GET api/values
        [HttpPost, Route("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] UserModel user)
        {
            if (user == null)
            {
                return BadRequest("Invalid client request");
            }
            var result = await _signInManager.PasswordSignInAsync(user.UserName, user.PasswordHash, user.RememberMe, false);

            if (result.Succeeded)
            {
                var tokenString = GenerateJSONWebToken(user.UserName);
                return Ok(new { Token = tokenString });
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpPost, Route("signin")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] UserModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new UserModel { UserName = model.UserName, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.PasswordHash);
                if (result.Succeeded)
                {
                    var tokenString = GenerateJSONWebToken(model.UserName);
                    return Ok(new { Token = tokenString });
                }
            }
            return Unauthorized();
        }

        [Authorize]
        [HttpGet, Route("values")]
        public IActionResult Values()
        {   
            var c = HttpContext.User.Identity.Name;
            return Ok(c);
        }

        private string GenerateJSONWebToken(string username)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Secrets.JWTKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(JwtRegisteredClaimNames.AuthTime, DateTime.Now.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
             };

            var token = new JwtSecurityToken(
                issuer: "http://localhost:5000",
                claims: claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        private string GenerateJSONWebToken(UserModel userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Secrets.JWTKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, userInfo.UserName),
                new Claim(JwtRegisteredClaimNames.Email, userInfo.Email),
                new Claim(JwtRegisteredClaimNames.AuthTime, DateTime.Now.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
             };

            var token = new JwtSecurityToken(
                issuer: "http://localhost:5000",
                claims: claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}