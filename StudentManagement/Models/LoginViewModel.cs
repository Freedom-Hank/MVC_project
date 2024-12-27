using System.ComponentModel.DataAnnotations;

namespace StudentManagement.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "請輸入學號")]
        public required string StudentNumber { get; set; }

        [Required(ErrorMessage = "請輸入密碼")]
        [DataType(DataType.Password)]
        public required string Password { get; set; }
    }
}