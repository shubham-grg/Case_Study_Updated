using CaseStudyService.Interface;
using CaseStudyService.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System;

namespace CaseStudyService
{
    public class EmployeeService : IEmployeeService
    {
        HttpClient client = new HttpClient();

        public EmployeeService()
        {
            string authorizationtoken = "fa114107311259f5f33e70a5d85de34a2499b4401da069af0b1d835cd5ec0d56";
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authorizationtoken);
            client.BaseAddress = new Uri("https://gorest.co.in/public-api/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<string> DeleteEmployee(int employeeid)
        {
            var response = await client.DeleteAsync("users/" + employeeid);
            var output = response.StatusCode;
            return output.ToString();
        }      

        public async Task<string> SaveEmployee(Employee employee)
        {            
            var response = await client.PostAsJsonAsync("users", employee);
            var output = response.StatusCode;
            return output.ToString();
        }

        public async Task<string> UpdateEmployee(Employee employee)
        {
            var response = await client.PutAsJsonAsync("users/" + employee.id, employee);
            var output = response.StatusCode;
            return output.ToString();
        }
        
    }
}
