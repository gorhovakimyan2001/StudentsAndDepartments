using StudentsEnrollment.Data.Entities;

namespace StudentsEnrollment.Models
{
    public class StudentModel
    {
        public int ID { get; set; }

        public int? Age { get; set; }

        public string Name { get; set; } = "";

        public string Surname { get; set; } = "";

        public string Department { get; set; } = "";
    }
}
