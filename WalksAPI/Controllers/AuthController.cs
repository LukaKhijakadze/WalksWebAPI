using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WalksAPI.Database.DTOModel;
using WalksAPI.Services.IRepository;

namespace WalksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public UserManager<IdentityUser> UserManager { get; }
        public ITokenRepository TokenRepository { get; }

        public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository)
        {
            UserManager = userManager;
            TokenRepository = tokenRepository;
        }


        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Reister([FromBody] RegisterRequestDto registerRequestDto)
        {
            var identityUser = new IdentityUser()
            {
                UserName = registerRequestDto.Username,
                Email = registerRequestDto.Username,
            };

            var identityResult = await UserManager.CreateAsync(identityUser, registerRequestDto.Password);

            if(identityResult.Succeeded)
            {
                if(registerRequestDto.Roles.Any(x => x != null))
                {
                    identityResult = await UserManager.AddToRolesAsync(identityUser, registerRequestDto.Roles);
                    if (identityResult.Succeeded)
                    {
                        return Ok("Role Added Successfully");
                    }
                }
            }

            return BadRequest("Can Not Add Role To User");
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            var user = await UserManager.FindByEmailAsync(loginRequestDto.Username);

            if(user != null)
            {
                var userPasswordResult = await UserManager.CheckPasswordAsync(user, loginRequestDto.Password);

                if (userPasswordResult)
                {
                    var roles = await UserManager.GetRolesAsync(user);

                    if (roles != null)
                    {
                        var jwtToken = TokenRepository.CreateJWTToken(user, roles.ToList());
                        return Ok(jwtToken);
                    }

                    return BadRequest("No Roles Selected");
                }
                return BadRequest("Password Incorect");
            }
            return BadRequest("Email Or Password Incorect");
        }
    }
}
