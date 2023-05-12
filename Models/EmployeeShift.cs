using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Models
{
    public class EmployeeShift
    {
        public int Id { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set;}
        public decimal TotalTime { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public List<EmployeeShiftLog> EmployeesShiftLogs { get; set;}


    
    }
}
