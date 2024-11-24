using StudentManagement.Data;
using StudentManagement.Models;
using System.Linq;

public static class DbInitializer
{
    public static void Initialize(ApplicationDbContext context)
    {
        // 檢查是否已經有資料
        if (context.Students.Any())
        {
            return;   // 如果已經有資料，不重複添加
        }

        var students = new Student[]
        {
            new Student { Name = "John Doe",StudentNumber = 411212345 ,Grade = 2 ,Class = null ,Gender = null ,Nationality = null},
            new Student { Name = "Jane Smith",StudentNumber = 411056789, Grade = 4 ,Class = null ,Gender = null ,Nationality = null},
            new Student { Name = "Jane Smith",StudentNumber = 411175364, Grade = 4 ,Class = null ,Gender = null ,Nationality = null},
        };
        context.Students.AddRange(students);
        context.SaveChanges();
    }
}
