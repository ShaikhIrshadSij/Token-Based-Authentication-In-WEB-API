using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using TokenBasedAuthenticationWebAPI.POCO.Entity;

namespace TokenBasedAuthenticationWebAPI.Controllers
{
    [Authorize]
    public class HomeController : ApiController
    {
        private readonly APIContext _apiContext = null;
        public HomeController()
        {
            _apiContext = new APIContext();
        }

        [Route("api/home/GetEmployees")]
        [HttpGet]
        public IHttpActionResult GetEmployees()
        {
            try
            {
                var allEmployees = _apiContext.Employees.Select(x => new
                {
                    x.EmployeeId,
                    x.EmployeeName,
                    x.Address,
                    x.PhoneNumber
                }).ToList();

                return Ok(new
                {
                    isSuccess = true,
                    allEmployees = JsonConvert.SerializeObject(allEmployees)
                });
            }
            catch (Exception ex)
            {
                return Ok(new
                {
                    isSuccess = false
                });
            }
        }

        [Route("api/home/GetEmployeesById")]
        [HttpGet]
        public IHttpActionResult GetEmployeesById(int empId)
        {
            try
            {
                var isEmployeeExist = _apiContext.Employees.FirstOrDefault(x => x.EmployeeId == empId);

                return Ok(new
                {
                    isSuccess = true,
                    data = isEmployeeExist
                });
            }
            catch (Exception ex)
            {
                return Ok(new
                {
                    isSuccess = false
                });
            }
        }

        [Route("api/home/AddEditEmpolyees")]
        [HttpPost]
        public IHttpActionResult AddEditEmpolyees()
        {
            try
            {
                HttpRequest httpRequest = HttpContext.Current.Request;
                var form = httpRequest.Form;
                var empObj = JsonConvert.DeserializeObject<Employee>(form["empObj"]);
                var isEmployeeExist = _apiContext.Employees.FirstOrDefault(x => x.EmployeeId == empObj.EmployeeId);
                if (isEmployeeExist == null)
                {
                    isEmployeeExist = new Employee
                    {
                        Address = empObj.Address,
                        EmployeeName = empObj.EmployeeName,
                        PhoneNumber = empObj.PhoneNumber
                    };
                    _apiContext.Employees.Add(isEmployeeExist);
                }
                else
                {
                    isEmployeeExist.Address = empObj.Address;
                    isEmployeeExist.EmployeeName = empObj.EmployeeName;
                    isEmployeeExist.PhoneNumber = empObj.PhoneNumber;
                }
                _apiContext.SaveChanges();
                return Ok(new
                {
                    isSuccess = true
                });
            }
            catch (Exception ex)
            {
                return Ok(new
                {
                    isSuccess = false
                });
            }
        }

        [Route("api/home/DeleteEmployeesById")]
        [HttpGet]
        public IHttpActionResult DeleteEmployeesById(int empId)
        {
            try
            {
                var isEmployeeExist = _apiContext.Employees.FirstOrDefault(x => x.EmployeeId == empId);
                if (isEmployeeExist != null)
                {
                    _apiContext.Employees.Remove(isEmployeeExist);
                    _apiContext.SaveChanges();
                }
                return Ok(new
                {
                    isSuccess = true,
                    data = isEmployeeExist
                });
            }
            catch (Exception ex)
            {
                return Ok(new
                {
                    isSuccess = false
                });
            }
        }
    }
}
