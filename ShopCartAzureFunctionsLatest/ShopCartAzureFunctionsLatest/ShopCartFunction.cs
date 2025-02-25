using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ShopCartAzureFunctionsLatest.Models;
using System.Text.Json.Serialization;

namespace ShopCartAzureFunctionsLatest
{
    public class ShopCartFunction
    {
        private readonly ILogger<ShopCartFunction> _logger;
        static List<ShoppingCartItem> Items = new();

        public ShopCartFunction(ILogger<ShopCartFunction> logger)
        {
            _logger = logger;
        }

        [Function("GetShoppingCartItems")]
        public   async Task<IActionResult> GetShoppingCartItems([HttpTrigger(AuthorizationLevel.Anonymous, "get",  Route ="shoppingcartitem")] HttpRequest req)
        {
            _logger.LogInformation("Get Shopping Cart Items.");
            return new OkObjectResult(Items);
            
        }
        [Function("GetShoppingCartItem")]
        public static async Task<IActionResult> GetShoppingCartItem([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "shoppingcartitem/{id}")] HttpRequest req, string id)
        {
            _logger.LogInformation("Get Shopping Cart Items.");
            var shoppingcartItem = Items.FirstOrDefault(q => q.Id == id);
            if (shoppingcartItem == null)
            {
                return new NotFoundResult();

            }
            return new OkObjectResult(shoppingcartItem);
        }

        [Function("CreateShoppingCartItems")]
        public   async Task<IActionResult> CreateShoppingCartItems([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "shoppingcartitem")] HttpRequest req)
        {
            _logger.LogInformation("create Shopping Cart Items.");
            string reqData = await( new StreamReader(req.Body).ReadToEndAsync());
            var data = JsonConvert.DeserializeObject<CreateShoppingCartItem>(reqData);
            var item = new ShoppingCartItem
            {
                ItemName = data.ItemName
            };

            Items.Add(item);

            return new OkObjectResult(item);
        }


        [Function("PutShoppingCartItems")]
        public   async Task<IActionResult> PutShoppingCartItems([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "shoppingcartitem/{id}")] HttpRequest req, string id)
        {
            _logger.LogInformation("Put Shopping Cart Items.");
            string reqData = await (new StreamReader(req.Body).ReadToEndAsync());
            var data = JsonConvert.DeserializeObject<UpdateShoppingCartItem>(reqData);
           // var data = JsonConvert.DeserializeObject<UpdateShoppingCartItem>(reqData);
            var shoppingcartItem = Items.FirstOrDefault(q => q.Id == id);
            if (shoppingcartItem == null)
            {
                return new NotFoundResult();

            }
            shoppingcartItem.Collected = data.Collected;

            return new OkObjectResult(shoppingcartItem);
        }


        [Function("DeleteShoppingCartItems")]
        public   async Task<IActionResult> DeleteShoppingCartItems([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "shoppingcartitem")] HttpRequest req, string id)
        {
            string reqData = await (new StreamReader(req.Body).ReadToEndAsync());
            
            var shoppingcartItem = Items.FirstOrDefault(q => q.Id == id);
            if (shoppingcartItem == null)
            {
                return new NotFoundResult();

            }
            Items.Remove(shoppingcartItem);

            return new OkObjectResult("Deleted success");

             
        }


    }
}
