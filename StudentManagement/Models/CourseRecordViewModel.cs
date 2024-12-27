using StudentManagement.Models;

public class CourseRecordViewModel
{
    public List<CourseRecord> CourseRecords { get; set; } = new List<CourseRecord>();
    public string? ErrorMessage { get; set; }
}
