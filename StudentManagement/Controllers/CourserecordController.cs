using Microsoft.AspNetCore.Mvc;

namespace StudentManagement.Controllers
{
    public class CourserecordController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
