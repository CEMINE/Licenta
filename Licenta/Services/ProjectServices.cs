using Licenta.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licenta.Services
{
    public class ProjectServices
    {
        //private string baseUrl = "http://localhost:8080/api/Project/";
        private string baseUrl { get; set; } = "http://localhost:8080/api/Project/";

        public ProjectServices()
        {
            this.baseUrl = DeviceInfo.Platform ==
            DevicePlatform.Android ? "http://192.168.1.6:8080/api/Project/" : "http://localhost:8080/api/Project/";
        }

        public async Task<List<ProjectModel>> GetAllProjects()
        {
            try
            {
                List<ProjectModel> projectList = new List<ProjectModel>();

                string fullUrl = this.baseUrl + "GetAllProjects";

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

                    projectList = JsonConvert.DeserializeObject<List<ProjectModel>>(contentResponse);
                    //Debug.WriteLine("Conectiune realizata cu success!");
                }

                return await Task.FromResult(projectList.ToList());
            }
            catch (Exception)
            {
                //Debug.WriteLine(e.Message);
                throw;
            }

        }

        public async Task<ProjectModel> AddProject(ProjectModel project)
        {
            try
            {
                string fullUrl = this.baseUrl + "AddProject";
                string projectInfoAsJson = JsonConvert.SerializeObject(project);
                StringContent projectStringContent = new StringContent(projectInfoAsJson, Encoding.UTF8, "application/json");

                HttpClient httpsClient = new HttpClient();
                httpsClient.BaseAddress = new Uri(fullUrl);
                httpsClient.Timeout = TimeSpan.FromSeconds(30);
                HttpResponseMessage httpResponseMessage = await httpsClient.PostAsync("", projectStringContent);

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    return await Task.FromResult(project);

                }

                return await Task.FromResult(new ProjectModel());
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<ProjectModel> GetProjectById(int projectId)
        {
            try
            {

                ProjectModel currentProject = new ProjectModel();

                string fullUrl = this.baseUrl + $"GetProjectById/{projectId}";

                HttpClient httpClient = new HttpClient();

                httpClient.BaseAddress = new Uri(fullUrl);
                httpClient.Timeout = TimeSpan.FromSeconds(30);

                HttpResponseMessage httpResponseMessage = await httpClient.GetAsync("");

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    string contentRespone = await httpResponseMessage.Content.ReadAsStringAsync();

                    currentProject = JsonConvert.DeserializeObject<ProjectModel>(contentRespone);
                }

                return await Task.FromResult(currentProject);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<ProjectModel> UpdateProject(ProjectModel projectInfo)
        {
            try
            {
                string fullUrl = this.baseUrl + $"EditProject/{projectInfo.ProjectID}";

                string projectInfoAsJson = JsonConvert.SerializeObject(projectInfo);

                StringContent employeeStringContent = new StringContent(projectInfoAsJson, Encoding.UTF8, "application/json");


                HttpClient httpClient = new HttpClient();

                httpClient.BaseAddress = new Uri(fullUrl);
                httpClient.Timeout = TimeSpan.FromSeconds(30);

                HttpResponseMessage httpResponseMessage = await httpClient.PutAsync("", employeeStringContent);

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    return await Task.FromResult(projectInfo);
                }

                return await Task.FromResult(new ProjectModel());
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> DeleteProject(ProjectModel projectInfo)
        {
            try
            {
                string fullUrl = this.baseUrl + $"EditProject/ {projectInfo.ProjectID}";

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
