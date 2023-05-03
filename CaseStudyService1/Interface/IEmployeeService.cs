using CaseStudyService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudyService.Interface
{
    public interface IEmployeeService
    {        
        Task<string> SaveEmployee(Employee employee);
        Task<string> UpdateEmployee(Employee employee);
        Task<string> DeleteEmployee(int employeeid);
    }
}
