using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebStoreHonda.Models;

namespace WebStoreHonda.Controllers
{
    public class AdminController : Controller
    {
        StoreHondaEntities4 db = new StoreHondaEntities();
        // GET: Admin
        public ActionResult Index()
        {
            if (Session["Admin"] == null)
            {
                return RedirectToAction("Loi404", "Admin");
            }
            var totalCustomers = db.KhachHangs.Count();
            var totalProducts = db.SanPhams.Count();
            var totalDonHang = db.HoaDons.Count();
            ViewBag.TotalProducts = totalProducts;
            ViewBag.TotalCustomers = totalCustomers;
            ViewBag.TotalDonHang = totalDonHang;
            return View();
        }
        public ActionResult CustomerList()
        {
            var customers = db.KhachHangs.Take(1000).ToList();

            return View(customers);
        }
        public ActionResult ProductList()
        {
            var products = db.SanPhams.Take(1000).ToList();

            return View(products);
        }
        public ActionResult DonhangList()
        {
            var Donhang = db.HoaDons.Take(1000).ToList();

            return View(Donhang);
        }
           
        
        public ActionResult DangNhap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangNhap(string tenDangNhap, string matKhau)
        {
            // Tìm kiếm tài khoản admin với thông tin đăng nhập
            var admin = db.TaiKhoanAdmins.FirstOrDefault(a => a.TaiKhoan == tenDangNhap && a.MatKhau == matKhau);

            // Nếu tìm thấy admin, lưu thông tin vào Session và chuyển hướng đến trang Index (quản trị)
            if (admin != null)
            {
                Session["Admin"] = admin.TaiKhoan; // Lưu tên đăng nhập vào Session
                return RedirectToAction("Index");
            }

            // Nếu không tìm thấy, thông báo lỗi và hiển thị lại trang đăng nhập
            ViewBag.ErrorMessage = "Tên đăng nhập hoặc mật khẩu không đúng!";
            return View();
        }

        // Action xử lý đăng xuất
        public ActionResult DangXuat()
        {
            Session["Admin"] = null; // Xóa thông tin đăng nhập trong Session
            return RedirectToAction("DangNhap"); // Chuyển hướng về trang đăng nhập
        }
        public ActionResult QuanLyDanhMuc(string searchString, int page = 1)
        {
            if (Session["Admin"] == null)
            {
                return RedirectToAction("Loi404", "Admin");
            }

            int pageSize = 6; // Số lượng danh mục hiển thị trên mỗi trang

            // Lấy danh sách danh mục từ cơ sở dữ liệu
            var danhMucList = db.DanhMucs.AsQueryable();

            // Tìm kiếm theo tên danh mục
            if (!string.IsNullOrEmpty(searchString))
            {
                danhMucList = danhMucList.Where(dm => dm.TenDanhMuc.Contains(searchString));
            }

            // Đếm tổng số danh mục
            int totalCount = danhMucList.Count();
            int totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

            // Lấy danh sách danh mục theo trang
            var danhMucs = danhMucList.OrderBy(dm => dm.MaDanhMuc) // Sắp xếp theo MaDanhMuc
                                       .Skip((page - 1) * pageSize) // Bỏ qua các mục trước đó
                                       .Take(pageSize) // Lấy số mục tương ứng với trang
                                       .ToList();

            // Tạo ViewBag cho phân trang
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            ViewBag.SearchString = searchString; // Để giữ lại giá trị tìm kiếm

            return View(danhMucs);
        }
        [HttpGet]
        public ActionResult ThemDanhMuc()
        {
            if (Session["Admin"] == null)
            {
                return RedirectToAction("Loi404", "Admin");
            }
            return View();
        }

        [HttpPost]
        public ActionResult ThemDanhMuc(DanhMuc danhMuc)
        {
            if (ModelState.IsValid)
            {
                danhMuc.TrangThai = true;
                db.DanhMucs.Add(danhMuc);
                db.SaveChanges();
                return RedirectToAction("QuanLyDanhMuc");
            }
            return View(danhMuc);
        }

        // Action để sửa danh mục
        [HttpGet]
        public ActionResult ChinhSuaDanhMuc(int id)
        {
            if (Session["Admin"] == null)
            {
                return RedirectToAction("Loi404", "Admin");
            }
            var danhMuc = db.DanhMucs.Find(id);
            if (danhMuc == null)
            {
                return HttpNotFound();
            }
            return View(danhMuc);
        }

        [HttpPost]
        public ActionResult ChinhSuaDanhMuc(DanhMuc danhMuc)
        {
            if (ModelState.IsValid)
            {
                danhMuc.TrangThai = true;
                db.Entry(danhMuc).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("QuanLyDanhMuc");
            }
            return View(danhMuc);
        }

        // Action để dừng hoạt động danh mục
        public ActionResult XoaDanhMuc(int id)
        {
            var danhMuc = db.DanhMucs.Find(id);
            if (danhMuc != null)
            {
                danhMuc.TrangThai = false; // Thay đổi trạng thái thành không hoạt động
                db.SaveChanges();
            }
            return RedirectToAction("QuanLyDanhMuc");
        }

        // Action để kích hoạt lại danh mục
        public ActionResult KichHoatDanhMuc(int id)
        {
            var danhMuc = db.DanhMucs.Find(id);
            if (danhMuc != null)
            {
                danhMuc.TrangThai = true; // Thay đổi trạng thái thành hoạt động
                db.SaveChanges();
            }
            return RedirectToAction("QuanLyDanhMuc");
        }

        public ActionResult QuanLySanPham(string searchString, int page = 1, int pageSize = 5)
        {

            if (Session["Admin"] == null)
            {
                return RedirectToAction("Loi404", "Admin");
            }

            // Lấy danh sách sản phẩm từ database
            var sanPhams = db.SanPhams.AsQueryable();

            // Tìm kiếm theo tên sản phẩm
            if (!string.IsNullOrEmpty(searchString))
            {
                sanPhams = sanPhams.Where(s => s.TenSanPham.Contains(searchString));
            }
             
            // Phân trang
            var totalRecords = sanPhams.Count();
            var totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);
            var products = sanPhams.OrderBy(s => s.MaSanPham)
                                    .Skip((page - 1) * pageSize)
                                    .Take(pageSize)
                                    .ToList();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            ViewBag.SearchString = searchString;

            return View(products);
        }
        public ActionResult ThemSanPham()
        {
            if (Session["Admin"] == null)
            {
                return RedirectToAction("Loi404", "Admin");
            }
            var mauSacList = db.MauSacs.ToList();
            ViewBag.MauSacList = mauSacList;

            // Lấy danh sách danh mục
            var danhMucList = db.DanhMucs.ToList();
            ViewBag.DanhMucList = new SelectList(danhMucList, "MaDanhMuc", "TenDanhMuc");

            return View();
        }

        [HttpPost]
        public ActionResult ThemSanPham(SanPham sanPham, int[] MaMauSac, HttpPostedFileBase HinhDaiDien)
        {
            if (ModelState.IsValid)
            {
                sanPham.TrangThai = true;
                // Lưu sản phẩm
                db.SanPhams.Add(sanPham);
                db.SaveChanges(); // Lưu sản phẩm trước để có MaSanPham

                // Tạo thư mục cho sản phẩm
                var productImagePath = Path.Combine(Server.MapPath("~/assets/images/product/"), sanPham.MaSanPham.ToString());
                Directory.CreateDirectory(productImagePath); // Tạo thư mục cho sản phẩm nếu chưa tồn tại

                // Lưu hình ảnh đại diện
                if (HinhDaiDien != null && HinhDaiDien.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(HinhDaiDien.FileName);
                    var path = Path.Combine(productImagePath, fileName);
                    HinhDaiDien.SaveAs(path);
                    sanPham.HinhAnh = fileName; // Lưu tên file vào thuộc tính HinhAnh
                }
                db.Entry(sanPham).State = EntityState.Modified;
                db.SaveChanges(); // Lưu cập nhật tên hình ảnh vào DB
                // Lưu thông tin màu sắc và hình ảnh tương ứng
                if (MaMauSac != null)
                {
                    foreach (var maMau in MaMauSac)
                    {
                        // Tạo thư mục cho màu sắc
                        var mauSacPath = Path.Combine(productImagePath, maMau.ToString());
                        Directory.CreateDirectory(mauSacPath); // Tạo thư mục cho màu sắc

                        // Giả sử bạn sẽ lấy hình ảnh từ các input hình ảnh màu sắc
                        var hinhMauSacFile = Request.Files[$"HinhMauSac_{maMau}"];
                        if (hinhMauSacFile != null && hinhMauSacFile.ContentLength > 0)
                        {
                            var hinhMauSacFileName = Path.GetFileName(hinhMauSacFile.FileName);
                            var hinhMauSacPath = Path.Combine(mauSacPath, hinhMauSacFileName);
                            hinhMauSacFile.SaveAs(hinhMauSacPath); // Lưu hình ảnh vào thư mục tương ứng

                            // Lưu thông tin vào bảng SanPham_MauSac
                            var sanPhamMauSac = new SanPham_MauSac
                            {
                                MaSanPham = sanPham.MaSanPham,
                                MaMauSac = maMau,
                                HinhMauSac = hinhMauSacFileName, // Lưu tên hình ảnh màu sắc
                                TrangThai = true // hoặc false tùy thuộc vào yêu cầu
                            };
                            db.SanPham_MauSac.Add(sanPhamMauSac);
                            db.SaveChanges();
                        }
                    }
                    db.SaveChanges(); // Lưu tất cả thông tin vào DB
                }


                return RedirectToAction("QuanLySanPham");
            }

            var mauSacList = db.MauSacs.ToList();
            ViewBag.MauSacList = mauSacList;

            // Lấy danh sách danh mục
            var danhMucList = db.DanhMucs.ToList();
            ViewBag.DanhMucList = new SelectList(danhMucList, "MaDanhMuc", "TenDanhMuc");

            return View(sanPham);
        }
        [HttpGet]
        public ActionResult SuaSanPham(int id)
        {
            if (Session["Admin"] == null)
            {
                return RedirectToAction("Loi404", "Admin");
            }
            var sanPham = db.SanPhams.Find(id); // Lấy sản phẩm từ cơ sở dữ liệu
            if (sanPham == null)
            {
                return HttpNotFound();
            }

            // Lấy danh sách màu sắc
            ViewBag.MauSacList = db.MauSacs.ToList();

            // Lấy danh sách danh mục
            var danhMucList = db.DanhMucs.ToList();
            ViewBag.DanhMucList = new SelectList(danhMucList, "MaDanhMuc", "TenDanhMuc");

            return View(sanPham); // Trả lại view với model là sản phẩm hiện tại
        }
        public ActionResult XoaSanPham(int id)
        {
            // Kiểm tra nếu admin chưa đăng nhập thì chuyển đến trang lỗi
            if (Session["Admin"] == null)
            {
                return RedirectToAction("Loi404", "Admin");
            }

            // Tìm sản phẩm theo id
            var sanPham = db.SanPhams.Find(id);
            if (sanPham != null)
            {
                // Xóa sản phẩm từ cơ sở dữ liệu
                db.SanPhams.Remove(sanPham);
                db.SaveChanges();
            }
            return RedirectToAction("QuanLySanPham");
        }

        [HttpPost]
        public ActionResult SuaSanPham(SanPham sanPham, int[] MaMauSac, HttpPostedFileBase HinhDaiDien)
        {
            if (ModelState.IsValid)
            {
                // Cập nhật thông tin sản phẩm
                var sanPhamToUpdate = db.SanPhams.Find(sanPham.MaSanPham);
                if (sanPhamToUpdate != null)
                {
                    sanPhamToUpdate.TenSanPham = sanPham.TenSanPham;
                    sanPhamToUpdate.Gia = sanPham.Gia;
                    sanPhamToUpdate.TrangThai = true;

                    // Lưu hình ảnh đại diện nếu có
                    var productImagePath = Path.Combine(Server.MapPath("~/assets/images/product/"), sanPhamToUpdate.MaSanPham.ToString());
                    if (!Directory.Exists(productImagePath))
                    {
                        Directory.CreateDirectory(productImagePath); // Tạo thư mục cho sản phẩm nếu chưa tồn tại
                    }

                    if (HinhDaiDien != null && HinhDaiDien.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(HinhDaiDien.FileName);
                        var path = Path.Combine(productImagePath, fileName);
                        HinhDaiDien.SaveAs(path);
                        sanPhamToUpdate.HinhAnh = fileName; // Lưu tên file vào thuộc tính HinhAnh
                    }

                    db.Entry(sanPhamToUpdate).State = EntityState.Modified;
                    db.SaveChanges(); // Lưu thay đổi vào DB

                 

                    // Lưu thông tin màu sắc và hình ảnh tương ứng
                    if (MaMauSac != null)
                    {
                        // Xóa màu sắc cũ nếu cần
                        var sanPhamMauSacCu = db.SanPham_MauSac.Where(x => x.MaSanPham == sanPhamToUpdate.MaSanPham).ToList();
                        db.SanPham_MauSac.RemoveRange(sanPhamMauSacCu);
                        db.SaveChanges();
                        foreach (var maMau in MaMauSac)
                        {
                            // Tạo thư mục cho màu sắc
                            var mauSacPath = Path.Combine(productImagePath, maMau.ToString());
                            Directory.CreateDirectory(mauSacPath); // Tạo thư mục cho màu sắc

                            // Giả sử bạn sẽ lấy hình ảnh từ các input hình ảnh màu sắc
                            var hinhMauSacFile = Request.Files[$"HinhMauSac_{maMau}"];
                            if (hinhMauSacFile != null && hinhMauSacFile.ContentLength > 0)
                            {
                                var hinhMauSacFileName = Path.GetFileName(hinhMauSacFile.FileName);
                                var hinhMauSacPath = Path.Combine(mauSacPath, hinhMauSacFileName);
                                hinhMauSacFile.SaveAs(hinhMauSacPath); // Lưu hình ảnh vào thư mục tương ứng

                                // Lưu thông tin vào bảng SanPham_MauSac
                                var sanPhamMauSac = new SanPham_MauSac
                                {
                                    MaSanPham = sanPhamToUpdate.MaSanPham,
                                    MaMauSac = maMau,
                                    HinhMauSac = hinhMauSacFileName, // Lưu tên hình ảnh màu sắc
                                    TrangThai = true // hoặc false tùy thuộc vào yêu cầu
                                };
                                db.SanPham_MauSac.Add(sanPhamMauSac);
                                db.SaveChanges();
                            }
                        }
                    }
                }

                return RedirectToAction("QuanLySanPham");
            }

            // Nếu model không hợp lệ, trả lại view với model
            ViewBag.MauSacList = db.MauSacs.ToList(); // Lấy lại danh sách màu sắc
            var danhMucList = db.DanhMucs.ToList();
            ViewBag.DanhMucList = new SelectList(danhMucList, "MaDanhMuc", "TenDanhMuc");
            return View(sanPham);
        }

        public ActionResult NgungBan(int id)
        {
            var sanPham = db.SanPhams.Find(id);
            if (sanPham != null)
            {
                sanPham.TrangThai = false; // Đặt trạng thái thành ngưng bán
                db.SaveChanges();
            }
            return RedirectToAction("QuanLySanPham");
        }

        public ActionResult KichHoatBan(int id)
        {
            var sanPham = db.SanPhams.Find(id);
            if (sanPham != null)
            {
                sanPham.TrangThai = true; // Đặt trạng thái thành đang bán
                db.SaveChanges();
            }
            return RedirectToAction("QuanLySanPham");
        }

        public ActionResult QuanLyMauSac(string searchString, int page = 1)
        {

            if (Session["Admin"] == null)
            {
                return RedirectToAction("Loi404", "Admin");
            }
            ViewBag.SearchString = searchString;

            var mauSacs = db.MauSacs.AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                mauSacs = mauSacs.Where(m => m.TenMauSac.Contains(searchString));
            }

            // Phân trang
            int pageSize = 5; // Số lượng item trên mỗi trang
            int totalCount = mauSacs.Count();
            ViewBag.TotalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            ViewBag.CurrentPage = page;

            var result = mauSacs.OrderBy(m => m.MaMauSac)
                                .Skip((page - 1) * pageSize)
                                .Take(pageSize)
                                .ToList();

            return View(result);
        }
        public ActionResult ThemMauSac()
        {

            if (Session["Admin"] == null)
            {
                return RedirectToAction("Loi404", "Admin");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ThemMauSac(MauSac mauSac)
        {
            if (ModelState.IsValid)
            {
                db.MauSacs.Add(mauSac);
                db.SaveChanges();
                return RedirectToAction("QuanLyMauSac");
            }
            return View(mauSac);
        }

        public ActionResult ChinhSuaMauSac(int id)
        {

            if (Session["Admin"] == null)
            {
                return RedirectToAction("Loi404", "Admin");
            }
            var mauSac = db.MauSacs.Find(id);
            if (mauSac == null)
            {
                return HttpNotFound();
            }
            return View(mauSac);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChinhSuaMauSac(MauSac mauSac)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mauSac).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("QuanLyMauSac");
            }
            return View(mauSac);
        }

        public ActionResult QuanLyKhachHang(string searchString, int page = 1, int pageSize = 5)
        {
            if (Session["Admin"] == null)
            {
                return RedirectToAction("Loi404", "Admin");
            }

            // Lấy danh sách khách hàng từ database
            var khachHangs = db.KhachHangs.AsQueryable();

            // Tìm kiếm theo tên hoặc email
            if (!string.IsNullOrEmpty(searchString))
            {
                khachHangs = khachHangs.Where(k => k.TenKhachHang.Contains(searchString) || k.Email.Contains(searchString));
            }

            // Phân trang
            var totalRecords = khachHangs.Count();
            var totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);
            var customers = khachHangs.OrderBy(k => k.MaKhachHang)
                                       .Skip((page - 1) * pageSize)
                                       .Take(pageSize)
                                       .ToList();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            ViewBag.SearchString = searchString;

            return View(customers);
        }


        public ActionResult QuanLyDonHang(string searchString, int page = 1)
        {

            if (Session["Admin"] == null)
            {
                return RedirectToAction("Loi404", "Admin");
            }
            // Tìm kiếm đơn hàng
            var orders = db.HoaDons.AsQueryable(); // Giả sử bạn có bảng `Orders`

            if (!string.IsNullOrEmpty(searchString))
            {
                orders = orders.Where(o => o.KhachHang.TenKhachHang.Contains(searchString));
            }

            // Phân trang
            int pageSize = 10;
            int totalOrders = orders.Count();
            var paginatedOrders = orders.OrderByDescending(o => o.NgayLap)
                                        .Skip((page - 1) * pageSize)
                                        .Take(pageSize)
                                        .ToList();

            ViewBag.SearchString = searchString;
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling((double)totalOrders / pageSize);

            return View(paginatedOrders);
        }

        // Duyệt đơn hàng
        public ActionResult DuyetDon(int id)
        {
            var order = db.HoaDons.Find(id); // Tìm đơn hàng theo id
            if (order != null && order.TrangThai == "Đang chờ xử lý")
            {
                order.TrangThai = "Đã duyệt";
                db.SaveChanges();
            }
            return RedirectToAction("QuanLyDonHang");
        }

        // Hủy đơn hàng
        public ActionResult HuyDon(int id)
        {
            var order = db.HoaDons.Find(id); // Tìm đơn hàng theo id
            if (order != null && order.TrangThai == "Đang chờ xử lý")
            {
                order.TrangThai = "Đã hủy";
                db.SaveChanges();
            }
            return RedirectToAction("QuanLyDonHang");
        }

        public ActionResult Loi404()
        {
            return View();
      
        
        }
        public ActionResult ThongKeDonHang()
        {
            return View();
        }

    }


}