using Licenta.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.Maui.ApplicationModel.Communication;
using System.Diagnostics;

namespace Licenta.Services
{
    public class EmployeeProjectServices
    {
        //private string baseUrl = "http://localhost:8080/api/EmployeeProject/";
        private string baseUrl { get; set; } = "http://localhost:8080/api/EmployeeProject/";

        public EmployeeProjectServices()
        {
            this.baseUrl = DeviceInfo.Platform ==
            DevicePlatform.Android ? "http://192.168.1.6:8080/api/EmployeeProject/" : "http://localhost:8080/api/EmployeeProject/";
        }

        public async Task<List<EmployeeProjectModel>> GetAllEmployeesProjects()
        {
            try
            {
                List<EmployeeProjectModel> employeeProjectsList = new List<EmployeeProjectModel>();

                string fullUrl = this.baseUrl + "GetAllEmployeesProjects";

                HttpClient httpClient = new HttpClient();
                //Debug.WriteLine("baseurl" + baseUrl);
                //Debug.WriteLine("Full url " + fullUrl);
                //Debug.WriteLine($"platforma: {DeviceInfo.Platform}" );
                httpClient.BaseAddress = new Uri(fullUrl);
                httpClient.Timeout = TimeSpan.FromSeconds(30);

                HttpResponseMessage httpResponseMessage = await httpClient.GetAsync("");

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    string contentResponse = await httpResponseMessage.Content.ReadAsStringAsync();

                    employeeProjectsList = JsonConvert.DeserializeObject<List<EmployeeProjectModel>>(contentResponse);
                    //Debug.WriteLine("Conectiune realizata cu success!");
                }

                return await Task.FromResult(employeeProjectsList.ToList());
            }
            catch (Exception)
            {
                //Debug.WriteLine(e.Message);
                throw;
            }

        }

        public async Task<EmployeeProjectModel> AddEmployeeProject(EmployeeProjectModel employee)
        {
            try
            {
                string fullUrl = this.baseUrl + "AddProjectToEmployee";
                string employeeInfoAsJson = JsonConvert.SerializeObject(employee);
                StringContent employeeStringContent = new StringContent(employeeInfoAsJson, Encoding.UTF8, "application/json");

                HttpClient httpsClient = new HttpClient();
                httpsClient.BaseAddress = new Uri(fullUrl);
                httpsClient.Timeout = TimeSpan.FromSeconds(30);
                HttpResponseMessage httpResponseMessage = await httpsClient.PostAsync("", employeeStringContent);

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    Debug.WriteLine(httpResponseMessage.Content);
                    return await Task.FromResult(employee);
                }

                return await Task.FromResult(new EmployeeProjectModel());
            }
            catch (Exception e)
            {
                Debug.WriteLine($"exceptie addemployeeproject: {e.Message}");
                throw;
            }
        }

        public async Task<List<EmployeeProjectModel>> GetEmployeeProjectsById(int employeeId)
        {
            try
            {
                List<EmployeeProjectModel> employeeProjectsList = new List<EmployeeProjectModel>();

                string fullUrl = this.baseUrl + $"GetEmployeeProjectsById/{employeeId}";

                HttpClient httpClient = new HttpClient();
                //Debug.WriteLine("baseurl" + baseUrl);
                //Debug.WriteLine("Full url " + fullUrl);
                //Debug.WriteLine($"platforma: {DeviceInfo.Platform}" );
                httpClient.BaseAddress = new Uri(fullUrl);
                httpClient.Timeout = TimeSpan.FromSeconds(30);

                HttpResponseMessage httpResponseMessage = await httpClient.GetAsync("");

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    string contentResponse = await httpResponseMessage.Content.ReadAsStringAsync();

                    employeeProjectsList = JsonConvert.DeserializeObject<List<EmployeeProjectModel>>(contentResponse);
                    //Debug.WriteLine("Conectiune realizata cu success!");
                }

                return await Task.FromResult(employeeProjectsList.ToList());
            }
            catch (Exception)
            {
                throw;
            }
        }
        /*
        public async Task<EmployeeProjectModel> UpdateEmployeeProject(int oldEmpId, int oldProjectId, EmployeeProjectModel employeeInfo)
        {
            try
            {
                /*
                string fullUrl = this.baseUrl + $"EditEmployeeProject/{empId}/{projectId}";

                string employeeInfoAsJson = JsonConvert.SerializeObject(employeeInfo);

                StringContent employeeStringContent = new StringContent(employeeInfoAsJson, Encoding.UTF8, "application/json");


                HttpClient httpClient = new HttpClient();

                httpClient.BaseAddress = new Uri(fullUrl);
                httpClient.Timeout = TimeSpan.FromSeconds(30);

                HttpResponseMessage httpResponseMessage = await httpClient.DeleteAsync("");

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    return await Task.FromResult(employeeInfo);
                }
                
                await this.DeleteEmployeeProject(oldEmpId, oldProjectId);
                Debug.WriteLine($"Inregistrarea cu emp id: {oldEmpId} si project id: {oldProjectId} a fost stearsa");
                await this.AddEmployeeProject(employeeInfo);
                Debug.WriteLine($"Inregistrarea cu emp id: {employeeInfo.EmployeeId} si project id: {employeeInfo.ProjectId} a fost adaugata");

                return await Task.FromResult(new EmployeeProjectModel());
            }
            catch (Exception e)
            {
                Debug.WriteLine($"{e.Message}");
                return null;
            }
        }
        */

        public async Task<bool> UpdateEmployeeProject(int employeeId, int oldProjectId, EmployeeProjectModel epm)
        {
            try
            {

                string fullUrl = this.baseUrl + $"EditEmployeeProject/{employeeId}/{oldProjectId}";

                string employeeProjectAsJson = JsonConvert.SerializeObject(epm);

                StringContent employeeProjectStringContent = new StringContent(employeeProjectAsJson, Encoding.UTF8, "application/json");

                using (HttpClient httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri(fullUrl);
                    httpClient.Timeout = TimeSpan.FromSeconds(30);

                    HttpResponseMessage httpResponseMessage = await httpClient.PutAsync("", employeeProjectStringContent);

                    return httpResponseMessage.IsSuccessStatusCode;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }


        public async Task<bool> DeleteEmployeeProject(int empId, int projectId)
        {
            try
            {
                string fullUrl = this.baseUrl + $"DeleteProjectEmployee/{empId}/{projectId}";

                HttpClient httpClient = new HttpClient();

                httpClient.BaseAddress = new Uri(fullUrl);
                httpClient.Timeout = TimeSpan.FromSeconds(30);

                HttpResponseMessage httpResponseMessage = await httpClient.DeleteAsync("");

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    return await Task.FromResult(true);
                }

                return await Task.FromResult(false);
            }
            catch (Exception)
            {
                return false;
            }
        }


    }
}
