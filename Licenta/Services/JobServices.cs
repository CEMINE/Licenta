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
    public class JobServices
    {

        //private string baseUrl = "http://localhost:8080/api/Employee/";
        private string baseUrl { get; set; } = "http://localhost:8080/api/Jobs/";

        public JobServices()
        {
            this.baseUrl = DeviceInfo.Platform ==
            DevicePlatform.Android ? "http://192.168.1.6:8080/api/Jobs/" : "http://localhost:8080/api/Jobs/";
        }

        public async Task<List<JobModel>> GetAllJobs()
        {
            try
            {
                List<JobModel> jobList = new List<JobModel>();

                string fullUrl = this.baseUrl + "GetAllJobs";

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

                    jobList = JsonConvert.DeserializeObject<List<JobModel>>(contentResponse);
                    //Debug.WriteLine("Conectiune realizata cu success!");
                }

                return await Task.FromResult(jobList.ToList());
            }
            catch (Exception)
            {
                //Debug.WriteLine(e.Message);
                throw;
            }

        }

		public async Task<JobModel> GetJobById(int jobId)
		{
			try
			{

				JobModel currentJob = new JobModel();

				string fullUrl = this.baseUrl + $"GetJobById/{jobId}";

				HttpClient httpClient = new HttpClient();

				httpClient.BaseAddress = new Uri(fullUrl);
				httpClient.Timeout = TimeSpan.FromSeconds(30);

				HttpResponseMessage httpResponseMessage = await httpClient.GetAsync("");

				if (httpResponseMessage.IsSuccessStatusCode)
				{
					string contentRespone = await httpResponseMessage.Content.ReadAsStringAsync();

					currentJob = JsonConvert.DeserializeObject<JobModel>(contentRespone);
				}

				return await Task.FromResult(currentJob);
			}
			catch (Exception)
			{
				return null;
			}
		}

		public async Task<JobModel> AddJob(JobModel jobModel)
		{
			try
			{
				string fullUrl = this.baseUrl + "AddJob";
				string jobInfoAsJson = JsonConvert.SerializeObject(jobModel);
				StringContent jobStringContent = new StringContent(jobInfoAsJson, Encoding.UTF8, "application/json");

				HttpClient httpsClient = new HttpClient();
				httpsClient.BaseAddress = new Uri(fullUrl);
				httpsClient.Timeout = TimeSpan.FromSeconds(30);
				HttpResponseMessage httpResponseMessage = await httpsClient.PostAsync("", jobStringContent);

				if (httpResponseMessage.IsSuccessStatusCode)
				{
					return await Task.FromResult(jobModel);

				}

				return await Task.FromResult(new JobModel());
			}
			catch (Exception)
			{

				throw;
			}
		}

		public async Task<JobModel> UpdateJob(JobModel jobModel)
		{
			try
			{
				string fullUrl = this.baseUrl + $"EditJob/{jobModel.JobID}";

				string jobInfoAsJson = JsonConvert.SerializeObject(jobModel);

				StringContent employeeStringContent = new StringContent(jobInfoAsJson, Encoding.UTF8, "application/json");


				HttpClient httpClient = new HttpClient();

				httpClient.BaseAddress = new Uri(fullUrl);
				httpClient.Timeout = TimeSpan.FromSeconds(30);

				HttpResponseMessage httpResponseMessage = await httpClient.PutAsync("", employeeStringContent);

				if (httpResponseMessage.IsSuccessStatusCode)
				{
					return await Task.FromResult(jobModel);
				}

				return await Task.FromResult(new JobModel());
			}
			catch (Exception)
			{
				return null;
			}
		}

		public async Task<bool> DeleteJob(JobModel jobModel)
		{
			try
			{
				string fullUrl = this.baseUrl + $"DeleteJob/{jobModel.JobID}";

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
