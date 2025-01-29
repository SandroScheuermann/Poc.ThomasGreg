using Microsoft.AspNetCore.Mvc;
using Poc.ThomasGreg.MVC.DTOs;
using Poc.ThomasGreg.MVC.Models;

namespace Poc.ThomasGreg.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly HttpClient _httpClient;

        public HomeController(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("https://localhost:5000/api/");
        }

        public IActionResult Index()
        { 
            if (HttpContext.Session.GetString("AuthToken") != null)
            {
                return View("Index");
            }
            return RedirectToAction("Login");
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string senha)
        {  
            var loginDto = new
            {
                Email = email,
                Senha = senha
            };

            var response = await _httpClient.PostAsJsonAsync("Autenticacao/login", loginDto);

            if (response.IsSuccessStatusCode)
            {
                var token = await response.Content.ReadAsStringAsync();
                HttpContext.Session.SetString("AuthToken", token);
                return RedirectToAction("Index");
            }

            ModelState.AddModelError(string.Empty, "E-mail ou senha inválidos.");
            return View("Login");
        } 

        [HttpPost]
        public async Task<IActionResult> Register(UsuarioDTO usuarioDTO)
        {    
            var response = await _httpClient.PostAsJsonAsync("Autenticacao/registrar", usuarioDTO);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            } 

            if (response.StatusCode == System.Net.HttpStatusCode.Conflict)
            {
                var error = await response.Content.ReadFromJsonAsync<ApiErrorViewModel>();
                ModelState.AddModelError("", error?.Message?.ToString() ?? "Erro ao cadastrar usuário.");
            }
            else
            {
                ModelState.AddModelError("", "Erro desconhecido ao cadastrar usuário.");
            }

            return View("Register");
        }

        public IActionResult Register()
        {
            return View("Register");
        }

        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("AuthToken") != null)
            {
                return RedirectToAction("Index");
            } 

            return View("Login");
        } 

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("AuthToken");
            return RedirectToAction("Login");
        }
    }

}
