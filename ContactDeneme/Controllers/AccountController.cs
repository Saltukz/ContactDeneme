using ContactDeneme.Identity;
using ContactDeneme.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ContactDeneme.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        readonly UserManager<ApplicationUser> _userManager;
        readonly SignInManager<ApplicationUser> _signInManager;
        readonly RoleManager<ApplicationRole> _roleManager;
  

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,RoleManager<ApplicationRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserCreateModel model)
        {
            if (ModelState.IsValid)
            {
   
                var checkUsername = await _userManager.FindByNameAsync(model.Username.Trim());
                if (checkUsername != null)
                {
                    return BadRequest("Kullanıcı adı kullanılmaktadır.");
                }

                var checkEmail= await _userManager.FindByEmailAsync(model.Email.Trim());

                if (checkEmail != null)
                {
                    return BadRequest("Email kullanılmaktadır.");
                }

                ApplicationUser newUser = new ApplicationUser()
                {
                    UserName = model.Username.Trim(),
                    Email = model.Email.Trim(),
                };

                IdentityResult result = await _userManager.CreateAsync(newUser, model.Password.Trim());

                if (result.Succeeded)
                {
                    if (!_roleManager.RoleExistsAsync("User").Result)
                    {
                        ApplicationRole role = new ApplicationRole()
                        {
                            Name = "User"
                        };
                        IdentityResult roleResult = await _roleManager.CreateAsync(role);
                        if (roleResult.Succeeded)
                        {
                            _userManager.AddToRoleAsync(newUser, "User").Wait();
                        }
                    
                    }
                    _userManager.AddToRoleAsync(newUser, "User").Wait();
                    return Ok();

                }
                return BadRequest("Bir hata oluştu");
            }
            return BadRequest("Bilgileri kontrol ediniz.");

        }


      
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginModel model)
        {

            if (ModelState.IsValid)
            {
                ApplicationUser user = await _userManager.FindByNameAsync(model.Username.Trim());
                if (user != null && await _userManager.CheckPasswordAsync(user, model.Password.Trim()))
                {
                    var claims = new[]
                    {
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(JwtRegisteredClaimNames.Sub,user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                };

                    var signinKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("00584011534043FF979EA331ABB70B6D"));

                    var token = new JwtSecurityToken(
                        issuer: "saltukz.com",
                        audience: "saltukz.com",
                        expires: DateTime.UtcNow.AddHours(1),
                        claims: claims,
                        signingCredentials: new Microsoft.IdentityModel.Tokens.SigningCredentials(signinKey, SecurityAlgorithms.HmacSha256)
                        );

                 
                    return Ok(new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(token),
                        expiration = token.ValidTo
                    });
                }

                return BadRequest("Giriş bilgileri hatalı.");
            }

            return BadRequest("Veriler uygun değil.");
        }



    }
}
