using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Fault_Client_Dashboard.Models;
using System.Web.Mvc;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Fault_Client_Dashboard.Controllers
{
    public class HomeController : Controller
    {

        SdDatabaseEntities db = new SdDatabaseEntities();

        public ActionResult Index()
        {
            return RedirectToAction("FaultDashboard");
        }
        //Get: FaultTbl
        public ActionResult FaultDashboard()
        {
            IEnumerable<FaultTbl> fault = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://servicedeliveryapi.azurewebsites.net/");
                //HTTP GET
                var responseTask = client.GetAsync("api/Fault/ViewAllFault");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<FaultTbl>>();
                    readTask.Wait();

                    fault = readTask.Result;
                }
            }
            return View(fault);
        }

        public ActionResult AddFault()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddFault(FaultTbl fault)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://servicedeliveryapi.azurewebsites.net/");

                //HTTP POST
                var postTask = client.PostAsJsonAsync<FaultTbl>("api/Fault/AddFault", fault);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("FaultDashboard");
                }
            }

            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View(fault);
        }

        public ActionResult FaultDetails()
        {
            return View();
        }

        public ActionResult UserProfile()
        {
            AdminTbl admin = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://servicedeliveryapi.azurewebsites.net/");
                //HTTP GET
                var responseTask = client.GetAsync("API_CONTROLLER_NAME/");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<AdminTbl>();
                    readTask.Wait();

                    admin = readTask.Result;
                }
            }

            return View(admin);
        }
        [HttpPost]
        public ActionResult UserProfile(AdminTbl admin)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:64189/api/student");

                //HTTP POST
                var putTask = client.PutAsJsonAsync<AdminTbl>("API_CONTROLLER_NAME", admin);
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("UserProfile");
                }
            }
            return View(admin);
        }

        [HttpGet]
        public ActionResult EditFault(int id)
        {
            FaultTbl fault = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://servicedeliveryapi.azurewebsites.net/");
                //HTTP GET
                var responseTask = client.GetAsync("api/Fault/UpdateFault");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<FaultTbl>();
                    readTask.Wait();

                    fault = readTask.Result;
                }
            }

            return View(fault);
        }

        [HttpPost]
        public ActionResult EditFault(FaultTbl fault)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://servicedeliveryapi.azurewebsites.net/");

                //HTTP POST
                var putTask = client.PutAsJsonAsync<FaultTbl>("api/Fault/UpdateFault", fault);
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("FaultDashboard");
                }
            }
            return View(fault);
        }

  
        public ActionResult DeleteFault(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://servicedeliveryapi.azurewebsites.net/");

                //HTTP DELETE
                var deleteTask = client.DeleteAsync("api/Fault/DeleteFault");
                deleteTask.Wait();

                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("FaultDashboard");
                }
            }

            return RedirectToAction("FaultDashboard");
        }

        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Index");
        }

        //Get: FaultTbl
        public ActionResult ClientDashboard()
        {
            IEnumerable<ClientTbl> CView = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://servicedeliveryapi.azurewebsites.net/");
                //HTTP GET
                var responseTask = client.GetAsync("api/Client/GetAllClient");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<ClientTbl>>();
                    readTask.Wait();

                    CView = readTask.Result;
                }
            }
            return View(CView);
        }
       
        public ActionResult DeleteClient(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://servicedeliveryapi.azurewebsites.net/");

                //HTTP DELETE
                var deleteTask = client.DeleteAsync("api/Client/DeleteClient");
                deleteTask.Wait();

                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("ClientDashboard");
                }
            }

            return RedirectToAction("ClientDashboard");
        }
    }
}