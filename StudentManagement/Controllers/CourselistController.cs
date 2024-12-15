using Microsoft.AspNetCore.Mvc;

namespace StudentManagement.Controllers
{
    public class CourselistController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
