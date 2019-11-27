using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AutoTrashCartWebServiceApp.Models;
using Newtonsoft.Json;

namespace AutoTrashCartWebServiceApp.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult ResetPassword(string code)
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel resetPasswordViewModel)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://autotrashcartwebserviceapp20191124023351.azurewebsites.net/api/");
                //client.BaseAddress = new Uri("https://localhost:44362/api/");

                //HTTP POST
                var postTask = client.PostAsJsonAsync("Account/ResetPassword", resetPasswordViewModel);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("ResetPasswordConfirmation");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Looks like the token is expired, please request for new password reset email.");
                    ModelState.AddModelError(string.Empty, "Or please contact administrator.");
                }
            }
            return View();
        }

        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }
    }
}