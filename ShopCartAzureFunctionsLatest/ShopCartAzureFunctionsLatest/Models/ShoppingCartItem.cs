using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopCartAzureFunctionsLatest.Models
{
    public class ShoppingCartItem
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public DateTime Created { get; set; } 
        public bool Collected {  get; set; }
        public string ItemName { get; set; }
    }
    internal class CreateShoppingCartItem
    {
        public string ItemName { get; set; }
    }

    internal class UpdateShoppingCartItem
    {
        public string ItemName { get; set; }
        public bool Collected { get; set; }

    }
}
