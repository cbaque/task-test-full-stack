using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using taskApp.Model;


namespace taskApp.Pages
{
    public class IndexModel : PageModel
    {

        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public List<TaskModel> Tasks { get; set; }

        public IndexModel(ILogger<IndexModel> logger, IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClient = httpClientFactory.CreateClient();
            _configuration = configuration;
        }

        public async Task OnGetAsync()
        {
            var apiBaseUrl = _configuration["apiUrl"];
            var response = await _httpClient.GetAsync($"{apiBaseUrl}/task/index");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                Tasks = JsonConvert.DeserializeObject<List<TaskModel>>(content);
            }
            else
            {
                // Manejar el caso de error
            }
        }

        public IActionResult EditTask(TaskModel task)
        {
            return RedirectToPage("/Index");
        }


    }
}