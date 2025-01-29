using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Poc.ThomasGreg.Application.DTOs;
using Poc.ThomasGreg.Application.Services.Interfaces;

namespace Poc.ThomasGreg.API.Controllers
{
	[ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService _clienteService; 

        public ClienteController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpPost]
        public async Task<IActionResult> CriarCliente([FromBody] CadastrarClienteDTO clienteDTO)
        {
            try
            {
                var result = await _clienteService.CriarClienteAsync(clienteDTO);

                if (result < 1)
                {
                    return NotFound("Cliente não foi criado.");
                }

                return Ok(result);
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627 || ex.Number == 2601)
                {
                    return Conflict(new { message = "O e-mail informado já está em uso." });
                }

                return StatusCode(500, new { message = "Erro interno no servidor." });
            }
        }


        [HttpPut("{id}")] 
        public async Task<IActionResult> AtualizarCliente(Guid id, [FromBody]AtualizarClienteDTO atualizarClienteDTO)
        { 
            atualizarClienteDTO.Id = id;

            var result = await _clienteService.AtualizarClienteAsync(atualizarClienteDTO);

            return Ok(result);
        }

        [HttpDelete("{id}")] 
        public async Task<IActionResult> RemoverCliente(Guid id)
        {
            await _clienteService.RemoverClienteAsync(id);

            return Ok();
        }

        [HttpGet] 
        public async Task<IActionResult> ObterClientes()
        {
            var result =  await _clienteService.ObterClientesAsync();

            return Ok(result);
        } 

        [HttpGet("{id}")] 
        public async Task<IActionResult> ObterClientePorId(Guid id)
        {
            var result = await _clienteService.ObterClientePorIdAsync(id);

            return Ok(result);
        }
    }
}
