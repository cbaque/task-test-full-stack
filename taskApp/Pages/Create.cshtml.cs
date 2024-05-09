using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace taskApp.Pages
{
    public class CreateModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        [BindProperty]
        public string title { get; set; }

        [BindProperty]
        public string description { get; set; }

        [BindProperty]
        public DateTime dateCreate { get; set; } = DateTime.Now;

        [BindProperty]
        public DateTime dateExpirate { get; set; } = DateTime.Now;

        [BindProperty]
        public Boolean complete { get; set; }


        public CreateModel(IHttpClientFactory httpClientFactory, IConfiguration configuration)
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
                    title = title,
                    description = description,
                    dateCreated = dateCreate,
                    dateExpiration = dateExpirate,
                    completed = complete,
                };

                var jsonContent = JsonConvert.SerializeObject(requestData);
                var requestContent = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync($"{apiBaseUrl}/task/create", requestContent);


                if (response.IsSuccessStatusCode)
                {
                    return RedirectToPage("/Index");
                }
                else
                {
                    TempData["error"] = "Error al registrar nueva tarea.";
                    return Page();
                }
            }
            catch (Exception ex)
            {
                TempData["error"] = $"Error: {ex.Message}";
                return Page();
            }
        }

    }
}
