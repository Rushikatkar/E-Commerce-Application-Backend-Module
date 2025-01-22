using Frontend_MVC.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Web.Mvc;

namespace Frontend_MVC.Controllers
{
    [RoutePrefix("Product")]
    public class ProductController : Controller
    {
        private readonly HttpClient _httpClient;

        public ProductController()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:22640/api/") // Ensure this matches your API base URL
            };
        }

        // GET: Product
        [HttpGet]
        [Route("")]
        public ActionResult Index()
        {
            var response = _httpClient.GetAsync("Products/GetProduct").Result;
            if (response.IsSuccessStatusCode)
            {
                var jsonData = response.Content.ReadAsStringAsync().Result;
                var products = JsonConvert.DeserializeObject<List<Product>>(jsonData);
                return View(products);
            }
            return View("Error");
        }

        // GET: Product/Create
        [HttpGet]
        [Route("create")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Product/Create
        [HttpPost]
        [Route("create")]
        public ActionResult Create([Bind(Exclude = "ProductId")] Product product)
        {
            if (ModelState.IsValid)
            {
                var jsonData = JsonConvert.SerializeObject(product);
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                var response = _httpClient.PostAsync("Products/AddProduct", content).Result;
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(product);
        }

        // GET: Product/Edit/{id}
        [HttpGet]
        [Route("edit/{id}")]
        public ActionResult Edit(int id)
        {
            var response = _httpClient.GetAsync($"Products/GetProduct/{id}").Result; // This assumes an endpoint to get a specific product by ID
            if (response.IsSuccessStatusCode)
            {
                var jsonData = response.Content.ReadAsStringAsync().Result;
                var product = JsonConvert.DeserializeObject<Product>(jsonData);
                return View(product);
            }
            return View("Error");
        }

        // POST: Product/Edit
        [HttpPost]
        [Route("edit")]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                var jsonData = JsonConvert.SerializeObject(product);
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                var response = _httpClient.PutAsync("Products/UpdateProduct", content).Result;
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(product);
        }

        // GET: Product/Delete/{id}
        [HttpGet]
        [Route("delete/{id}")]
        public ActionResult Delete(int id)
        {
            var response = _httpClient.GetAsync($"Products/GetProduct/{id}").Result; // Ensure your API has a way to get a specific product
            if (response.IsSuccessStatusCode)
            {
                var jsonData = response.Content.ReadAsStringAsync().Result;
                var product = JsonConvert.DeserializeObject<Product>(jsonData);
                return View(product);
            }
            return View("Error");
        }

        // POST: Product/Delete
        [HttpPost, ActionName("Delete")]
        [Route("delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var response = _httpClient.DeleteAsync($"Products/DeleteProduct/{id}").Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View("Error");
        }
    }
}
