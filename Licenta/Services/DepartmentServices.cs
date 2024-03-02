using Licenta.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Newtonsoft.Json;

namespace Licenta.Services
{
    public class DepartmentServices
    {
        //private string baseUrl = "http://localhost:8080/api/Employee/";
        private string baseUrl { get; set; } = "http://localhost:8080/api/Department/";

        public DepartmentServices()
        {
            this.baseUrl = DeviceInfo.Platform ==
            DevicePlatform.Android ? "http://192.168.1.6:8080/api/Department/" : "http://localhost:8080/api/Department/";
        }

        public async Task<List<DepartmentModel>> GetAllDepartments()
        {
            try
            {
                List<DepartmentModel> departmentList = new List<DepartmentModel>();

                string fullUrl = this.baseUrl + "GetAllDepartments";

                HttpClient httpClient = new HttpClient();
                //Debug.WriteLine("baseurl" + baseUrl);
                //Debug.WriteLine("Full url " + fullUrl);
                //Debug.WriteLine($"platforma: {DeviceInfo.Platform}");
                httpClient.BaseAddress = new Uri(fullUrl);
                httpClient.Timeout = TimeSpan.FromSeconds(30);

                HttpResponseMessage httpResponseMessage = await httpClient.GetAsync("");

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    string contentResponse = await httpResponseMessage.Content.ReadAsStringAsync();

                    departmentList = JsonConvert.DeserializeObject<List<DepartmentModel>>(contentResponse);
                    //Debug.WriteLine("Conectiune realizata cu success!");
                }

                return await Task.FromResult(departmentList.ToList());
            }
            catch (Exception)
            {
                //Debug.WriteLine(e.Message);
                throw;
            }

        }

		public async Task<DepartmentModel> AddDepartment(DepartmentModel departmentModel)
		{
			try
			{
				string fullUrl = this.baseUrl + "AddDepartment";
				string depInfoAsJson = JsonConvert.SerializeObject(departmentModel);
				StringContent depStringContent = new StringContent(depInfoAsJson, Encoding.UTF8, "application/json");

				HttpClient httpsClient = new HttpClient();
				httpsClient.BaseAddress = new Uri(fullUrl);
				httpsClient.Timeout = TimeSpan.FromSeconds(30);
				HttpResponseMessage httpResponseMessage = await httpsClient.PostAsync("", depStringContent);

				if (httpResponseMessage.IsSuccessStatusCode)
				{
					return await Task.FromResult(departmentModel);

				}

				return await Task.FromResult(new DepartmentModel());
			}
			catch (Exception)
			{

				throw;
			}
		}

		public async Task<DepartmentModel> UpdateDepartment(DepartmentModel departmentModel)
		{
			try
			{
				string fullUrl = this.baseUrl + $"EditDepartment/{departmentModel.DepartmentID}";

				string depInfoAsJson = JsonConvert.SerializeObject(departmentModel);

				StringContent employeeStringContent = new StringContent(depInfoAsJson, Encoding.UTF8, "application/json");


				HttpClient httpClient = new HttpClient();

				httpClient.BaseAddress = new Uri(fullUrl);
				httpClient.Timeout = TimeSpan.FromSeconds(30);

				HttpResponseMessage httpResponseMessage = await httpClient.PutAsync("", employeeStringContent);

				if (httpResponseMessage.IsSuccessStatusCode)
				{
					return await Task.FromResult(departmentModel);
				}

				return await Task.FromResult(new DepartmentModel());
			}
			catch (Exception)
			{
				return null;
			}
		}

		public async Task<bool> DeleteDepartment(DepartmentModel departmentModel)
		{
			try
			{
				string fullUrl = this.baseUrl + $"Delete/{departmentModel.DepartmentID}";

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
