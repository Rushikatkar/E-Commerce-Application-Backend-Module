using Frontend_MVC.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Frontend_MVC.Controllers
{
    [RoutePrefix("user")]
    public class UserController : Controller
    {
        private readonly string _baseUrl = "http://localhost:22640/api"; // Replace with your actual API URL

        // Register View
        [HttpGet]
        [Route("register")]
        public ActionResult Register()
        {
            return View();
        }

        // Register POST action
        [HttpPost]
        [Route("register")]
        public async Task<ActionResult> Register([Bind(Exclude = "UserId,ConfirmPassword")] UserModel user)
        {
            if (ModelState.IsValid)
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    var content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
                    var response = await httpClient.PostAsync($"{_baseUrl}/users/register", content);

                    string responseBody = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Login");
                        
                    }
                    else
                    {
                        ViewBag.Message = $"Registration failed: {responseBody}";
                    }
                }
                return View();
            }

            ViewBag.Message = "Invalid input data";
            return View();
        }

        // Login View
        [HttpGet]
        [Route("login")]
        public ActionResult Login()
        {
            return View();
        }

        // Login POST action
        [HttpPost]
        [Route("login")]
        public async Task<ActionResult> Login(LoginModel login)
        {
            if (ModelState.IsValid)
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    var content = new StringContent(JsonConvert.SerializeObject(login), Encoding.UTF8, "application/json");
                    var response = await httpClient.PostAsync($"{_baseUrl}/users/login", content);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        var responseObject = JsonConvert.DeserializeObject<Dictionary<string, string>>(responseBody);

                        if (responseObject != null && responseObject.ContainsKey("token"))
                        {
                            string token = responseObject["token"];

                            // Store token in an HTTP-only and secure cookie
                            HttpCookie tokenCookie = new HttpCookie("AuthToken", token)
                            {
                                HttpOnly = true,  // Prevents access via JavaScript
                                Secure = true,    // Ensures the cookie is sent only over HTTPS
                                Expires = DateTime.Now.AddMinutes(60) // Set cookie expiration
                            };
                            Response.Cookies.Add(tokenCookie);

                            
                            return RedirectToAction("Index", "Product");
                        }
                        else
                        {
                            ViewBag.Message = "Login response invalid.";
                        }
                    }
                    else
                    {
                        ViewBag.Message = "Invalid credentials.";
                    }
                }
                return View();
            }

            ViewBag.Message = "Invalid input data";
            return View();
        }

        // Logout action to clear the cookie
        [Route("logout")]
        public ActionResult Logout()
        {
            if (Request.Cookies["AuthToken"] != null)
            {
                HttpCookie tokenCookie = new HttpCookie("AuthToken");
                tokenCookie.Expires = DateTime.Now.AddDays(-1);  // Set expiration date to a past date to remove the cookie
                Response.Cookies.Add(tokenCookie);
            }
            return RedirectToAction("Login");
        }
    }
}
