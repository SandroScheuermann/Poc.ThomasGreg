using Microsoft.AspNetCore.Mvc;
using Poc.ThomasGreg.Application.DTOs;
using Poc.ThomasGreg.Application.Services.Interfaces;

namespace Poc.ThomasGreg.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService _clienteService; 

        public ClienteController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpPost]
        public async Task<IActionResult> CriarCliente([FromBody] ClienteDTO clienteDTO)
        {
            //TODO: Criar validações para email

            var result = await _clienteService.CriarClienteAsync(clienteDTO);
             
            if(result < 1)
            {
                return NotFound();
            }
             
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult ObterClientePorId(Guid id)
        {
            return null;
        }

        [HttpPut("{id}")]
        public IActionResult AtualizarCliente(Guid id)
        {
            return null;
        }

        [HttpDelete("{id}")]
        public IActionResult RemoverCliente(Guid id)
        {
            _clienteService.RemoverClienteAsync(id);

            return Ok();
        }
    }
}
