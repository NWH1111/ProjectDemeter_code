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
        public IActionResult iShop(String member_account, String member_password,string member_name,string member_birth, string group_gender, string[] checkbooox)
        {          
            for (int i = 0; i < checkbooox.Length; i++)
            {
                var check = checkbooox[i];
                string sqlCommand = @"
                                INSERT INTO member_pitem
                                VALUES ('1', '{0}')";
                System.Text.StringBuilder SB = new System.Text.StringBuilder();
                using (System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection(connectionString))
                {
                    connection.Open();

                    using (System.Data.SqlClient.SqlCommand command = new System.Data.SqlClient.SqlCommand(string.Format(sqlCommand, check), connection))
                    {
                        command.ExecuteNonQuery();
                    }
                    connection.Close();
                }               
            }
            return View("Client_Template");
        }

        [HttpPost]
        public string DoQuery_iplaint()
        {
            string input_id = Request.Form["id"]; ;
            string sqlCommand = "SELECT * FROM iplaint";
            System.Text.StringBuilder SB = new System.Text.StringBuilder();
            using (System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection(connectionString))
            {
                connection.Open();

                using (System.Data.SqlClient.SqlCommand command = new System.Data.SqlClient.SqlCommand(string.Format(sqlCommand, input_id), connection))
                {
                    using (System.Data.SqlClient.SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string plaint_id = reader.GetString(0);
                            string name = reader.GetString(1);
                            string info = reader.GetString(2);
                            string steps = reader.GetString(3);
                            string img = reader.GetString(4);
                            SB.AppendLine("<li class=\"media\">");
                            SB.AppendLine("<a class=\"media-left\">");
                            SB.AppendLine(string.Format("<img class=\"media-object\" src =\"{0}\" style=\"width: 140px; height: 140px\">", img));
                            SB.AppendLine("</a>");
                            SB.AppendLine("<div class=\"media-body\">");
                            SB.AppendLine(string.Format("<h2 class=\"media-heading\">{0}</h2>", name));
                            SB.AppendLine(string.Format("<h3 class=\"media-heading\">{0}</h3>", info));
                            SB.AppendLine(string.Format("<h4 class=\"media-heading\">{0}</h4>", steps));
                            SB.AppendLine("<label data-pg-collapsed>");
                            SB.AppendLine(string.Format("<input class=\"control-label\" name=\"checkbooox\" type=\"checkbox\" value=\"{0}\">", plaint_id));
                            SB.AppendLine("+入有興趣清單");
                            SB.AppendLine("</label>");
                            SB.AppendLine("</li>");
                        }
                    }
                }
                connection.Close();
            }
            Console.WriteLine(SB.ToString());
            return SB.ToString();
        }

        public IActionResult Error()
        {
            return View();
        }


        // SEAN add v2017.06.18

        [HttpPost]
        public void WriteGoBuyToDB()
        {
            try
            {
                string buyer_message_id = Request.Form["member_id_"];
                string buyer_selling_item_name = Request.Form["selling_item_name"];

                //
                string connectionString = "Server=tcp:demeterdb.database.windows.net,1433;Initial Catalog=ProjectD;Persist Security Info=False;User ID=Demeter_DB;Password=1qaz@WSX;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";


                string sqlCommand = @"
                                  UPDATE ishop
                                  SET is_to_sell = {0}
                                  WHERE member_id = {1} AND selling_item_name = '{2}'
                                ";
                System.Text.StringBuilder SB = new System.Text.StringBuilder();

                using (System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection(connectionString))
                {
                    connection.Open();

                    string sqlSendCommend = string.Format(sqlCommand, 1, buyer_message_id, buyer_selling_item_name);

                    using (System.Data.SqlClient.SqlCommand command = new System.Data.SqlClient.SqlCommand(sqlSendCommend, connection))
                    {
                        command.ExecuteNonQuery();
                    }

                    //
                }
            }
            catch (Exception eee)
            {
                string msg = eee.Message;
                msg = msg;
            }

        }


        string connectionString = "Server=tcp:demeterdb.database.windows.net,1433;Initial Catalog=ProjectD;Persist Security Info=False;User ID=Demeter_DB;Password=1qaz@WSX;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        [HttpPost]
        public string TalkTalk_Post()
        {
            string input_member_id = Request.Form["member_id"];
            string input_selling_item_name = Request.Form["selling_item_name"];
            string input_message = Request.Form["message"];

            string sqlCommand = @"
                                INSERT INTO ishop_message
                                VALUES ('{0}', '{1}', '1', '', '{2}', GETDATE())";


            using (System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection(connectionString))
            {
                connection.Open();

                using (System.Data.SqlClient.SqlCommand command = new System.Data.SqlClient.SqlCommand(string.Format(sqlCommand, input_member_id, input_selling_item_name, input_message), connection))
                {
                    command.ExecuteNonQuery();
                }
            }

            return "";
        }

        [HttpPost]
        public string TalkTalk_Get()
        {
            string input_member_id = Request.Form["member_id"];
            string input_selling_item_name = Request.Form["selling_item_name"];


            string sqlCommand = @"
SELECT msg.*, vip.member_img
FROM ishop_message msg
LEFT JOIN member_vip vip
ON msg.member_id = vip.member_id
WHERE msg.member_id = '{0}' AND selling_item_name = '{1}'
ORDER BY datetime";

            System.Text.StringBuilder SB = new System.Text.StringBuilder();
            using (System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection(connectionString))
            {
                connection.Open();

                using (System.Data.SqlClient.SqlCommand command = new System.Data.SqlClient.SqlCommand(string.Format(sqlCommand, input_member_id, input_selling_item_name), connection))
                {
                    using (System.Data.SqlClient.SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string member_id = reader.GetString(0);
                            string selling_item_name = reader.GetString(1);
                            string buyer_id = reader.GetString(2);
                            string seller_id = reader.GetString(3);
                            string message = reader.GetString(4);
                            string member_img = reader.GetString(6);
                            //
                            if (!string.IsNullOrEmpty(buyer_id) && string.IsNullOrEmpty(seller_id))
                            {

                                SB.AppendLine("<div class=\"talk-bubble tri-right border round btm-left-in\">");
                                SB.AppendLine(string.Format("<img class=\"media-object img-circle\" style=\"width: 50px; height: 50px\" src =https://scontent-tpe1-1.xx.fbcdn.net/v/t1.0-1/p240x240/983877_10203227876014695_3420118522412449375_n.jpg?oh=2bc1f85ef432167575d9ae9f9ed6c0b6&oe=59D4B4BE alt=\"...\">"));
                                SB.AppendLine("<div class=\"talktext\">");

                                SB.AppendLine(string.Format("<p>{0}</p>", message));
                                SB.AppendLine("</div>");
                                SB.AppendLine("</div>");
                                SB.AppendLine("<p/>");
                            }
                            else if (string.IsNullOrEmpty(buyer_id) && !string.IsNullOrEmpty(seller_id))
                            {

                                SB.AppendLine("<div class=\"talk-bubble tri-right border round btm-right-in\" style=\"margin-left:600px\">");
                                SB.AppendLine(string.Format("<img class=\"media-object img-circle\" style=\"width: 50px; height: 50px\" src =https://scontent-tpe1-1.xx.fbcdn.net/v/t1.0-1/p240x240/16114848_10158192073215445_7414104280503542946_n.jpg?oh=42ef68eb58a53d29ee3329896f490cca&oe=59D575C1 alt=\"...\">")); SB.AppendLine("<div class=\"talktext\">");

                                SB.AppendLine(string.Format("<p>{0}</p>", message));
                                SB.AppendLine("</div>");
                                SB.AppendLine("</div>");
                                SB.AppendLine("<p/>");
                            }
                        }
                    }
                }
            }


            return SB.ToString();
        }


        [HttpPost]
        public void WriteToDB()
        {
            //Request.Form["buyerName"]

            try
            {
                string buyerCommend = Request.Form["buyerCommend"];
                string buyer_message_id = Request.Form["buyer_message_id"];
                string buyer_selling_item_name = Request.Form["buyer_selling_item_name"];



                string sqlCommand = @"
                                  UPDATE ishop
                                  SET buyer_comment = N'{0}'
                                  WHERE member_id = {1} AND selling_item_name = '{2}'
                                ";
                System.Text.StringBuilder SB = new System.Text.StringBuilder();

                using (System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection(connectionString))
                {
                    connection.Open();

                    string sqlSendCommend = string.Format(sqlCommand, buyerCommend, buyer_message_id, buyer_selling_item_name);

                    using (System.Data.SqlClient.SqlCommand command = new System.Data.SqlClient.SqlCommand(sqlSendCommend, connection))
                    {
                        command.ExecuteNonQuery();
                    }

                    //
                }
            }
            catch (Exception eee)
            {
                string msg = eee.Message;
                msg = msg;
            }


        }

      
         
        [HttpPost]
        public string DoQuery_ishop()
        {
            string input_member_id = Request.Form["member_id"]; ;
            string sqlCommand = "SELECT * FROM ishop WHERE member_id = {0}";
            System.Text.StringBuilder SB = new System.Text.StringBuilder();


            using (System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection(connectionString))
            {
                connection.Open();

                using (System.Data.SqlClient.SqlCommand command = new System.Data.SqlClient.SqlCommand(string.Format(sqlCommand, input_member_id), connection))
                {
                    using (System.Data.SqlClient.SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string member_id = reader.GetString(0);
                            string selling_item_name = reader.GetString(1);
                            string selling_item_intro = reader.GetString(2);
                            string selling_price = reader.GetString(3);
                            string selling_image = reader.GetString(4);
                            string buyer_comment = reader.GetString(5);
                            string seller_comment = reader.GetString(6);
                            int is_to_sell = reader.GetInt32(7);

                            SB.AppendLine("<div class=\"col-md-6\">");
                            SB.AppendLine("<div class=\"media\">");
                            SB.AppendLine("<div class=\"media-left\">");
                            SB.AppendLine("<a href=\"#\" >");
                            SB.AppendLine(string.Format("<img class=\"media-object\" style=\"width: 120px; height: 120px\" src =\"{0}\" alt=\"...\">", selling_image));
                            SB.AppendLine("</a>");
                            SB.AppendLine("</div>");
                            SB.AppendLine("<div class=\"media-body\">");
                            SB.AppendLine(string.Format("<h4 class=\"media-heading\">{0}</h4>", selling_item_name));
                            SB.AppendLine(string.Format("{0}", selling_item_intro));
                            SB.AppendLine("<p/>");
                            SB.AppendLine(string.Format("{0}$", selling_price));

                            SB.AppendLine("<p/>");
                            SB.AppendLine(string.Format("<button class=\"btn btn-default\" onClick=\"addBuyerComment(\'{0}\', \'{1}\', \'{2}\')\" style=\"height:30px\" > 我有興趣 </ button >", member_id, selling_item_name, buyer_comment));


                            if (!string.IsNullOrEmpty(seller_comment))
                            {
                               
                                SB.AppendLine("<p/>");
                                SB.AppendLine(string.Format("<button class=\"btn btn-default\" onClick=\"GoBuy(\'{0}\', \'{1}\')\" style=\"height:30px\" > 購買 </ button >", member_id, selling_item_name));

                            }


                            SB.AppendLine("</div>");
                            SB.AppendLine("</div>");
                            SB.AppendLine("</div>");

                        }
                    }
                }


            }

            Console.WriteLine(SB.ToString());

            return SB.ToString();
        }

        [HttpPost]
        public string DoQuery_ishop_is_to_sell()
        {
            string input_member_id = Request.Form["member_id"];
            string sqlCommand = @"IF EXISTS
                                (
	                                SELECT * FROM ishop
	                                WHERE member_id = '{0}' AND seller_comment <> ''
                                )
                                BEGIN
	                                SELECT 1 AS res
                                END
                                ELSE
                                BEGIN
	                                SELECT 0 AS res
                                END";

            int v = 0;
            using (System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection(connectionString))
            {
                connection.Open();

                using (System.Data.SqlClient.SqlCommand command = new System.Data.SqlClient.SqlCommand(string.Format(sqlCommand, input_member_id), connection))
                {
                    using (System.Data.SqlClient.SqlDataReader reader = command.ExecuteReader())
                    {
                        reader.Read();

                        v = reader.GetInt32(0);
                        
                    }
                }


            }

            return v.ToString();
        }



        [HttpPost]
        public string DoQuery_evaluation()
        {
            string input_member_id = Request.Form["member_id"];
            string sqlCommand = @"
                                SELECT evaluation.member_id, evaluation.client_id, evaluation.evaluation, evaluation.start, vip.member_img, vip.member_name  
                                FROM client_evaluation evaluation
                                LEFT JOIN member_vip vip
                                ON evaluation.client_id = vip.member_id
                                WHERE evaluation.member_id = {0}
                                ";

            System.Text.StringBuilder SB = new System.Text.StringBuilder();
            using (System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection(connectionString))
            {
                connection.Open();

                using (System.Data.SqlClient.SqlCommand command = new System.Data.SqlClient.SqlCommand(string.Format(sqlCommand, input_member_id), connection))
                {
                    using (System.Data.SqlClient.SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string member_id = reader.GetString(0);
                            string client_id = reader.GetString(1);
                            string evaluation = reader.GetString(2);
                            int start = reader.GetInt32(3);
                            string member_img = reader.GetString(4);
                            string member_name = reader.GetString(5);

                            SB.AppendLine("<div class=\"col-md-12\" style=\"margin-top:5px\">");
                            SB.AppendLine("<div class=\"media\">");
                            SB.AppendLine("<div class=\"media-left\">");
                            SB.AppendLine("<a href=\"#\" >");
                            SB.AppendLine(string.Format("<img class=\"media-object\" style=\"width: 120px; height: 120px\" src =\"{0}\" alt=\"...\">", member_img));
                            SB.AppendLine("</a>");
                            SB.AppendLine("</div>");
                            SB.AppendLine("<div class=\"media-body\">");
                            SB.AppendLine(string.Format("<h4 class=\"media-heading\">{0}</h4>", member_name));
                            SB.AppendLine(string.Format("{0}", evaluation));
                            SB.AppendLine("<p/>");
                            SB.AppendLine(string.Format("{0}星", start));

                            


                            SB.AppendLine("</div>");
                            SB.AppendLine("</div>");
                            SB.AppendLine("<hr>");
                            SB.AppendLine("</div>");

                        }
                    }
                }


            }

            return SB.ToString();
        }

        [HttpPost]
        public string DoQuery_evaluation_count()
        {
            string input_member_id = Request.Form["member_id"];
            string sqlCommand = @"
                                SELECT ISNULL(COUNT(*), 0) AS total
                                FROM client_evaluation evaluation
                                WHERE evaluation.member_id = {0}
                                ";

            string v = "0";
            using (System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection(connectionString))
            {
                connection.Open();

                using (System.Data.SqlClient.SqlCommand command = new System.Data.SqlClient.SqlCommand(string.Format(sqlCommand, input_member_id), connection))
                {
                    using (System.Data.SqlClient.SqlDataReader reader = command.ExecuteReader())
                    {
                        reader.Read();

                        v = reader.GetInt32(0).ToString();


                    }
                }


            }

            return v;
        }
    }
}
