namespace StudentManagement.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        public string Name { get; set; }
        public int? StudentNumber { get; set; }
        public int? Grade { get; set; }
        public char? Class { get; set; }
        public char? Gender { get; set; }
        public char? Nationality { get; set; }
        // 添加更多屬性，如 Email、Phone 等
    }
}
