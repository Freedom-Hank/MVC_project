using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentManagement.Controllers
{
    public class CourseRecordController : Controller
    {
        private readonly IConfiguration _configuration;

        public CourseRecordController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // Session 鍵值，用於保存課程記錄
        private const string SessionKeyCourseRecords = "CourseRecords";

        [HttpPost]
        public async Task<IActionResult> GetCourseInfo(string courseId, string grade, string status)
        {
            if (string.IsNullOrEmpty(courseId) || string.IsNullOrEmpty(grade))
            {
                ViewBag.ErrorMessage = "課程代碼和成績不能為空！";
                return View("Index");
            }

            string courseName = null;

            // 查詢課程名稱
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

            if (string.IsNullOrEmpty(courseName))
            {
                ViewBag.ErrorMessage = $"找不到選課代碼為 {courseId} 的課程！";
                return View("Index");
            }

            // 查詢成績
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

            // 新建一筆課程記錄
            var newRecord = new CourseRecord
            {
                CourseId = courseId,
                CourseName = courseName,
                Grade = courseGrade,
                Status = status == "已修課" ? "已修課" : "進修中"
            };

            // 取得 Session 中的現有記錄
            List<CourseRecord> courseRecords = HttpContext.Session.Get<List<CourseRecord>>(SessionKeyCourseRecords) ?? new List<CourseRecord>();
            courseRecords.Add(newRecord);

            // 儲存更新的記錄回 Session
            HttpContext.Session.Set(SessionKeyCourseRecords, courseRecords);

            // 傳遞記錄清單到前端
            ViewBag.CourseRecords = courseRecords;

            return View("Index");
        }
    }

    // 定義課程記錄類別
    public class CourseRecord
    {
        public string CourseId { get; set; }
        public string CourseName { get; set; }
        public string Grade { get; set; }
        public string Status { get; set; }
    }

    // Session 擴展方法
    public static class SessionExtensions
    {
        public static void Set<T>(this ISession session, string key, T value)
        {
            session.SetString(key, System.Text.Json.JsonSerializer.Serialize(value));
        }

        public static T Get<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default(T) : System.Text.Json.JsonSerializer.Deserialize<T>(value);
        }
    }
}


