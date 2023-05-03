using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Windows;
using System.Net.Http.Headers;
using CaseStudyService.Models;
using CaseStudyService.Interface;
using System.Threading.Tasks;
using System.Windows.Data;

namespace TCSCaseStudy
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IEmployeeService _service;
        HttpClient client = new HttpClient(); 
        public MainWindow(IEmployeeService service)
        {
            string authorizationtoken = "fa114107311259f5f33e70a5d85de34a2499b4401da069af0b1d835cd5ec0d56";
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authorizationtoken);
            client.BaseAddress = new Uri("https://gorest.co.in/public-api/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _service = service;
            InitializeComponent();            
        }

        private void btnLoadEmployees_Click(object sender, RoutedEventArgs e)
        {
            GetEmployees();
        }

        private async void GetEmployees()
        {
            var response = await client.GetStringAsync("users");
            wrapperEmployee employees = JsonConvert.DeserializeObject<wrapperEmployee>(response);
            dgEmployee.DataContext = employees.data;
        }

        private void SaveEmployee(Employee employee)
        {
            var response =_service.SaveEmployee(employee);           
            string messageBoxText = "User added successfully";
            string caption = "Message";                
            GetEmployees();
            CollectionViewSource.GetDefaultView(dgEmployee.ItemsSource).Refresh();
            MessageBox.Show(messageBoxText, caption);
           
           
        }

        private void UpdateEmployee(Employee emp)
        {
            var response = _service.UpdateEmployee(emp);           
            string messageBoxText = "User updated successfully";
            string caption = "Message";
            MessageBox.Show(messageBoxText, caption);            
            GetEmployees();
        }
        private void DeleteEmployee(int empid)
        {
            var response = _service.DeleteEmployee(empid);           
            string messageBoxText = "User removed successfully";
            string caption = "Message";
            MessageBox.Show(messageBoxText, caption);           
            GetEmployees();
        }

        private void btnSaveEmployee_Click(object sender, RoutedEventArgs e)
        {
            var employee = new Employee()
            {
                id = Convert.ToInt32(txtEmployeeId.Text),
                name = txtName.Text.ToString(),
                email = txtEmail.Text.ToString(),
                gender = txtGender.Text.ToString(),
                status = txtStatus.Text.ToString()
            };
            if(employee.id == 0)
            {
                SaveEmployee(employee);
                GetEmployees();
            }
            else
            {
                UpdateEmployee(employee);
                GetEmployees();
            }

            txtEmployeeId.Text = 0.ToString();
            txtName.Text = "";
            txtEmail.Text = "";
            txtGender.Text = "";
            txtStatus.Text = "";
            GetEmployees();
            CollectionViewSource.GetDefaultView(dgEmployee.ItemsSource).Refresh();
        }

        void btnEditEmployee(object sender, RoutedEventArgs e)
        {
            Employee employee = ((FrameworkElement)sender).DataContext as Employee;
            txtEmployeeId.Text = employee.id.ToString();
            txtName.Text = employee.name;
            txtEmail.Text = employee.email;
            txtGender.Text = employee.gender;
            txtStatus.Text = employee.status;
        }

        void btnDeleteEmployee(object sender, RoutedEventArgs e)
        {
            Employee employee = ((FrameworkElement)sender).DataContext as Employee;
            this.DeleteEmployee(employee.id);
            GetEmployees();
            CollectionViewSource.GetDefaultView(dgEmployee.ItemsSource).Refresh();
        }
    }
}
