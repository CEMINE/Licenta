﻿
using Licenta.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licenta.Services
{
    public class EmployeeServices
    {
        //private string baseUrl = "http://localhost:8080/api/Employee/";
        private string baseUrl { get; set; } = "http://localhost:8080/api/Employee/";
        
        public EmployeeServices()
        {
            this.baseUrl = DeviceInfo.Platform ==
            DevicePlatform.Android ? "http://192.168.1.6:8080/api/Employee/" : "http://localhost:8080/api/Employee/";      
        }
        
        public async Task<List<EmployeeModel>> GetAllEmployees()
        {
            try
            {
                List<EmployeeModel> employeeList = new List<EmployeeModel>();

                string fullUrl = this.baseUrl + "GetAllEmployees";

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

                    employeeList = JsonConvert.DeserializeObject<List<EmployeeModel>>(contentResponse);
                    //Debug.WriteLine("Conectiune realizata cu success!");
                }

                return await Task.FromResult(employeeList.ToList());
            }
            catch (Exception)
            {
                //Debug.WriteLine(e.Message);
                throw;
            }

        }

        public async Task<EmployeeModel> AddEmployee(EmployeeModel employee)
        {
            try
            {
                string fullUrl = this.baseUrl + "AddEmployee";
                string employeeInfoAsJson = JsonConvert.SerializeObject(employee);
                StringContent employeeStringContent = new StringContent(employeeInfoAsJson, Encoding.UTF8, "application/json");

                HttpClient httpsClient = new HttpClient();
                httpsClient.BaseAddress = new Uri(fullUrl);
                httpsClient.Timeout = TimeSpan.FromSeconds(30);
                HttpResponseMessage httpResponseMessage = await httpsClient.PostAsync("", employeeStringContent);

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    return await Task.FromResult(employee);

                }

                return await Task.FromResult(new EmployeeModel());
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<EmployeeModel> GetEmployeeById(int employeeId)
        {
            try
            {

                EmployeeModel currentEmployee = new EmployeeModel();

                string fullUrl = this.baseUrl + $"GetEmployeeById/{employeeId}";

                HttpClient httpClient = new HttpClient();

                httpClient.BaseAddress = new Uri(fullUrl);
                httpClient.Timeout = TimeSpan.FromSeconds(30);

                HttpResponseMessage httpResponseMessage = await httpClient.GetAsync("");

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    string contentRespone = await httpResponseMessage.Content.ReadAsStringAsync();

                    currentEmployee = JsonConvert.DeserializeObject<EmployeeModel>(contentRespone);
                }

                return await Task.FromResult(currentEmployee);
            }
            catch (Exception)
            {
                return null;
            }
        }


        public async Task<EmployeeModel> UpdateEmployee(EmployeeModel employeeInfo)
        {
            try
            {
                string fullUrl = this.baseUrl + $"EditEmployee/{employeeInfo.EmployeeID}";

                string employeeInfoAsJson = JsonConvert.SerializeObject(employeeInfo);

                StringContent employeeStringContent = new StringContent(employeeInfoAsJson, Encoding.UTF8, "application/json");


                HttpClient httpClient = new HttpClient();

                httpClient.BaseAddress = new Uri(fullUrl);
                httpClient.Timeout = TimeSpan.FromSeconds(30);

                HttpResponseMessage httpResponseMessage = await httpClient.PutAsync("", employeeStringContent);

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    return await Task.FromResult(employeeInfo);
                }

                return await Task.FromResult(new EmployeeModel());
            }
            catch (Exception)
            {
                return null;
            }
        }

        

        public async Task<bool> DeleteEmployee(EmployeeModel empInfo)
        {
            try
            {
                string fullUrl = this.baseUrl + $"DeleteEmployee/{empInfo.EmployeeID}";

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
