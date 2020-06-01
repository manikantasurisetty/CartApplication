using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using CardProjectBL;


namespace ProductCartWebApplication.Controllers
{
    public class HomeController : Controller
    {

    private EmployeeBL employeeBL=new EmployeeBL();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    public string DataTableToJSONWithJSONNet(DataTable table)
    {
      string JSONString = string.Empty;
      JSONString = JsonConvert.SerializeObject(table);
      return JSONString;
    }

    public string GetAllStudents()
    {
      var dataset = employeeBL.GetStudentBelow5();
      var jsonArray = this.DataTableToJSONWithJSONNet(dataset);
      return jsonArray;
    }
  }

}

