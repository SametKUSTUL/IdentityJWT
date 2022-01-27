using IdentityJWT.Data;
using IdentityJWT.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace IdentityJWT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController1 : ControllerBase
    {
        private UserManager<AppliicationUser> userManager;

        public AuthController1(UserManager<AppliicationUser> userManager)
        {
            this.userManager = userManager;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user =await userManager.FindByNameAsync(model.UserName);
            if(user !=null && await userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await userManager.GetRolesAsync(user);

                var claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Sub,user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
                };

                foreach (var role in userRoles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }

                var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("AspNetCoreDersim"));

                var token = new JwtSecurityToken(
                issuer: "https://localhost:36587",
                audience: "https://localhost:36587",
                notBefore:DateTime.SpecifyKind(DateTime.Now,DateTimeKind.Utc),
                expires: DateTime.SpecifyKind(DateTime.Now,DateTimeKind.Utc).AddHours(1),
                claims: claims,
                signingCredentials:new SigningCredentials(signingKey,SecurityAlgorithms.HmacSha256)
                );

                return Ok(new
                {
                    token=new JwtSecurityTokenHandler().WriteToken(token),
                    beginTime=token.ValidFrom,
                    expiration=token.ValidTo,
                    message="Giriş Başarili"
                });
            }

            else
            {
                return BadRequest(new
                {
                    message="Kullanıcı adı ve parola yanlış!"
                });
            }

            
        }

    }
}
