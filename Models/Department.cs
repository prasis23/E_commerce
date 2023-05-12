namespace EmployeeManagement.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        //One to many mapping
        public List<Employee> Employees { get; set; }
    }
}
