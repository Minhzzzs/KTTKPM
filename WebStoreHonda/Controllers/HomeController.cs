using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebStoreHonda.Models;

namespace WebStoreHonda.Controllers
{
    public class HomeController : Controller
    {
        StoreHondaEntities4 db = new StoreHondaEntities4();
        public ActionResult Index()
        {
            var products = db.SanPhams.Where(u => u.TrangThai == true)
                        .OrderByDescending(p => p.MaSanPham)
                        .Take(8)
                        .ToList();
            return View(products);
        }
        public ActionResult GetDanhMuc()
        {

            var danhMucs = db.DanhMucs.Where(u => u.TrangThai == true).ToList(); // Lấy tất cả các danh mục từ database
            return PartialView("_DanhMucPartial", danhMucs); // Trả về partial view chứa danh mục
        }

        public ActionResult ChiTietSanPham(int id)
        {
            // Retrieve the product by its ID
            var sanPham = db.SanPhams.FirstOrDefault(sp => sp.MaSanPham == id && sp.TrangThai == true);
            if (sanPham == null)
            {
                return HttpNotFound();
            }

            // Get reviews (comments) for the product
            var binhLuans = db.BinhLuans
                              .Where(b => b.MaSanPham == id)
                              .OrderByDescending(b => b.NgayBinhLuan)
                              .ToList();
            ViewBag.BinhLuans = binhLuans;
            var mauSacList = db.MauSacs
                .Where(ms => ms.SanPham_MauSac.Any(spm => spm.MaSanPham == sanPham.MaSanPham && spm.TrangThai == true))
                .Select(ms => new MauSacViewModel
                {
                    MaMauSac = ms.MaMauSac,
                    TenMauSac = ms.TenMauSac,
                    MaSanPham = sanPham.MaSanPham,
                    HinhMauSac = ms.SanPham_MauSac
                        .Where(spm => spm.MaSanPham == sanPham.MaSanPham)
                        .Select(spm => spm.HinhMauSac) // Get the first associated image
                        .FirstOrDefault() // Only take the first image (if available)
                })
                .ToList();
            ViewBag.MauSac = mauSacList;

            // Get all images associated with the product's colors
            var hinhmausac = db.SanPham_MauSac
                               .Where(u => u.MaSanPham == sanPham.MaSanPham && u.TrangThai == true)
                               .ToList();
            ViewBag.HinhMauSac = hinhmausac;

            // Get other products in a similar price range (excluding the current product)
            var sanPhamCungGia = db.SanPhams
                .Where(sp => sp.Gia >= sanPham.Gia - 10000000 && sp.Gia <= sanPham.Gia + 10000000
                            && sp.MaSanPham != sanPham.MaSanPham && sp.TrangThai == true) // Price range filter
                .ToList();
            ViewBag.SanPhamCungGia = sanPhamCungGia;

            // Return the product to the view
            return View(sanPham);
        }

        public ActionResult GioHang()
        {
            var email = Session["UserEmail"] as string;
            var user = db.KhachHangs.SingleOrDefault(u => u.Email == email);
            var giohang = db.GioHangs.SingleOrDefault(u => u.MaKhachHang == user.MaKhachHang);
            var chitietgiohang = db.ChiTietGioHangs
                   .Where(u => u.MaGioHang == giohang.MaGioHang)
                   .ToList();
            return View(chitietgiohang);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult CuaHang(int id, string search = "", int page = 1, int pageSize = 6, string sort = "")
        {
            // Kiểm tra xem có sản phẩm nào với MaDanhMuc = id hay không
            var products = db.SanPhams.Where(u => u.MaDanhMuc == id && u.TrangThai == true);
            if (search != "")
            {
                products = products.Where(u => u.TenSanPham.Contains(search));
            }
            // Nếu không tìm thấy sản phẩm nào, chuyển hướng đến action Error
            if (!products.Any())
            {
                return RedirectToAction("Error", "Home"); // Chuyển đến action Error trong controller Home
            }
            switch (sort)
            {
                case "price_asc":
                    products = products.OrderBy(p => p.Gia);
                    ViewBag.CurrentSort = sort;
                    break;
                case "price_desc":
                    products = products.OrderByDescending(p => p.Gia);
                    ViewBag.CurrentSort = sort;
                    break;
                default:
                    products = products.OrderBy(p => p.MaSanPham);
                    break;
            }

            var totalProducts = products.Count();
            var totalPages = (int)Math.Ceiling((double)totalProducts / pageSize);

            var productsToDisplay = products
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentPage = page;

            return View(productsToDisplay);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult DatHangThanhCong()
        {
            var email = Session["UserEmail"] as string;
            if (string.IsNullOrEmpty(email))
            {
                return RedirectToAction("DangNhap", "Home");
            }

            var user = db.KhachHangs.SingleOrDefault(u => u.Email == email);
            if (user == null)
            {
                return HttpNotFound("Người dùng không tìm thấy.");
            }

            var giohang = db.GioHangs.SingleOrDefault(u => u.MaKhachHang == user.MaKhachHang);
            if (giohang == null)
            {
                return HttpNotFound("Giỏ hàng không tồn tại.");
            }

            var chitietgiohang = db.ChiTietGioHangs
                .Where(u => u.MaGioHang == giohang.MaGioHang)
                .ToList();

            if (!chitietgiohang.Any())
            {
                ModelState.AddModelError("", "Giỏ hàng của bạn đang trống.");
                return View("GioHang");
            }

            // Kiểm tra tồn kho của tất cả sản phẩm trước khi tạo đơn hàng
            foreach (var item in chitietgiohang)
            {
                var sanPham = db.SanPhams.SingleOrDefault(sp => sp.MaSanPham == item.MaSanPham);
                if (sanPham == null || sanPham.SoLuong < item.SoLuong)
                {
                    TempData["ErrorMessage"] = $"Sản phẩm \"{sanPham?.TenSanPham}\" đã hết hàng hoặc không đủ số lượng.";
                    return RedirectToAction("GioHang");
                }
            }

            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    // Tạo đơn hàng mới
                    var donHang = new HoaDon
                    {
                        MaKhachHang = user.MaKhachHang,
                        NgayLap = DateTime.Now,
                        TongTien = chitietgiohang.Sum(item => item.SoLuong * db.SanPhams.Single(sp => sp.MaSanPham == item.MaSanPham).Gia),
                        TrangThai = "Đang chờ xử lý"
                    };

                    db.HoaDons.Add(donHang);
                    db.SaveChanges();

                    // Cập nhật tồn kho
                    foreach (var item in chitietgiohang)
                    {
                        var sanPham = db.SanPhams.SingleOrDefault(sp => sp.MaSanPham == item.MaSanPham);
                        if (sanPham != null)
                        {
                            sanPham.SoLuong -= item.SoLuong;
                        }
                    }

                    db.SaveChanges();

                    // Xóa chi tiết giỏ hàng sau khi đặt hàng
                    db.ChiTietGioHangs.RemoveRange(chitietgiohang);
                    db.SaveChanges();

                    transaction.Commit(); // Xác nhận thay đổi
                }
                catch (Exception ex)
                {
                    transaction.Rollback(); // Hoàn tác tất cả thay đổi nếu có lỗi
                    TempData["ErrorMessage"] = "Đã xảy ra lỗi khi xử lý đơn hàng: " + ex.Message;
                    return RedirectToAction("GioHang");
                }
            }
            return View();
        }


        public ActionResult Error()
        {
            return View();
        }
        public ActionResult ChiTietDonHang(int id)
        {
            // Lấy thông tin đơn hàng từ cơ sở dữ liệu theo id
            var order = db.HoaDons.SingleOrDefault(o => o.MaHoaDon == id);

            if (order == null)
            {
                return HttpNotFound(); // Nếu không tìm thấy đơn hàng
            }

            // Lấy chi tiết đơn hàng
            var chiTietDonHang = db.ChiTietHoaDons.Where(ct => ct.MaHoaDon == id).ToList();

            // Lưu thông tin vào ViewBag
            ViewBag.Order = order; // Lưu thông tin đơn hàng
            ViewBag.ChiTietDonHang = chiTietDonHang; // Lưu thông tin chi tiết đơn hàng

            return View(); // Trả về view
        }


        public ActionResult HoSo()
        {
            var email = Session["UserEmail"] as string;
            var user = db.KhachHangs.SingleOrDefault(k => k.Email.Contains(email));

            // Lấy danh sách đơn hàng của người dùng
            var orders = db.HoaDons.Where(d => d.MaKhachHang == user.MaKhachHang).ToList(); // Giả sử `KhachHangId` là khóa ngoại trong bảng `DonHang`

            ViewBag.Orders = orders; // Đưa danh sách đơn hàng vào ViewBag
            return View(user);
        }

        [HttpPost]
        public ActionResult AddToCart(int productId, int quantity, int selectedColor)
        {
            // Tìm sản phẩm theo productId
            var product = db.SanPhams.SingleOrDefault(sp => sp.MaSanPham == productId);
            if (product == null)
            {
                return HttpNotFound();
            }

            // Lấy email từ Session và tìm khách hàng tương ứng
            var email = Session["UserEmail"] as string;
            var user = db.KhachHangs.SingleOrDefault(u => u.Email == email);
            if (user == null)
            {
                return HttpNotFound("User not found.");
            }

            // Lấy giỏ hàng của người dùng
            var giohang = db.GioHangs.SingleOrDefault(u => u.MaKhachHang == user.MaKhachHang);
            if (giohang == null)
            {
                // Nếu giỏ hàng không tồn tại, bạn có thể tạo một giỏ hàng mới
                giohang = new GioHang
                {
                    MaKhachHang = user.MaKhachHang,
                    // Thêm các thuộc tính khác cần thiết cho giỏ hàng
                };
                db.GioHangs.Add(giohang);
                db.SaveChanges(); // Lưu giỏ hàng mới vào cơ sở dữ liệu
            }

            // Tìm sản phẩm trong giỏ hàng
            var cartItem = db.ChiTietGioHangs
                .SingleOrDefault(ci => ci.MaGioHang == giohang.MaGioHang && ci.MaSanPham == productId && ci.MaMauSac == selectedColor);

            if (cartItem == null)
            {
                // Nếu sản phẩm chưa có trong giỏ, thêm sản phẩm mới vào giỏ
                var newCartItem = new ChiTietGioHang
                {
                    MaGioHang = giohang.MaGioHang,
                    MaSanPham = productId,
                    SoLuong = quantity,
                    MaMauSac = selectedColor, // Lưu màu sắc
   
                };
                db.ChiTietGioHangs.Add(newCartItem);
            }
            else
            {
                // Nếu sản phẩm đã có trong giỏ, cập nhật số lượng
                cartItem.SoLuong += quantity;
            }

            // Lưu thay đổi vào cơ sở dữ liệu
            db.SaveChanges();

            return RedirectToAction("GioHang", "Home"); // Chuyển hướng tới trang giỏ hàng
        }
        public ActionResult XoaGioHang(int id, int mamau)
        {
            var email = Session["UserEmail"] as string;
            var user = db.KhachHangs.SingleOrDefault(k => k.Email.Contains(email));
            var giohang = db.GioHangs.SingleOrDefault(u => u.MaKhachHang == user.MaKhachHang);
            var sanpham = db.ChiTietGioHangs.SingleOrDefault(u=>u.MaSanPham == id && u.MaGioHang == giohang.MaGioHang && u.MaMauSac== mamau);
            db.ChiTietGioHangs.Remove(sanpham);
            db.SaveChanges();
            return RedirectToAction("GioHang", "Home");
        }

        public ActionResult DangXuat()
        {
            Session.Remove("UserEmail");
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public ActionResult HoSo(string tenKhachHang, string soDienThoai, string diaChi, string matKhauHienTai, string matKhauMoi, string xacNhanMatKhauMoi)
        {
            var email = Session["UserEmail"] as string;
            var user = db.KhachHangs.SingleOrDefault(k => k.Email.Contains(email));

            if (!string.IsNullOrEmpty(matKhauHienTai))
            {
               
                if (user.MatKhau == matKhauHienTai)
                {
                    if (matKhauMoi == xacNhanMatKhauMoi)
                    {
                        user.MatKhau = matKhauMoi;
                    }
                    else
                    {
                        ModelState.AddModelError("", "Xác nhận mật khẩu mới không khớp.");
                        return View(user);
                    }
                }
                else
                {
                    ModelState.AddModelError("MatKhauHienTai", "Mật khẩu hiện tại không đúng.");
                    return View(user);
                }
            }

            if (ModelState.IsValid)
            {
               
                user.TenKhachHang = tenKhachHang;
                user.SoDienThoai = soDienThoai;
                user.DiaChi = diaChi;
                
                db.SaveChanges();
                return RedirectToAction("HoSo","Home"); 
            }

            return View();
        }
        public ActionResult DatHang()
        {
          
            return RedirectToAction("DatHangThanhCong", "Home"); // Chuyển hướng tới trang xác nhận đặt hàng thành công
        }

        public ActionResult DangNhap()
        {

            return View(); 
        }
        [HttpPost]
        public ActionResult DangNhap(string Email, string MatKhau)
        {
            if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(MatKhau))
            {
                ModelState.AddModelError(string.Empty, "Vui lòng nhập đầy đủ thông tin.");
                return View();
            }

           
            var user = db.KhachHangs.FirstOrDefault(u => u.Email == Email && u.MatKhau == MatKhau);
            if (user != null)
            {
                Session["UserEmail"] = user.Email;
                return RedirectToAction("Index", "Home"); 
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Email hoặc mật khẩu không chính xác.");
                return View();
            }
        }
        [HttpPost]
        public JsonResult CapNhatGioHang(int productId, int quantity, int selectedColor)
        {
            var email = Session["UserEmail"] as string;
            var user = db.KhachHangs.SingleOrDefault(u => u.Email == email);
            if (user == null)
            {
                return Json(new { success = false, message = "Người dùng không tìm thấy." });
            }

            var giohang = db.GioHangs.SingleOrDefault(u => u.MaKhachHang == user.MaKhachHang);
            if (giohang == null)
            {
                return Json(new { success = false, message = "Giỏ hàng không tồn tại." });
            }

            var cartItem = db.ChiTietGioHangs
                .SingleOrDefault(ci => ci.MaGioHang == giohang.MaGioHang && ci.MaSanPham == productId && ci.MaMauSac == selectedColor);

            if (cartItem != null)
            {
                cartItem.SoLuong = quantity;
                db.SaveChanges();
                return Json(new { success = true, message = "Sản phẩm đã được cập nhật trong giỏ hàng." });
            }

            return Json(new { success = false, message = "Sản phẩm không tồn tại trong giỏ hàng." });
        }

        public ActionResult DangKy()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangKy(KhachHang model,string XacNhanMatKhau)
        {
            if (ModelState.IsValid)
            {

              
                 if (db.KhachHangs.Any(u=>u.Email==model.Email)) 
                 {
                     ModelState.AddModelError("Email", "Email đã tồn tại.");
                     return View(model);
                 }
                if (model.MatKhau != XacNhanMatKhau)
                {
                    ModelState.AddModelError("MatKhauNhapLai", "Mật khẩu không khớp.");
                    return View(model);
                }

                var user = new KhachHang
                    {
                        TenKhachHang = model.TenKhachHang,
                        Email = model.Email,
                        DiaChi = model.DiaChi,
                        SoDienThoai = model.SoDienThoai,
                        MatKhau = model.MatKhau
                };

                db.KhachHangs.Add(user);
                db.SaveChanges();
                var giohang = new GioHang
                {
                    MaKhachHang = user.MaKhachHang
                };
                db.GioHangs.Add(giohang);
                db.SaveChanges();
                

                
                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }
        [HttpPost]
        public ActionResult BinhLuan(int MaSP, string NoiDung, int DanhGia, int? page)
        {
            // Kiểm tra nếu Session["UserEmail"] là null hoặc không phải kiểu KhachHang
            if (Session["UserEmail"] == null )
            {
                // Nếu chưa đăng nhập, chuyển hướng đến trang đăng nhập
                return RedirectToAction("DangNhap", "Home");
            }
            var mySessionValue = Session["UserEmail"]?.ToString();
            // Khởi tạo đối tượng BinhLuan
            BinhLuan bl = new BinhLuan
            {
                MaSanPham = MaSP,
                Email = mySessionValue,  // Sử dụng thông tin từ đối tượng KhachHang
                NoiDung = NoiDung,
                DanhGia = DanhGia,
                NgayBinhLuan = DateTime.Now
            };

            // Thêm bình luận vào cơ sở dữ liệu
            db.BinhLuans.Add(bl);
            db.SaveChanges();

            // Chuyển hướng về trang chi tiết sản phẩm sau khi bình luận thành công
            return RedirectToAction("ChiTietSanPham", new { id = MaSP });
        }

    }
}