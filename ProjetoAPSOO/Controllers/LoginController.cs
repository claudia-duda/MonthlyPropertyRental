using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;
using ProjetoAPSOO.Services;
using System.Security.Claims;

namespace ProjetoAPSOO.Controllers
{
    [ApiController]
    public class LoginController : Controller
    {
        private readonly IAuthService _authService;

        public LoginController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet("login/google")]
        public IActionResult LoginWithGoogle()
        {
            var redirectUrl = Url.Action("GoogleCallback");
            return Challenge(new AuthenticationProperties { RedirectUri = redirectUrl }, GoogleDefaults.AuthenticationScheme);
        }

        [HttpGet("signin-google")]
        public async Task<IActionResult> GoogleCallback()
        {
            // Recupera as informações do usuário autenticado
            var authenticateResult = await HttpContext.AuthenticateAsync();
            if (!authenticateResult.Succeeded || authenticateResult.Principal == null)
            {
                return Unauthorized(new { Message = "Falha ao autenticar com o Google." });
            }

            var userName = authenticateResult.Principal.FindFirst(ClaimTypes.Name)?.Value;
            var userEmail = authenticateResult.Principal.FindFirst(ClaimTypes.Email)?.Value;

            return Ok(new
            {
                Message = "Autenticação bem-sucedida com o Google.",
                UserName = userName,
                UserEmail = userEmail
            });
        }
    
    }
}
