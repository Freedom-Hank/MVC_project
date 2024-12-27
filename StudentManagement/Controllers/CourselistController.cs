using Microsoft.AspNetCore.Mvc;

namespace StudentManagement.Controllers
{
    public class CourselistController : Controller
    {
        public IActionResult Index()
        {
            // 檢查使用者是否已登入
            if (HttpContext.Session.GetString("LoggedInStudentNumber") == null)
            {
                // 如果未登入，重定向到登入頁面
                return RedirectToAction("Login", "Account");
            }

            // 如果已登入，顯示修課列表
            return View();
        }
    }
}
