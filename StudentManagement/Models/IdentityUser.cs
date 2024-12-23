using Microsoft.AspNetCore.Identity;

namespace StudentManagement.Models
{
    public class ApplicationUser : IdentityUser
    {
        public int StudentId { get; set; } // 外鍵，關聯到 Student 表
        public virtual Student Student { get; set; } // 導航屬性
    }
}