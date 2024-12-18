using Microsoft.AspNetCore.Identity;

namespace StudentManagement.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string StudentName { get; set; }
        public string StudentNumber { get; set; }
        public int Grade { get; set; }
        public char? Class { get; set; }
        public string Gender { get; set; }
        public string Nationality { get; set; }
    }
}
