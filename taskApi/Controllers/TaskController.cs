using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using taskApi.Data;
using taskApi.DTO;

namespace taskApi.Controllers
{
    [Route("api/task")]
    [ApiController]
    public class TaskController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TaskController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TaskController
        [HttpGet("index")]
        public ActionResult Index()
        {
            var tasks = _context.Tasks.ToList();

            return Json(tasks);

        }


       [HttpPost("create")]
        public ActionResult Create(TaskDTO data)
        {
            var task = new Tasks
            {
                title = data.title,
                description = data.description,
                dateCreated = data.dateCreated,
                dateExpiration = data.dateExpirated,
                completed = data.completed
            };

            _context.Tasks.Add(task);
            _context.SaveChanges();

            return Ok(new { sms = "Tarea creada correctamente." });
        }




    }
}
