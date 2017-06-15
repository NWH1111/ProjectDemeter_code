using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
 
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ProjectD.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

 

        public IActionResult iMap()
        {

            return View();
        }


        public IActionResult Client_Template()
        {
            ViewData["Message"] = "Your iSHOP.";

            return View();
        }

        public IActionResult iShop()
        {
            ViewData["Message"] = "Your iSHOP.";

            return View();
        }

        [HttpPost]
        public IActionResult iShop(String TEST, String TEST2)
        {
            var name = TEST;
            name = "2";
            var name2 = TEST2;
            string connectionString = "Server=tcp:demeterdb.database.windows.net,1433;Initial Catalog=ProjectD;Persist Security Info=False;User ID=Demeter_DB;Password=1qaz@WSX;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
                string sqlCommand = @"INSERT INTO member_vip (member_id, member_name) VALUES ('"+name+"', '"+name2+"')";

                System.Text.StringBuilder SB = new System.Text.StringBuilder();
                SB.AppendLine("{");
                SB.AppendLine("\"values\":[");

                using (System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection(connectionString))
                {
                    connection.Open();

                    using (System.Data.SqlClient.SqlCommand command = new System.Data.SqlClient.SqlCommand(sqlCommand, connection))
                    {
                        using (System.Data.SqlClient.SqlDataReader reader = command.ExecuteReader())
                        {
                            bool gotData = reader.Read();
                            do
                            {
                                if (gotData)
                                {                               

                                }
                                gotData = reader.Read();
                                if (gotData)
                                    SB.AppendLine(",");
                            } while (gotData);
                        }
                    }
                }

                SB.AppendLine("]");
                SB.AppendLine("}");

                string result = SB.ToString();

            result = result;
            ViewBag.RESULT = "No";
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }


        public ActionResult Login()
        {
            return View();
        }


 

    }
}
