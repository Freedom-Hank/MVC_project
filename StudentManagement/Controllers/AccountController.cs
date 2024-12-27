using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Data;
using StudentManagement.Models;
using BCrypt.Net;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.AspNetCore.Http;

namespace StudentManagement.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string studentNumber, string password)
        {
            // 查找 Student 表中是否有匹配的帳號和密碼
            var student = _context.Students
                .FirstOrDefault(s => s.StudentNumber.ToString() == studentNumber && s.Password == password);

            if (student != null)
            {
                // 登入成功，將學生資訊存入 Session 或其他儲存機制
                HttpContext.Session.SetString("LoggedInStudentNumber", student.StudentNumber.ToString());
                HttpContext.Session.SetString("StudentName", student.Name);
                return RedirectToAction("Index", "Home");
            }

            // 登入失敗，顯示錯誤訊息
            ViewBag.ErrorMessage = "帳號或密碼錯誤！";
            return View();
        }

        public IActionResult Logout()
        {
            // 清除 Session
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}