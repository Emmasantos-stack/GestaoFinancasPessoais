using Microsoft.AspNetCore.Mvc;
using SistemaFinanceiro.Services;

namespace SistemaFinanceiro.Controllers
{
    [ApiController]
    [Route("api/login")]
    public class LoginController : ControllerBase
    {
        private readonly Login _login;

        public LoginController(Login login)
        {
            _login = login;
        }

        [HttpPost]
        public IActionResult Login([FromBody] LoginDto dto)
        {
            if (dto == null)
                return BadRequest();

            var user = _login.Autenticar(dto.Email, dto.Password);

            if (user == null)
                return Unauthorized();

            return Ok(new
            {
                user.Id,
                user.Nome,
                user.Email,
                user.Perfil
            });
        }
    }

    public class LoginDto
    {
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";
    }
}
