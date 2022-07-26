using DisneyApi.Models;
using DisneyApi.Querys.Users;
using DisneyApi.Services.EmailService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ApiGeo.Controllers
{
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AuthenticationController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }


        [HttpPost]
        [Route("/auth/register")]

        public async Task<IActionResult> RegisterUser(RegisterUser model)
        {
            var userExits = await _userManager.FindByIdAsync(model.UserName);

            if (userExits != null)
            {
                return BadRequest();
            }

            var user = new User() { UserName = model.UserName, Email = model.Email };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Status = "Error", Message = $"Error al crear el usuario: {String.Join(", ", result.Errors.Select(x => x.Description))}" });
            }

            // envia un email de registro con los datos del usuario via email

            EmailService email = new();
            await email.SendRegisterEmail(model.Email, model.UserName, model.Password);

            return Ok("El usuario se registro con exito!");
        }


        [HttpPost]
        [Route("/auth/login")]

        public async Task<IActionResult> Login(LoginUser model)
        {
            var ChequedUser = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, false, false);
            if (ChequedUser.Succeeded)
            {
                var currentUser = await _userManager.FindByNameAsync(model.UserName);
                if (currentUser.IsActive)
                {
                    return Ok(await GetToken(currentUser));
                }
            }
            return StatusCode(StatusCodes.Status401Unauthorized, new { Status = "Error", Message = $"El usuario {model.UserName} no esta autorizado" });

        }

        private async Task<LoginResponse> GetToken(User currentUser)
        {
            var userRoles = await _userManager.GetRolesAsync(currentUser);

            var authClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,currentUser.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            authClaims.AddRange(userRoles.Select(x => new Claim(ClaimTypes.Role, x)));

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("KeyDeAlkemyParaElTokenJwtBearer"));

            var token = new JwtSecurityToken(
                    issuer: "https://localhost:7148",
                    audience: "https://localhost:7148",
                    expires: DateTime.Now.AddHours(1),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256));

            var stringToken = new JwtSecurityTokenHandler().WriteToken(token);

            EmailService loginEmail = new();
            await loginEmail.SendLoginEmail(stringToken, token.ValidTo, currentUser.UserName, currentUser.Email);

            return new LoginResponse()
            {
                Token = stringToken,
                ValidTo = token.ValidTo,
                Information = "These data were sent by email, se enviaron estos datos por email"
            };

        }
    }
}
