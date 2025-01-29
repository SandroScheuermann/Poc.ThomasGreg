using Microsoft.AspNetCore.Mvc;
using Poc.ThomasGreg.Application.DTOs;
using Poc.ThomasGreg.Application.Services.Interfaces;

namespace Poc.ThomasGreg.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AutenticacaoController : ControllerBase
    {
        private readonly IAutenticacaoService _autenticacaoService;

        public AutenticacaoController(IAutenticacaoService usuarioService)
        {
            _autenticacaoService = usuarioService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDto)
        {
            var token = await _autenticacaoService.AutenticarUsuarioAsync(loginDto);

            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized(new { Message = "Email ou senha inválidos." });
            }

            return Ok(new { Token = token });
        }

        [HttpPost("registrar")]
        public async Task<IActionResult> Registrar([FromBody] RegistrarUsuarioDTO registrarUsuarioDTO)
        {
            await _autenticacaoService.RegistrarUsuarioAsync(registrarUsuarioDTO); 

            return Ok();
        } 
    }
}
