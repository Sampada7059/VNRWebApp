
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Newtonsoft.Json;
using VNRWebMVC.ViewModels;
namespace VNRWebMVC.Controllers
{
    [Authorize(Roles = "Admin,Teacher")]
    [RoutePrefix("Student")]
    public class StudentController : Controller
    {
        [Route]
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        Uri baseAddress = new Uri(ConfigurationManager.AppSettings["ApiLink"]);
        private readonly HttpClient _client;
        public StudentController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }

        [HttpPost]
        public ActionResult Index(StudentVM student)
        {
            if (ModelState.IsValid)
            {
                string data = JsonConvert.SerializeObject(student);//Serialization is the process of converting . NET objects, such as strings, into a JSON format
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "/Student/CreateStudent", content).Result;//PostAsync-> sends Post request along with Api-link and content and returns StatusCode
                if (response.IsSuccessStatusCode)
                    TempData["SuccessMessage"] = "Student registered successfully."; //for SweetAlert Message
                return RedirectToAction("Home", "User");
            }
            else
            {
                return View(student);
            }
        }

    }
}
