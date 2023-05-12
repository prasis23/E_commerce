namespace EmployeeManagement.DTO
{
    public class EmployeeInformationDTO
    {
        public EmployeeInformationDTO(string employeeName, string employeeAddress, string departmentName, string departmentDescription)
        {
            EmployeeName = employeeName;
            EmployeeAddress = employeeAddress;
            DepartmentName = departmentName;
            DepartmentDescription = departmentDescription;
        }

        public string EmployeeName { get; set; }
        public string EmployeeAddress { get; set; }
        public string DepartmentName { get; set; }
        public string DepartmentDescription { get; set; }
    }
}
