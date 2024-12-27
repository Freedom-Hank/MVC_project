using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using StudentManagement.Models;

namespace StudentManagement.Controllers
{
    public class CourserecordController : Controller
    {
        private readonly IConfiguration _configuration;

        public CourserecordController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> GetCourseInfo(string courseId, string grade, string status)
        {
            var model = new CourseRecordViewModel();

            if (string.IsNullOrEmpty(courseId) || string.IsNullOrEmpty(grade))
            {
                model.ErrorMessage = "課程代碼和成績不能為空！";
                return View("Index", model);
            }

            // 使用正確的資料表名稱 CourseRegistrations
            string courseName = null;
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                string courseQuery = @"SELECT Name FROM CourseRegistrations WHERE CourseId = @CourseId";
                SqlCommand command = new SqlCommand(courseQuery, connection);
                command.Parameters.AddWithValue("@CourseId", courseId);

                try
                {
                    await connection.OpenAsync();
                    courseName = await command.ExecuteScalarAsync() as string;
                }
                catch (SqlException ex)
                {
                    model.ErrorMessage = $"資料庫錯誤：{ex.Message}";
                    return View("Index", model);
                }
            }

            if (string.IsNullOrEmpty(courseName))
            {
                model.ErrorMessage = $"找不到選課代碼為 {courseId} 的課程！";
                return View("Index", model);
            }

            // 將查詢結果新增到 model
            model.CourseRecords.Add(new CourseRecord
            {
                CourseId = courseId,
                CourseName = courseName,
                Grade = grade,
                Status = status == "已修課" ? "已修課" : "進修中"
            });

            return View("Index", model);
        }

        [HttpGet]
        public IActionResult Index()
        {
            var model = new CourseRecordViewModel();
            return View(model);
        }
    }
}