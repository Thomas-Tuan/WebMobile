using Mobile_ecommerce.Common;
using Mobile_ecommerce.Models.EF;
using Mobile_ecommerce.Models.ViewModel.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace Mobile_ecommerce.Controllers
{
    public class ShoppingCartController : BaseController
    {
        private MobileDbContext db;
        public ShoppingCartController()
        {
            db = new MobileDbContext();
        }
        public Cart GetCart()
        {
            Cart cart = Session["cart"] as Cart;
            if (cart == null || Session["cart"] == null)
            {
                cart = new Cart();
                Session["cart"] = cart;
            }
            return cart;
        }
        // GET: ShoppingCart
        public ActionResult Index()
        {
            if (Session["cart"] == null)
            {
                return View();
            }
            Cart giohang = Session["cart"] as Cart;
            string a = giohang.GetTotal().ToString();
            System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("en-US");
            decimal value = decimal.Parse(a, System.Globalization.NumberStyles.AllowThousands);
            ViewBag.price = string.Format(culture, "{0:N0}", value);
            return View(giohang);
        }

        public ActionResult AddToCart(int id)
        {         
            var product = db.Products.SingleOrDefault(p => p.ProductID == id);
            if (product != null)
            {           
                GetCart().AddItem(product);
            }
            TempData["AlertMessage"] = "Thêm vào giỏ hàng thành công !";
            TempData["AlertType"] = "alert-success";
            return RedirectToAction("Index","ShoppingCart");
        }
        public ActionResult RemoveFromCart(int productId)
        {
            Cart cart = Session["cart"] as Cart;
            Product product = db.Products
                .SingleOrDefault(p => p.ProductID == productId);

            if (product != null)
            {
                cart.RemoveItem(productId);
            }
            TempData["AlertMessage"] = "Xóa thành công !";
            TempData["AlertType"] = "alert-danger";
            return RedirectToAction("Index", "ShoppingCart");
        }
        public ActionResult UpdateFromCart(FormCollection form)
        {
            Cart cart = Session["Cart"] as Cart;
            int idPro = int.Parse(form["IDPro"]);
            int soluong = int.Parse(form["SL"]);
            cart.UpdateItem(idPro, soluong);
            TempData["AlertMessage"] = "Cập nhật thành công !";
            TempData["AlertType"] = "alert-success";
            return RedirectToAction("Index", "ShoppingCart");
        }
        public PartialViewResult BagCart()
        {
            int totalitem = 0;
            Cart cart = Session["cart"] as Cart;
            if (cart != null)
            {
                totalitem = cart.TotalQuantity();
            }
            ViewBag.Listitem = totalitem;
            return PartialView("BagCart");
        }

        public ActionResult Order()
        {
            if (Session[CommonConstants.USER_SESSION] == null)
            {
                return RedirectToAction("Index", "UserClient");
            }
            Cart cart = GetCart();
            if (cart == null)
            {
                return RedirectToAction("Index", "ShoppingCart");
            }
            var id = (UserLogin)(Session[CommonConstants.USER_SESSION]);
            var getid = db.Customers.Where(n => n.CustomerID.Equals(id.UserID)).FirstOrDefault();
            ViewBag.Info = getid;
            ViewBag.Totalquantity = cart.TotalQuantity();
            string a = cart.GetTotal().ToString();
            System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("en-US");
            decimal value = decimal.Parse(a, System.Globalization.NumberStyles.AllowThousands);
            ViewBag.price = string.Format(culture, "{0:N0}", value); 
            return View(cart);
        }      
        public ActionResult SuccessPage()
        {
            return View();
        }
        public ActionResult GetVoucher(FormCollection form)
        {        
            string mavoucher = (form["mavoucher"]).Trim().ToString();
            var id = (UserLogin)(Session[CommonConstants.USER_SESSION]);
            var getid = db.Customers.Where(n => n.CustomerID.Equals(id.UserID)).FirstOrDefault();
            ViewBag.Info = getid;
            Cart cart = GetCart();
            if (!string.IsNullOrEmpty(mavoucher))
            {
                var check = db.Vouchers.Where(n => n.VoucherID.Contains(mavoucher)).FirstOrDefault();
                if (check == null)
                {
                    ViewBag.messageOn = "Mã không hợp lệ hoặc không tồn tại";
                    return View("Order", cart);
                }
                else
                {
                    decimal apdungvoucher = cart.GetTotal() * check.Discount;
                    string formatDecimal = apdungvoucher.ToString("0.#");
                    decimal totalMoney = cart.GetTotal() - decimal.Parse(formatDecimal);
                    System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("en-US");
                    string a = totalMoney.ToString();
                    decimal value = decimal.Parse(a, System.Globalization.NumberStyles.AllowThousands);
                    ViewBag.Totalprice = string.Format(culture, "{0:N0}", value);
                    ViewBag.messageOn = "Áp dụng thành công";
                    Session["CouponCode"] = mavoucher;
                    return View("Order", cart);
                }
            }
            ViewBag.messageOn = "Mã không hợp lệ hoặc không tồn tại";
            return View("Order", cart);
        }
        public ActionResult SuccessOrder()
        {
            Cart cart = GetCart();
            Order order = new Order();
            var id = (UserLogin)(Session[CommonConstants.USER_SESSION]);
            var getid = db.Customers.Where(n => n.CustomerID.Equals(id.UserID)).FirstOrDefault();
            ViewBag.Info = getid;     
            order.CustomerID = id.UserID;
            order.OrderDate = DateTime.Now;          
            order.ShippingID = 1;
            order.OrderStatusID = 1;
            if (Session["CouponCode"] != null)
            {
                order.VoucherID = Session["CouponCode"].ToString();
            }           
            db.Orders.Add(order);
            db.SaveChanges();
            foreach(var item in cart.Items)
            {
                OrderDetail details = new OrderDetail();
                details.OrderID = order.OrderID;
                details.ProductID = item.Product.ProductID;
                details.Quantity = item.Quantity;
                details.TotalMoney = item.TotalPrice;
                db.OrderDetails.Add(details);           
            }
            db.SaveChanges();
            cart.Clear();
            return RedirectToAction("SuccessPage");
        }       
    }
}