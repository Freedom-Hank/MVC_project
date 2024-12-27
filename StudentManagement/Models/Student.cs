namespace StudentManagement.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        public required string Name { get; set; }
        public string? StudentNumber { get; set; }
        public int? Grade { get; set; }
        public char? Class { get; set; }
        public string? Gender { get; set; }
        public string? Nationality { get; set; }

        private string _password;

        public required string Password
        {
            get => _password ?? StudentNumber ?? string.Empty;
            set => _password = value ?? StudentNumber ?? string.Empty;
        }
    }
}
