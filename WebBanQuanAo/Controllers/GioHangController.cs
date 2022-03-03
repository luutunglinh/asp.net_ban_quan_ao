using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanQuanAo.Models;
namespace WebBanQuanAo.Controllers
{
    public class GioHangController : Controller
    {
        //
      
        // GET: /GioHang/
        BanQuanAoEntities2 db = new BanQuanAoEntities2();
        public List<ItemGioHang> LayGioHang()
        {
            // giỏ hàng đả tồn tại
            List<ItemGioHang> lstGioHang = Session["GioHang"] as List<ItemGioHang>;
            if (lstGioHang == null)
            {
                // nếu session giỏ hàng chưa tồn tại thì khởi tại giỏ hàng 
                lstGioHang = new List<ItemGioHang>();
                Session["GioHang"] = lstGioHang;

            }
            return lstGioHang;
        }


        // thêm giỏ hàng thông thường (Load lại trang)
        public ActionResult ThemGioHang(int IdSanPham, string strUrl)
        {
            // kiểm tra sản phẩm có tồn tại trong csdl ko
            SanPham sanpham = db.SanPhams.SingleOrDefault(n => n.IdSanPham == IdSanPham);
            if (sanpham == null)
            {
                // trang đường dẩn ko hợp lệ
                Response.StatusCode = 404;
                return null;
            }

            // lấy giỏ hàng 
            List<ItemGioHang> lstGioHang = LayGioHang();
            // trường hợp đả tồn tại một sản phẩm trên giỏ hàng
            ItemGioHang sanphamcheck = lstGioHang.SingleOrDefault(n => n.IdSanPham == IdSanPham);
            if (sanphamcheck != null)
            {
                // kiểm tra số lượng sản phẩm tồn
                if (sanpham.SoLuongTon < sanphamcheck.SoLuong)
                {
                    return View("ThongBao");
                }
                sanphamcheck.SoLuong++;
                sanphamcheck.TongTien = sanphamcheck.SoLuong * sanphamcheck.DonGia;
                return Redirect(strUrl);
            }
            ItemGioHang itemGH = new ItemGioHang(IdSanPham);
            if (sanpham.SoLuongTon < itemGH.SoLuong)
            {
                return View("ThongBao");
            }
            lstGioHang.Add(itemGH);
            db.SaveChanges();
            return Redirect(strUrl);
        }
        // xây dựng phương thức tính số lương
        public double TinhTongSoLuong()
        {
            List<ItemGioHang> lstGioHang = Session["GioHang"] as List<ItemGioHang>;
            if (lstGioHang == null)
            {
                return 0;
            }
            return lstGioHang.Sum(n => n.SoLuong);
        }
        // phương thức tính tiền
        public decimal TongTien()
        {
            List<ItemGioHang> lstGioHang = Session["GioHang"] as List<ItemGioHang>;
            if (lstGioHang == null)
            {
                return 0;
            }
            return lstGioHang.Sum(n => n.TongTien);
        }
        // xây dựng giỏ hàng partial
        public ActionResult GioHangPartial()
        {
            if (TinhTongSoLuong() == 0)
            {
                ViewBag.TongSoLuong = 0;
                ViewBag.TongTien = 0;
                return PartialView();
            }
            ViewBag.TongSoLuong = TinhTongSoLuong();
            ViewBag.TongTien = TongTien();
            return PartialView();
        }

        // Xem giỏ hàng
        public ActionResult XemGioHang()
        {
            List<ItemGioHang> lstGioHang = LayGioHang();
            return View(lstGioHang);
        }


        [HttpGet]
        public ActionResult SuaGioHang(int IdSanPham)
        {
            // kiểm tra sản phẩm có tồn tại ko
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            // kiểm tra sản phẩm có tồn tại trong csdl ko
            SanPham sanpham = db.SanPhams.SingleOrDefault(n => n.IdSanPham == IdSanPham);
            if (sanpham == null)
            {
                // trang đường dẩn ko hợp lệ
                Response.StatusCode = 404;
                return null;
            }
            // lấy list giỏ hàng từ session
            List<ItemGioHang> lstGioHang = LayGioHang();
            // kiểm tra sản phẩm có tồn tại trong giỏ hàng chưa
            ItemGioHang sanphamcheck = lstGioHang.SingleOrDefault(n => n.IdSanPham == IdSanPham);
            if (sanphamcheck == null)
            {
                return RedirectToAction("Index", "Home");
            }
            // Lấy list giỏ hàng trên giao diện
            ViewBag.GioHang = lstGioHang;
            // nếu sản phẩm đả tồn tại
            return View(sanphamcheck);
        }
        // cập nhật giỏ hàng
        [HttpPost]
        public ActionResult CapNhatGioHang(ItemGioHang itemGH)
        {
            // kiểm tra số lượng tồn
            SanPham sanphamcheck = db.SanPhams.SingleOrDefault(n => n.IdSanPham == itemGH.IdSanPham);
            if (sanphamcheck.SoLuongTon < itemGH.SoLuong)
            {
                return View("ThongBao");
            }
            // cập nhật số lượng trong session giỏ hàng
            List<ItemGioHang> lstGH = LayGioHang();
            // lấy sản phẩm cập nhật từ  trong list<GioHang>
            ItemGioHang itemGHUpdate = lstGH.Find(n => n.IdSanPham == itemGH.IdSanPham);
            // cập nhật lại số lượng và tổng tiền
            itemGHUpdate.SoLuong = itemGH.SoLuong;
            itemGHUpdate.TongTien = itemGHUpdate.SoLuong * itemGHUpdate.DonGia;
            return RedirectToAction("XemGioHang");
        }


        public ActionResult XoaGioHang(int IdSanPham)
        {
            // kiểm tra sản phẩm có tồn tại ko
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            // kiểm tra sản phẩm có tồn tại trong csdl ko
            SanPham sanpham = db.SanPhams.SingleOrDefault(n => n.IdSanPham == IdSanPham);
            if (sanpham == null)
            {
                // trang đường dẩn ko hợp lệ
                Response.StatusCode = 404;
                return null;
            }
            // lấy list giỏ hàng từ session
            List<ItemGioHang> lstGioHang = LayGioHang();
            // kiểm tra sản phẩm có tồn tại trong giỏ hàng chưa
            ItemGioHang sanphamcheck = lstGioHang.SingleOrDefault(n => n.IdSanPham == IdSanPham);
            if (sanphamcheck == null)
            {
                return RedirectToAction("Index", "Home");
            }
            // xóa sản phẩm 
            lstGioHang.Remove(sanphamcheck);
            return RedirectToAction("XemGioHang");
        }
        // xậy dựng action đặt hàng
        public ActionResult DatHang(KhachHang khachhang)
        {
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            KhachHang KH = new KhachHang();
            if (Session["user"] == null)
            {
                // thêm khách hàng đối với khách hàng vãng lai(chưa có tài khoản)
                KH = khachhang;
                db.KhachHangs.Add(KH);
                db.SaveChanges();
            }
         
            // thêm đơn hàng
            DonDatHang ddh = new DonDatHang();
            //ddh.IdKH =  int.Parse(KH.IdKH.ToString());
            ddh.IdKH = KH.IdKH;
            ddh.NgayDat = DateTime.Now;
            ddh.TinhTrangGiaoHang = false;
            ddh.DaThanhToan = false;
            ddh.DaHuy = false;
            ddh.DaXoa = false;
            db.DonDatHangs.Add(ddh);
            db.SaveChanges();
            // thêm chi tiết đơn đặt hàng
            List<ItemGioHang> lstGH = LayGioHang();
            foreach (var item in lstGH)
            {
                ChiTietDonDatHang ctdh = new ChiTietDonDatHang();
                ctdh.IdDDH = ddh.IdDDH;
                ctdh.IdSanPham = item.IdSanPham;
                ctdh.TenSanPham = item.TenSanPham;
                ctdh.SoLuong = item.SoLuong;
                ctdh.DonGia = item.DonGia;
                db.ChiTietDonDatHangs.Add(ctdh);
            }
        
            Session["GioHang"] = null;
            db.SaveChanges();
            return View("ThongBao1");
        }







        // thêm giỏ hàng ajax
        public ActionResult ThemGioHangAjax(int IdSanPham, string strUrl)
        {
            // kiểm tra sản phẩm có tồn tại trong csdl ko
            SanPham sanpham = db.SanPhams.SingleOrDefault(n => n.IdSanPham == IdSanPham);
            if (sanpham == null)
            {
                // trang đường dẩn ko hợp lệ
                Response.StatusCode = 404;
                return null;
            }
            // lấy giỏ hàng 

            List<ItemGioHang> lstGioHang = LayGioHang();
            // trường hợp đả tồn tại một sản phẩm trên giỏ hàng
            ItemGioHang sanphamcheck = lstGioHang.SingleOrDefault(n => n.IdSanPham == IdSanPham);
            if (sanphamcheck != null)
            {
                // kiểm tra số lượng sản phẩm tồn
                if (sanpham.SoLuongTon < sanphamcheck.SoLuong)
                {
                    return Content("<script>alert(\"Sản phẩm đả hết hàng !\")</script>");
                }
                sanphamcheck.SoLuong++;
                sanphamcheck.TongTien = sanphamcheck.SoLuong * sanphamcheck.DonGia;
                ViewBag.TongSoLuong = TinhTongSoLuong();
                ViewBag.TongTien = TongTien();
                return PartialView("GioHangPartial");
            }

            ItemGioHang itemGH = new ItemGioHang(IdSanPham);
            if (sanpham.SoLuongTon < itemGH.SoLuong)
            {
                return Content("<script>alert(\"Sản phẩm đả hết hàng !\")</script>");
            }
            lstGioHang.Add(itemGH);
            ViewBag.TongSoLuong = TinhTongSoLuong();
            ViewBag.TongTien = TongTien();
            return PartialView("GioHangPartial");
        }
	}
}