namespace StudentManagement.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        public required string Name { get; set; }
        public int? StudentNumber { get; set; }
        public int? Grade { get; set; }
        public char? Class { get; set; }
        public string? Gender { get; set; }
        public string? Nationality { get; set; }
        public required string Password {
            get => Password ?? (StudentNumber.HasValue ? StudentNumber.Value.ToString() : string.Empty);
            set => Password = value ?? (StudentNumber.HasValue ? StudentNumber.Value.ToString() : string.Empty);
        }
    }
}
