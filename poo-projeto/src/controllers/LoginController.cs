using Microsoft.AspNetCore.Mvc;
using SistemaFinanceiro.Services;

namespace SistemaFinanceiro.Controllers
{
    // Controller responsável pela autenticação dos utilizadores.
    // Expõe um endpoint da API que permite validar email e password.
    [ApiController] // Indica que esta classe é um controller de API
    [Route("api/login")] // Define a rota base: /api/login
    public class LoginController : ControllerBase
    {
        // Serviço responsável pela lógica de autenticação
        private readonly Login _login;

        // Construtor do controller.
        // Recebe o serviço Login por injeção de dependências.
        public LoginController(Login login)
        {
            _login = login;
        }


        // Endpoint para autenticação do utilizador.
        // Recebe email e password no corpo do pedido (JSON).
        // <param name="dto">Objeto com email e password</param>
        // 200 OK com dados do utilizador se sucesso,
        // 401 Unauthorized se credenciais inválidas,
        // 400 BadRequest se dados inválidos

        [HttpPost]
        public IActionResult Login([FromBody] LoginDto dto)
        {
            // Verifica se o corpo do pedido é nulo
            if (dto == null)
                return BadRequest();

            // Tenta autenticar o utilizador com os dados fornecidos
            var user = _login.Autenticar(dto.Email, dto.Password);

            // Se não existir utilizador com essas credenciais
            if (user == null)
                return Unauthorized();

            // Retorna apenas os dados necessários do utilizador
            return Ok(new
            {
                user.Id,
                user.Nome,
                user.Email,
                user.Perfil
            });
        }
    }


    // DTO (Data Transfer Object) usado no login.
    //Representa os dados recebidos do frontend.

    public class LoginDto
    {

        // Email do utilizador

        public string Email { get; set; } = "";

        // Password do utilizador
        public string Password { get; set; } = "";
    }
}
