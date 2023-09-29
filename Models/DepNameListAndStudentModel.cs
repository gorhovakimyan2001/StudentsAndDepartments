namespace StudentsEnrollment.Models
{
    public class DepNameListAndStudentModel
    {

        public StudentModel student { get; set; }

        public IEnumerable<string> departmnetsNames { get; set; }

        public override string ToString()
        {
            return student.Name + departmnetsNames.Count();
        }
    }
}
