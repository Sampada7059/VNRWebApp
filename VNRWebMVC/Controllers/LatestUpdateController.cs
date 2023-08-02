using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;
using VNRWebMVC.ViewModels;

namespace VNRWebMVC.Controllers
{
    //Only specified user can access this controller
    [Authorize(Roles = "Admin,Teacher,Student")]
    [RoutePrefix("LatestUpdate")]
    public class LatestUpdateController : Controller
    {
        Uri baseAddress = new Uri(ConfigurationManager.AppSettings["ApiLink"]); //API link Assigning to baseAddress variable
        private readonly HttpClient _client;
        public LatestUpdateController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }
        [Route]
        [HttpGet]
        public ActionResult DisplayUpdates()
        {
            List<LatestUpdateVM> updates = new List<LatestUpdateVM>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/LatestUpdate/GetAllUpdates").Result;//GetAsync-> sends Get request with Api-link and returns StatusCode
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                updates = JsonConvert.DeserializeObject<List<LatestUpdateVM>>(data);//deserialization is the process of converting JSON data into . NET objects
            }
            return View(updates);
        }
        [Route]
        [HttpPost]
        public ActionResult DisplayUpdates(LatestUpdateVM latestUpdate)
        {
            string data = JsonConvert.SerializeObject(latestUpdate);//Serialization is the process of converting . NET objects, such as strings, into a JSON format
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "/LatestUpdate/AddLatestUpdate", content).Result; //PostAsync-> sends Post request along with Api-link and content and returns StatusCode
            if (response.IsSuccessStatusCode)
                return RedirectToAction("DisplayUpdates");
            return View();
        }
    }
}