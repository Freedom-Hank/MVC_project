using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using StudentManagement.Models;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace StudentManagement.Controllers
{
    public class CourserecordController : Controller
    {
        private readonly IConfiguration _configuration;

        public CourserecordController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // Session 鍵值，用於保存課程記錄
        private const string SessionKeyCourseRecords = "CourseRecords";

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

            // 創建新的課程記錄
            var newRecord = new CourseRecord
            {
                CourseId = courseId,
                CourseName = courseName,
                Grade = grade,
                Status = status == "已修課" ? "已修課" : "進修中"
            };

            // 從 Session 中取得現有的課程記錄，並初始化為空列表
            var courseRecords = HttpContext.Session.GetObjectFromJson<List<CourseRecord>>(SessionKeyCourseRecords) ?? new List<CourseRecord>();

            // 新的課程記錄加入到列表中
            courseRecords.Add(newRecord);

            // 保存更新後的課程記錄回 Session
            HttpContext.Session.SetObjectAsJson(SessionKeyCourseRecords, courseRecords);

            // 返回顯示課程記錄的頁面
            model.CourseRecords = courseRecords;

            return View("Index", model);
        }

        [HttpGet]
        public IActionResult Index()
        {
            var model = new CourseRecordViewModel
            {
                // 從 Session 中加載課程記錄
                CourseRecords = HttpContext.Session.GetObjectFromJson<List<CourseRecord>>(SessionKeyCourseRecords) ?? new List<CourseRecord>()
            };

            return View(model);
        }
    }
}
