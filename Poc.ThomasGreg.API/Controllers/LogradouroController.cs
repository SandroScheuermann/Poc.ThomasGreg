using Microsoft.AspNetCore.Mvc;
using Poc.ThomasGreg.Application.DTOs;
using Poc.ThomasGreg.Application.Services.Interfaces;

namespace Poc.ThomasGreg.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LogradouroController : ControllerBase
    {
        public readonly ILogradouroService _logradouroService;

        public LogradouroController(ILogradouroService logradouroService)
        {
            _logradouroService = logradouroService;
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarLogradouro([FromBody] CadastrarLogradouroDTO cadastrarLogradouroDTO)
        {
            var result = await _logradouroService.CriarLogradouroAsync(cadastrarLogradouroDTO);

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarLogradouro(Guid id, [FromBody] AtualizarLogradouroDTO atualizarLogradouroDTO)
        {
            atualizarLogradouroDTO.Id = id;

            var result = await _logradouroService.AtualizarLogradouroAsync(atualizarLogradouroDTO);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoverLogradouro(Guid id)
        {
            await _logradouroService.RemoverLogradouroAsync(id);

            return Ok();
        }

        [HttpGet()]
        public async Task<IActionResult> ListarLogradouros()
        {
            var result = await _logradouroService.ObterLogradourosAsync();

            return Ok(result);
        }

        [HttpGet("{clienteId}")]
        public async Task<IActionResult> ListarLogradourosPorClienteId(Guid clienteId)
        {
            var result = await _logradouroService.ObterLogradourosPorClienteIdAsync(clienteId);

            return Ok(result);
        }
    }
}
