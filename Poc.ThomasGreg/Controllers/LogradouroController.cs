using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json.Linq;
using Poc.ThomasGreg.MVC.DTOs;

namespace Poc.ThomasGreg.MVC.Controllers
{
    public class LogradouroController : Controller
    {
        private readonly HttpClient _httpClient;

        public LogradouroController(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("http://localhost:5001/api/");

            var tokenJson = httpContextAccessor.HttpContext?.Session.GetString("AuthToken");

            if (!string.IsNullOrEmpty(tokenJson))
            {
                var token = JObject.Parse(tokenJson)["token"]?.ToString();
                _httpClient.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }
        }

        [HttpGet]
		public async Task<IActionResult> Index(Guid? clienteId)
		{ 
			var clientesResponse = await _httpClient.GetAsync("Cliente");
			var clientes = new List<SelectListItem>();

			if (clientesResponse.IsSuccessStatusCode)
			{
				var clientesData = await clientesResponse.Content.ReadFromJsonAsync<IEnumerable<ClienteDTO>>();

                if(clientesData != null)
                { 
					clientes = clientesData
						.Select(c => new SelectListItem
						{
							Value = c.Id.ToString(),
							Text = c.Nome,
							Selected = clienteId.HasValue && c.Id == clienteId.Value
						}).ToList();
				}
			}

			ViewBag.Clientes = clientes;
             
			HttpResponseMessage response;
			if (clienteId.HasValue)
			{
				response = await _httpClient.GetAsync($"Logradouro/cliente/{clienteId}");
			}
			else
			{
				response = await _httpClient.GetAsync("Logradouro");
			}

			if (response.IsSuccessStatusCode)
			{
				var logradouros = await response.Content.ReadFromJsonAsync<IEnumerable<LogradouroDTO>>(); 
				ViewBag.ClienteSelecionado = clienteId;
				return View(logradouros);
			}

			ModelState.AddModelError("", "Erro ao carregar Logradouros.");
			return View(Enumerable.Empty<LogradouroDTO>());
		}

		[HttpGet]
        public async Task<IActionResult> Adicionar()
        {
            var clientesResponse = await _httpClient.GetAsync("Cliente");
            if (clientesResponse.IsSuccessStatusCode)
            {
                var clientes = await clientesResponse.Content.ReadFromJsonAsync<IEnumerable<ClienteDTO>>();

                if (clientes is not null)
                {
                    ViewBag.Clientes = clientes.Select(c => new SelectListItem
                    {
                        Value = c.Id.ToString(),
                        Text = c.Nome
                    }).ToList();
                }
            }
            else
            {
                ViewBag.Clientes = new List<SelectListItem>();
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Adicionar(LogradouroDTO LogradouroDTO)
        {
            var payload = new
            {
                LogradouroDTO.Endereco,
                LogradouroDTO.ClienteId
            };

            var response = await _httpClient.PostAsJsonAsync("Logradouro", payload);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Erro ao adicionar logradouro.");
            return View(LogradouroDTO);
        }
        [HttpGet]
        public async Task<IActionResult> Editar(Guid id)
        {
            var response = await _httpClient.GetAsync($"Logradouro/{id}");
            if (response.IsSuccessStatusCode)
            {
                var logradouro = await response.Content.ReadFromJsonAsync<LogradouroDTO>();

                var clientesResponse = await _httpClient.GetAsync("Cliente");
                if (clientesResponse.IsSuccessStatusCode)
                {
                    var clientes = await clientesResponse.Content.ReadFromJsonAsync<IEnumerable<ClienteDTO>>();

                    if (clientes != null && logradouro != null)
                    {
                        ViewBag.Clientes = clientes.Select(c => new SelectListItem
                        {
                            Value = c.Id.ToString(),
                            Text = c.Nome,
                            Selected = c.Id == logradouro.ClienteId

                        }).ToList();
                    }
                }
                else
                {
                    ViewBag.Clientes = new List<SelectListItem>();
                }

                return View(logradouro);
            }

            return View("Erro");
        }

        [HttpPost]
        public async Task<IActionResult> Editar(LogradouroDTO LogradouroDTO)
        {
            var payload = new
            {
                LogradouroDTO.Endereco,
                LogradouroDTO.ClienteId
            };

            var response = await _httpClient.PutAsJsonAsync($"Logradouro/{LogradouroDTO.Id}", payload);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Erro ao atualizar Logradouro.");
            return View(LogradouroDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Deletar(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"Logradouro/{id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Erro ao excluir Logradouro.");
            return RedirectToAction("Index");
        }

    }

}
