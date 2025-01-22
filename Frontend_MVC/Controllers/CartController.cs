using Frontend_MVC.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Frontend_MVC.Controllers
{
    [RoutePrefix("cart")]  // Prefix for all Cart actions
    public class CartController : Controller
    {
        private readonly HttpClient _httpClient;
        private const string ApiBaseUrl = "http://localhost:22640/api/"; // Backend API base URL

        public CartController()
        {
            _httpClient = new HttpClient();
        }

        // View Cart Items
        [HttpGet]
        [Route("")]  // Route to "/cart"
        public async Task<ActionResult> Index()
        {
            var response = await _httpClient.GetAsync(ApiBaseUrl + "Cart");
            var content = await response.Content.ReadAsStringAsync();
            var cartItems = JsonConvert.DeserializeObject<List<CartItem>>(content);
            return View(cartItems);
        }

        // Add to Cart
        [HttpGet]
        [Route("{cart/productId}")]  // Route to "/cart/add/{productId}"
        public async Task<ActionResult> Cart(int productId)
        {
            var cartItem = new CartItem
            {
                ProductId = productId,
                Quantity = 1
            };

            var jsonContent = JsonConvert.SerializeObject(cartItem);
            var response = await _httpClient.PostAsync(
                ApiBaseUrl + "Cart",
                new StringContent(jsonContent, Encoding.UTF8, "application/json")
            );

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            // Handle error
            return View("Error");
        }
    }
}
