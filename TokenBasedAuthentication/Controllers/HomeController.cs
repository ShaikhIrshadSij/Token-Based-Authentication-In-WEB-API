using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TokenBasedAuthentication.Helper;

namespace TokenBasedAuthentication.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public async Task<object> GetEmployees()
        {
            object str = await APIServices.Get<object>("/api/home/GetEmployees");
            return str;
        }

        public async Task<object> GetEmployeesById(int empId)
        {
            object str = await APIServices.Get<object>($"/api/home/GetEmployeesById?empId={empId}");
            return str;
        }

        public async Task<object> DeleteEmployeesById(int empId)
        {
            object str = await APIServices.Get<object>($"/api/home/DeleteEmployeesById?empId={empId}");
            return str;
        }

        [HttpPost]
        public async Task<object> AddEditEmpolyees(string empObj)
        {
            MultipartFormDataContent multiForm = new MultipartFormDataContent
            {
                { new StringContent(empObj??""), "empObj" }
            };
            object str = await APIServices.Post<object>($"/api/home/AddEditEmpolyees", multiForm);
            return str;
        }
    }
}