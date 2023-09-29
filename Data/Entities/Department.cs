namespace StudentsEnrollment.Data.Entities
{
    public class Department: EntityBase
    {
        public string DepartmentName { get; set; } = "";

        public int StudentCount { get; set; }

        public virtual List<Student> Students { get; set; } = new List<Student>();

        public override string ToString()
        {
            return DepartmentName;
        }

        public override bool Equals(object? obj)
        {
            Department? d = obj as Department;
            if (d.DepartmentName == d.DepartmentName)
                return true;

            return false;
        }
    }
}
