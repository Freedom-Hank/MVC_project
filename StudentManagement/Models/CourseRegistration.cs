namespace StudentManagement.Models
{
    public class CourseRegistration
    {
        public int CourseId { get; set; }
        public string Class { get; set; }
        public string Category { get; set; }
        public string Name { get; set; }
        public int Credit { get; set; }
        public string Instructor {  get; set; }
        public string Schedule { get; set; }
        public string Location { get; set; }
        public string Semester { get; set; }

    }
}
