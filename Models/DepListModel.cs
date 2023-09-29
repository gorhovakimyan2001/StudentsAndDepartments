namespace StudentsEnrollment.Models
{
    public class DepListModel
    {
        public DepModel DeletedDep { get; set; }

        public List<String> ExistingDepsNames { get; set; }

        public string ChangedDepName { get; set; } = "";

    }
}
