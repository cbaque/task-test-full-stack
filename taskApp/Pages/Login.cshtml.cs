using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace taskApp.Pages
{
    public class LoginModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public string Password { get; set; }


        public LoginModel(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }


        public async Task<IActionResult> OnPostAsync()
        {
            var apiBaseUrl = _configuration["apiUrl"];
            var httpClient = _httpClientFactory.CreateClient();

            try
            {

                var requestData = new
                {
                    Email = Email,
                    Password = Password
                };

                var jsonContent = JsonConvert.SerializeObject(requestData);
                var requestContent = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync($"{apiBaseUrl}/auth/login", requestContent);


                if (response.IsSuccessStatusCode)
                {
                    var token = await response.Content.ReadAsStringAsync();

                    TempData["token"] = token;
                    return RedirectToPage("/Index");
                }
                else
                {
                    TempData["error"] = "Credenciales incorrectas.";
                    return Page();
                }
            }
            catch (Exception ex)
            {
                TempData["error"] = $"Error al iniciar sesión: {ex.Message}";
                return Page();
            }
        }




    }
}
