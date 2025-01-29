using Microsoft.AspNetCore.Mvc;

namespace Poc.ThomasGreg.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LogradouroController : ControllerBase
    {
        public LogradouroController()
        {
        }

        [HttpPost]
        public IActionResult AdicionarLogradouro()
        {
            return null;
        }

        [HttpGet("{clienteId}")]
        public IActionResult ListarLogradouros(Guid clienteId)
        {
            return null;
        }

        [HttpDelete("{id}")]
        public IActionResult RemoverLogradouro(Guid id)
        {
            return null;
        }
    }
}
