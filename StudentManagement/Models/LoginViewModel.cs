using System.ComponentModel.DataAnnotations;

namespace StudentManagement.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Student Number is required.")]
        public required string StudentNumber { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        public required string Password { get; set; }
    }
}