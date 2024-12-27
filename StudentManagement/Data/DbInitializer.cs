using StudentManagement.Data;
using StudentManagement.Models;
using System.Linq;
using System.Security.Claims;

public static class DbInitializer
{
    public static void Initialize(ApplicationDbContext context)
    { 
        // 自動建立資料庫和資料表（如果不存在）
        context.Database.EnsureCreated();

        // 檢查並初始化 `Students` 資料表
        if (!context.Students.Any())
        {
            var students = new Student[]
            {
                new Student { Name = "John Doe", StudentNumber = "411212345", Grade = 2, Class = 'A', Gender = "Male", Nationality = "Taiwan", Password = "411212345" },
                new Student { Name = "Jane Smith", StudentNumber = "411056789", Grade = 4, Class = 'B', Gender = "Female", Nationality = "HongKong", Password = "411056789" },
                new Student { Name = "Bob Johnson", StudentNumber = "411175364", Grade = 4, Class = 'A', Gender = "Male", Nationality = "Vietnam", Password = "411175364" },
            };
            context.Students.AddRange(students);
        }

        // 檢查並初始化 `CourseRegistration` 資料表
        if (!context.CourseRegistrations.Any())
        {
            var courseRegistrations = new CourseRegistration[]
            {
                new CourseRegistration { CourseId = "2576",Class = "資工一A",Category="必修" , Name = "微積分", Credit = 2, Instructor="騰元翔",Schedule ="三 5、6" ,Location="主顧205" ,Semester="上"},
                new CourseRegistration { CourseId = "2598",Class = "資工二B" ,Category="必修", Name = "資料結構", Credit = 3, Instructor="劉建興",Schedule ="三 2、3、4" ,Location="主顧316" ,Semester="上" },
            };
            context.CourseRegistrations.AddRange(courseRegistrations);
        }

        // 儲存變更
        context.SaveChanges();
    }
}
