using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace StudentManagement.Controllers
{
    public class CourserecordController : Controller
    {
        private readonly IConfiguration _configuration;

        public CourserecordController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // 查詢課程及成績
        [HttpPost]
        public async Task<IActionResult> GetCourseInfo(string courseId, string grade, string status)
        {
            // 檢查課程代碼和成績是否為空
            if (string.IsNullOrEmpty(courseId) || string.IsNullOrEmpty(grade))
            {
                ViewBag.ErrorMessage = "課程代碼和成績不能為空！";
                return View("Index");
            }

            // 查詢 CourseRegistration 資料庫中的課程名稱
            string courseName = null;
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("CourseRegistration")))
            {
                string courseQuery = @"SELECT Name FROM CourseRegistration WHERE CourseId = @CourseId";
                SqlCommand command = new SqlCommand(courseQuery, connection);
                command.Parameters.AddWithValue("@CourseId", courseId);

                try
                {
                    await connection.OpenAsync();
                    courseName = await command.ExecuteScalarAsync() as string;
                }
                catch (SqlException ex)
                {
                    ViewBag.ErrorMessage = $"資料庫錯誤：{ex.Message}";
                    return View("Index");
                }
            }

            // 如果未找到課程名稱
            if (string.IsNullOrEmpty(courseName))
            {
                ViewBag.ErrorMessage = $"找不到選課代碼為 {courseId} 的課程！";
                return View("Index");
            }

            // 查詢 Grades 資料庫中的成績
            string courseGrade = null;
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Grades")))
            {
                string gradeQuery = @"SELECT Grade FROM Grades WHERE CourseId = @CourseId";
                SqlCommand command = new SqlCommand(gradeQuery, connection);
                command.Parameters.AddWithValue("@CourseId", courseId);

                try
                {
                    await connection.OpenAsync();
                    courseGrade = await command.ExecuteScalarAsync() as string;
                }
                catch (SqlException ex)
                {
                    ViewBag.ErrorMessage = $"資料庫錯誤：{ex.Message}";
                    return View("Index");
                }
            }

            // 傳遞查詢結果給視圖
            ViewBag.CourseId = courseId;
            ViewBag.CourseName = courseName;
            ViewBag.Grade = courseGrade;
            ViewBag.Status = status == "已修課" ? "已修課" : "進修中";

            return View("Index");
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

    }
}
