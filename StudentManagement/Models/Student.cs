namespace StudentManagement.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        public string Name { get; set; }
        public int? StudentNumber { get; set; }
        public int? Grade { get; set; }
        public char? Class { get; set; }
        public string? Gender { get; set; }
        public string? Nationality { get; set; }
        // 添加更多屬性，如 Email、Phone 等
    }
}
