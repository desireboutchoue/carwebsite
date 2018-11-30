using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace carwebsite.Models
{
    public partial class ShoppingCart
    {
       CarStoreEntities db = new CarStoreEntities();
        string ShoppingCartId { get; set; }

        public const string CartSessionKey = "CartId";
        public static ShoppingCart GetCart(HttpContextBase context)
        {
            var cart = new ShoppingCart();
            cart.ShoppingCartId = cart.GetCartId(context);  //透過context  取得 context.Session[CartSessionKey]  給cartId
            return cart;
        }

        // Helper method to simplify shopping cart calls
        public static ShoppingCart GetCart(Controller controller)
        {
            return GetCart(controller.HttpContext);
        }

        public void AddToCart(Car car)
        {
            // Get the matching cart and album instances
            var cartItem = db.CartsCar.SingleOrDefault(
                c => c.CartId == ShoppingCartId
                && c.CarId == car.CarId);

            if (cartItem == null)
            {
                // Create a new cart item if no cart item exists
                cartItem = new Cart
                {
                    CarId = car.CarId,
                    CartId = ShoppingCartId,
                    Count = 1,
                    DateCreated = DateTime.Now
                };
                db.CartsCar.Add(cartItem);
            }
            else
            {
                // If the item does exist in the cart, 
                // then add one to the quantity
                cartItem.Count++;
            }
            // Save changes
            db.SaveChanges();
        }


        public int RemoveFromCart(int id)
        {
            // Get the cart
            var cartItem = db.CartsCar.Single(
                cart => cart.CartId == ShoppingCartId
                && cart.RecordId == id);

            int itemCount = 0;

            if (cartItem != null)
            {
                //if (cartItem.Count > 1)  一次減一個商品
                //{
                //    cartItem.Count--;
                //    itemCount = cartItem.Count;
                //}
                //else
                //{
                db.CartsCar.Remove(cartItem);
                //}
                // Save changes
                db.SaveChanges();
            }
            return itemCount;
        }


        public void EmptyCart()
        {
            var cartItems = db.CartsCar.Where(            //淨空購物車
                cart => cart.CartId == ShoppingCartId);

            foreach (var cartItem in cartItems)
            {
                db.CartsCar.Remove(cartItem);
            }
            // Save changes
           db.SaveChanges();
        }

        public List<Cart> GetCartItems()   //取得現在在購物車內的商品
        {
            return db.CartsCar.Where(
                cart => cart.CartId == ShoppingCartId).ToList();
        }


        public int GetCount()   //取得商品數量，顯示在首頁
        {
            // Get the count of each item in the cart and sum them up
            int? count = (from cartItems in db.CartsCar
                          where cartItems.CartId == ShoppingCartId
                          select (int?)cartItems.Count).Sum();
            // Return 0 if all entries are null
            return count ?? 0;
        }


        public decimal GetTotal() //取得總金額
        {
            // Multiply album price by count of that album to get 
            // the current price for each of those albums in the cart
            // sum all album price totals to get the cart total
            decimal? total = (from cartItems in db.CartsCar
                              where cartItems.CartId == ShoppingCartId
                              select (int?)cartItems.Count *
                              cartItems.Car.Price).Sum();

            return total ?? decimal.Zero;  //若total是空值，則回傳0.00
        }

        public int CreateOrder(Booking booking, decimal disCount)   //把每一筆購買商品記錄到order中
        {
            decimal orderTotal = 0;

            var cartItems = GetCartItems();  //先取得所有商品
            // Iterate over the items in the cart, 
            // adding the order details for each
            foreach (var item in cartItems)  //每筆存成orderDetail
            {
                var bookingDetail = new BookingDetail
                {
                    CarId = item.CarId,
                    BookingId = booking.BookingId,
                    UnitPrice = item.Car.Price,
                    Quantity = item.Count
                };

                // Set the order total of the shopping cart
                orderTotal += (item.Count * item.Car.Price) * disCount;  //累計取得訂單總價

                db.BookingDetails.Add(bookingDetail);

            }
            // Set the order's total to the orderTotal count
            db.Bookings.Single(o => o.BookingId == booking.BookingId).Total = orderTotal;


            // Save the order
            db.SaveChanges();
            // Empty the shopping cart
            EmptyCart();
            // Return the OrderId as the confirmation number
            return booking.BookingId;
        }

        // We're using HttpContextBase to allow access to cookies.
        public string GetCartId(HttpContextBase context)
        {
            if (context.Session[CartSessionKey] == null)
            {
                if (!string.IsNullOrWhiteSpace(context.User.Identity.Name))
                {
                    context.Session[CartSessionKey] =
                        context.User.Identity.Name;      //使用者已經登入，CartID直接設成userEmail
                }
                else
                {
                    // Generate a new random GUID using System.Guid class
                    Guid tempCartId = Guid.NewGuid();   //給購物車隨機ID  因為使用者還未登入
                    // Send tempCartId back to client as a cookie
                    context.Session[CartSessionKey] = tempCartId.ToString();
                }
            }
            return context.Session[CartSessionKey].ToString();
        }

        // When a user has logged in, migrate their shopping cart to
        // be associated with their username
        public void MigrateCart(string UserEmail)      //登入之後將上面隨機給的ID換成UserEmail
        {
            var shoppingCart = db.CartsCar.Where(
                c => c.CartId == ShoppingCartId);

            foreach (Cart item in shoppingCart)
            {
                item.CartId = UserEmail;  //把cartid以email標註為某user的
            }
            db.SaveChanges();
        }

        //用來更新數量
        public int UpdateCartCount(int id, int cartCount)
        {
            // Get the cart 
            var cartItem = db.CartsCar.Single(
                cart => cart.CartId == ShoppingCartId
                && cart.RecordId == id);

            int itemCount = 0;

            if (cartItem != null)
            {
                if (cartCount > 0)
                {
                    cartItem.Count = cartCount;
                    itemCount = cartItem.Count;
                }
                else
                {
                    db.CartsCar.Remove(cartItem);
                }
                // Save changes 
               db.SaveChanges();
            }
            return itemCount;
        }

    }
}
