using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
 
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using ProjectD.Models.AccountViewModels;
using ProjectD.Models;
using Microsoft.AspNetCore.Identity;
using ProjectD.Services;

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

/*
        public ActionResult Login()
        {
            return View();
        }
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly ISmsSender _smsSender;
        private readonly ILogger _logger;
        private readonly string _externalCookieScheme;

        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=532713
                    // Send an email with this link
                    //var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    //var callbackUrl = Url.Action(nameof(ConfirmEmail), "Account", new { userId = user.Id, code = code }, protocol: HttpContext.Request.Scheme);
                    //await _emailSender.SendEmailAsync(model.Email, "Confirm your account",
                    //    $"Please confirm your account by clicking this link: <a href='{callbackUrl}'>link</a>");
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    _logger.LogInformation(3, "User created a new account with password.");
                    return RedirectToLocal(returnUrl);
                }
                AddErrors(result);
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }


        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }
        */
    }
}
