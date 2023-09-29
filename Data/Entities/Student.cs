using System.ComponentModel.DataAnnotations.Schema;

namespace StudentsEnrollment.Data.Entities
{
    public class Student: EntityBase
    {
        public int? Age { get; set; }

        public string StudentName { get; set; } = "";

        public string StudentSurname { get; set; } = "";

        public virtual Department Department { get; set; }

        [ForeignKey("Department")]
        public int DepartmentId { get; set; }
    }
}
