namespace EmployeeManagement.Models
{
    public class EmployeeShiftLog
    {
        public int id {  get; set; }
        public DateTime? CheckInTime { get; set; }
        public DateTime? CheckOutTime { get; set; }
        public int EmployeeId { get; set; }
        public int EmployeeShiftId { get; set; }
        public Employee employee { get; set; }
        public EmployeeShift employeeShift { get; set; }

    }
}
