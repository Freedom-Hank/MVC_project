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
            new Student { Name = "John Doe", Grade = 1 },
            new Student { Name = "Jane Smith", Grade = 2 }
        };
        context.Students.AddRange(students);
        context.SaveChanges();
    }
}
