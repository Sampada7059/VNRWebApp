using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using VNRWebMVC.Models;
using VNRWebMVC.ViewModels;

namespace VNRWebMVC.Controllers
{
    public class UserController : Controller
    {
        [Authorize(Roles = "Admin,Teacher,Student")]
        [Route("Home")]
        public ActionResult Home()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        Uri baseAddress = new Uri(ConfigurationManager.AppSettings["ApiLink"]);//API link Assigning to baseAddress variable
        private readonly HttpClient _client;
        public UserController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }
        [HttpPost]
        public ActionResult Login(Models.Users user)
        {
            if (ModelState.IsValid)
            {
                string data = JsonConvert.SerializeObject(user);//Serialization is the process of converting . NET objects, such as strings, into a JSON format
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "/Users/Login", content).Result; //PostAsync-> sends Post request along with Api-link and content and returns StatusCode
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = response.Content.ReadAsStringAsync().Result; //reading return content 
                    var responseObject = JsonConvert.DeserializeObject<dynamic>(responseContent); //deserialization is the process of converting JSON data into . NET objects
                    string nameOfUser = responseObject.nameOfUser;
                    string userType = responseObject.userType;
                    string userName = responseObject.userName;
                    string Id = responseObject.userId;
                    Session["userID"] = Id; //for Latest Update
                    Session["nameOfUser"] = nameOfUser; //for Display the User Name
                    Session["UserType"] = userType; //for Role base authentication
                    Session["userName"] = userName; //for Role base authentication
                    FormsAuthentication.SetAuthCookie(user.Username, false); //redirect to Web.config->for Role base authentication
                    return RedirectToAction("Home");
                }
                return View();
            }
            else
            {
                return View();
            }
        }
        //Logout Method
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

    }
}