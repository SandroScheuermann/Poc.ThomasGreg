using Microsoft.AspNetCore.Mvc;

namespace Poc.ThomasGreg.Controllers
{
    public class HomeController : Controller
    {
        private readonly HttpClient _httpClient;

        public HomeController(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7298/api/");
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
        public async Task<IActionResult> Register(string nome, string email, string senha)
        {  
            var registerDto = new
            {
                Nome = nome,
                Email = email,
                Senha = senha
            };

            var response = await _httpClient.PostAsJsonAsync("Autenticacao/registrar", registerDto);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            ModelState.AddModelError(string.Empty, "Erro ao registrar o usuário.");
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
