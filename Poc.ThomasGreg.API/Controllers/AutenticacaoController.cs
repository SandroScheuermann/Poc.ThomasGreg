using Microsoft.AspNetCore.Mvc;
using Poc.ThomasGreg.Application.DTOs;

namespace Poc.ThomasGreg.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AutenticacaoController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public AutenticacaoController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDto)
        {
            var token = await _usuarioService.AutenticarUsuarioAsync(loginDto.Email, loginDto.Senha);

            if (token == null)
            {
                return Unauthorized(new { Message = "Email ou senha inválidos." });
            }

            return Ok(new { Token = token });
        }

    }
}
