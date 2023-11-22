using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApiPrestamos.Dtos;
using WebApiPrestamos.Dtos.Auth;
using WebApiPrestamos.Services;

namespace WebApiPrestamos.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IEmailSenderServices _emailSenderService;

        public AuthController(
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager,
            IConfiguration configuration,
            IEmailSenderServices emailSenderService)
        {
            this._signInManager = signInManager;
            this._userManager = userManager;
            this._configuration = configuration;
            this._emailSenderService = emailSenderService;
        }

        //Login de usuario
        [HttpPost("Login")]
        public async Task<ActionResult<ResponseDto<LoginResponseDto>>> Login(LoginDto dto)
        {
            var result = await _signInManager
                .PasswordSignInAsync(dto.Email, dto.Password,
                    isPersistent: false, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                var user = await _userManager.FindByEmailAsync(dto.Email);

                // Crear Claims
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim("UserId", user.Id)
                };

                var userRoles = await _userManager.GetRolesAsync(user);
                foreach (var role in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, role));
                }

                // Generar token
                var jwtToken = GetToken(authClaims);

                var loginResponseDto = new LoginResponseDto
                {
                    Email = user.Email,
                    FullName = "",
                    TokenExpiration = jwtToken.ValidTo,
                    Token = new JwtSecurityTokenHandler().WriteToken(jwtToken)
                };


                    //Enviar Email al ususario porque ingreso en la aplicacion
                //await _emailSenderService.SendEmailAsync(user.Email, "WebApiAutores -- Incio de sesion",
                //EmailTemplates.LoginTemplate(user.Email));

                return Ok(new ResponseDto<LoginResponseDto>
                {
                    Status = true,
                    Message = "Autenticacion satisfactoria",
                    Data = loginResponseDto
                });
            }
            return StatusCode(StatusCodes.Status401Unauthorized, new ResponseDto<LoginResponseDto>
            {
                Status = false,
                Message = "La autenticacion fallo."
            });
        }

        //Registro de usuario
        [HttpPost("Register")]
        public async Task<ActionResult<ResponseDto<RegisterUserDto>>> RegisterUser (RegisterUserDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);

            if (user is not null)
            {
                return BadRequest(new ResponseDto<object>
                {
                    Status = false,
                    Message = $"El usuario con el correo {dto.Email} ya se encuentra registrado"
                });
            }

            var indetityUser = new IdentityUser
            {
                Email = dto.Email,
                UserName = dto.Email
            };

            var result = await _userManager.CreateAsync(indetityUser, dto.Password);

            if (!result.Succeeded)
            {
                return BadRequest(new ResponseDto<object>
                {
                    Status = false,
                    Message = $"Fallo el registro del usuario",
                    Data = result.Errors
                });
            }

            return Ok(new ResponseDto<object>
            {
                Status = true,
                Data = dto
            });
        }


        //Funciones

        //Obtener TOKEN
        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSinginKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.UtcNow.AddHours(1),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSinginKey,
                    SecurityAlgorithms.HmacSha256)
                );

            return token;
        }


    }
}
