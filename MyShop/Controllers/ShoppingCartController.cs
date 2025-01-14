using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using MyShop.Models;

namespace MyShop.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly ShopContext _context;
        private List<ShoppingCartItem> _cartItems;

        public ShoppingCartController(ShopContext context)
        {
            _context = context;
            _cartItems = new List<ShoppingCartItem>();
        }
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult AddToCart(int id)
        {
            var bicycleToAdd = _context.Bicycles.Find(id);

            var cartItems = HttpContext.Session.Get<List<ShoppingCartItem>>("Cart") ?? new List<ShoppingCartItem>();

            var existingCartItem = cartItems.FirstOrDefault(item => item.Bicycle.Id == id);

            if (existingCartItem != null)
            {
                existingCartItem.Quantity++;
            }
            else
            {
                cartItems.Add(new ShoppingCartItem
                {
                    Bicycle = bicycleToAdd,
                    Quantity = 1
                });
            }

            HttpContext.Session.Set("Cart", cartItems);

            //writing temp data
            TempData["CartMessage"] = $"{bicycleToAdd.Brand} {bicycleToAdd.Model} added to cart";

            return RedirectToAction("ViewCart");


        }

        public IActionResult ViewCart()
        {
            var cartItems = HttpContext.Session.Get<List<ShoppingCartItem>>("Cart") ?? new List<ShoppingCartItem>();

            var cartViewModel = new ShoppingCartViewModel
            {
                CartItems = cartItems,
                TotalPrice = cartItems.Sum(item => item.Bicycle.Price * item.Quantity)
            };

            ViewBag.CartMessage = TempData["CartMessage"]; //reading tempdata

            return View(cartViewModel);
        }
        public IActionResult RemoveItem(int id)
        {
            var cartItems = HttpContext.Session.Get<List<ShoppingCartItem>>("Cart") ?? new List<ShoppingCartItem>();
            var itemToRemove = cartItems.FirstOrDefault(item => item.Bicycle.Id == id);

            //writing tempdata
            TempData["CartMessage"] = $"{itemToRemove.Bicycle.Brand} {itemToRemove.Bicycle.Model} Removed From Cart";

            if (itemToRemove != null)
            {
                if (itemToRemove.Quantity > 1)
                {
                    itemToRemove.Quantity--;
                }
                else
                {

                    cartItems.Remove(itemToRemove);
                }
            }

            HttpContext.Session.Set("Cart", cartItems);

            return RedirectToAction("ViewCart");
        }

        [HttpPost]
        public IActionResult PurchaseItems()
        {
            var cartItems = HttpContext.Session.Get<List<ShoppingCartItem>>("Cart") ?? new List<ShoppingCartItem>();

            foreach (var item in cartItems)
            {
                //Save each item as a purchase

                _context.purchases.Add(new Purchase
                {
                    BicycleId = item.Bicycle.Id,
                    Quantity = item.Quantity,
                    PurchaseDate = DateTime.Now,
                    Total = item.Bicycle.Price * item.Quantity
                });            
            }

            _context.SaveChanges();

            //clear the cart
            HttpContext.Session.Set("Cart", new List<ShoppingCartItem>());

            return RedirectToAction("Index", "Home");
        }

    }
  }


        
        
    

