using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Poc.ThomasGreg.MVC.DTOs;
using Poc.ThomasGreg.MVC.Models;

namespace Poc.ThomasGreg.MVC.Controllers
{
    public class ClienteController : Controller
    {
        private readonly HttpClient _httpClient;

        public ClienteController(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClientFactory.CreateClient();

            _httpClient.BaseAddress = new Uri(Environment.GetEnvironmentVariable("API_BASE_URL"));

			var tokenJson = httpContextAccessor.HttpContext?.Session.GetString("AuthToken");

            if (!string.IsNullOrEmpty(tokenJson))
            {
                var token = JObject.Parse(tokenJson)["token"]?.ToString();
                _httpClient.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }
        }

        public async Task<IActionResult> Index()
        { 
            var response = await _httpClient.GetAsync("Cliente");
            if (response.IsSuccessStatusCode)
            {
                var clientes = await response.Content.ReadFromJsonAsync<IEnumerable<ClienteDTO>>();
                return View(clientes);
            }

            ModelState.AddModelError("", "Erro ao carregar clientes.");
            return View(Enumerable.Empty<ClienteDTO>());
        }


        [HttpGet]
        public IActionResult Adicionar()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Adicionar(ClienteDTO clienteDto)
        { 
            if (clienteDto.LogotipoFile != null && clienteDto.LogotipoFile.Length > 0)
            {
                using var memoryStream = new MemoryStream();
                await clienteDto.LogotipoFile.CopyToAsync(memoryStream);
                clienteDto.Logotipo = memoryStream.ToArray();
            }
             
            var payload = new
            {
                clienteDto.Nome,
                clienteDto.Email,
                clienteDto.Logotipo
            };
             
            var response = await _httpClient.PostAsJsonAsync("Cliente", payload);

            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Cliente cadastrado com sucesso.";
                return RedirectToAction("Index");
            }

            if (response.StatusCode == System.Net.HttpStatusCode.Conflict)
            {
                var error = await response.Content.ReadFromJsonAsync<ApiErrorViewModel>();
                ModelState.AddModelError("", error?.Message?.ToString() ?? "Erro ao cadastrar cliente."); 
            }
            else
            {
                ModelState.AddModelError("", "Erro desconhecido ao cadastrar cliente."); 
            }
             
            return View(clienteDto);
        }

        [HttpGet]
        public async Task<IActionResult> Editar(Guid id)
        {
            var response = await _httpClient.GetAsync($"Cliente/{id}");
            if (response.IsSuccessStatusCode)
            {
                var cliente = await response.Content.ReadFromJsonAsync<ClienteDTO>();
                return View(cliente); 
            }
             
            return View("Error");
        } 

        [HttpPost]
        public async Task<IActionResult> Editar(ClienteDTO clienteDto)
        {
            if (clienteDto.LogotipoFile != null && clienteDto.LogotipoFile.Length > 0)
            {
                using var memoryStream = new MemoryStream();
                await clienteDto.LogotipoFile.CopyToAsync(memoryStream);
                clienteDto.Logotipo = memoryStream.ToArray();
            }

            var payload = new
            {
                clienteDto.Nome,
                clienteDto.Email,
                clienteDto.Logotipo
            };

            var response = await _httpClient.PutAsJsonAsync($"Cliente/{clienteDto.Id}", payload);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Erro ao atualizar cliente.");
            return View(clienteDto);
        }

        [HttpPost]
        public async Task<IActionResult> Deletar(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"Cliente/{id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Erro ao excluir cliente.");
            return RedirectToAction("Index");
        }

    }

}
