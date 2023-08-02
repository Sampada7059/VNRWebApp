using System;
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
    [Authorize(Roles = "Admin")]
    [RoutePrefix("Teacher")]
    public class TeacherController : Controller
    {

        Uri baseAddress = new Uri(ConfigurationManager.AppSettings["ApiLink"]); //Assigning API to baseAddress
        private readonly HttpClient _client;
        public TeacherController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }
        [Route]
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [Route]
        [HttpPost]
        public ActionResult Index(TeacherVM teacher)
        {
            if (ModelState.IsValid)
            {

                string data = JsonConvert.SerializeObject(teacher);//Serialization is the process of converting . NET objects, such as strings, into a JSON format
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "/Teacher/CreateTeacher", content).Result;//PostAsync-> sends Post request along with Api-link and content and returns StatusCode
                if (response.IsSuccessStatusCode)
                    TempData["SuccessMessage"] = "Teacher registered successfully."; //For SweetAlert Message
                return RedirectToAction("Home", "User");
            }
            else
            {
                return View(teacher);
            }
        }
    }
}
