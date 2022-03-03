using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanQuanAo.Models;

namespace WebBanQuanAo.Controllers
{
    public class QuanLyNhapHangController : Controller
    {
        //
        BanQuanAoEntities2 db = new BanQuanAoEntities2();
        // GET: /QuanLyNhapHang/
        [HttpGet]
        public ActionResult NhapHang()
        {
           
            ViewBag.IdMLSP = db.MaLoaiSanPhams;
            ViewBag.ListSanPham = db.SanPhams;
            return View();
        }
        [HttpPost]
        public ActionResult NhapHang(PhieuNhap model,IEnumerable<ChiTietPhieuNhap> lstModel )
        {
          ViewBag.IdMLSP = db.MaLoaiSanPhams;
            ViewBag.ListSanPham = db.SanPhams;
            model.DaXoa = false;
            db.PhieuNhaps.Add(model);
            db.SaveChanges();
            // save changes để lấy đc id phiếu nhập
            SanPham sanpham;
            foreach (var item in lstModel)
            {
                // cập nhật số lượng tồn
                sanpham = db.SanPhams.Single(n=> n.IdSanPham==item.IdSanPham);
                sanpham.SoLuongTon += item.SoLuongNhap;
                // gán mã phiếu nhập cho phiếu nhập
                item.IdPN = model.IdPN;
            }
            db.ChiTietPhieuNhaps.AddRange(lstModel);
            db.SaveChanges();
            return View();
        }
        [HttpGet]
        public ActionResult SanPhamHetHang()
        {
            // liệt kệ danh sách sản phẩm gần hết hàn
            var lstSanPham = db.SanPhams.Where(n => n.DaXoa == false && n.SoLuongTon <= 15);
            return View(lstSanPham);
        }
        // tạo view nhập hàng
        [HttpGet]
        public ActionResult NhapHangDon(int? IdSanPham)
        {
            ViewBag.IdMLSP = new SelectList(db.MaLoaiSanPhams.OrderByDescending(n=> n.TenLSP),"IdMLSP","TenLSP");
            if (IdSanPham == null)
            {
                 Response.StatusCode = 404;
                 return null;
            }
            SanPham sanpham = db.SanPhams.SingleOrDefault(n => n.IdSanPham==IdSanPham);
            if (IdSanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanpham);
        }
        [HttpPost]
        public ActionResult NhapHangDon(PhieuNhap model, ChiTietPhieuNhap ctpn)
        {
            ViewBag.IdMLSP = new SelectList(db.MaLoaiSanPhams.OrderByDescending(n=> n.TenLSP),"IdMLSP","TenLSP");
            model.DaXoa = false;
            model.NgayNhap = DateTime.Now;
            db.PhieuNhaps.Add(model);
            db.SaveChanges();
            // save changes để lấy đc id phiếu nhập
            ctpn.IdPN = model.IdPN;
            // cập nhật tồn
            SanPham sanpham = db.SanPhams.Single(n => n.IdSanPham == ctpn.IdSanPham);
            sanpham.SoLuongTon += ctpn.SoLuongNhap;
            sanpham.DonGia = ctpn.DonGiaNhap;
            db.ChiTietPhieuNhaps.Add(ctpn);
            db.SaveChanges();
            return View(sanpham);
        }
        //Giải phóng biến cho vùng nhớ
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (db != null)
                    db.Dispose();
                db.Dispose();
            }
            base.Dispose(disposing);
        }
	
	}
}