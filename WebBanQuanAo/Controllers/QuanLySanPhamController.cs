using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebBanQuanAo.Models;

namespace WebBanQuanAo.Controllers
{
    public class QuanLySanPhamController : Controller
    {
        //
        BanQuanAoEntities2 db = new BanQuanAoEntities2();
        // GET: /QuanLySanPham/
        public ActionResult Index()
        {
            return View(db.SanPhams.Where(n => n.DaXoa == false).OrderByDescending(n => n.IdSanPham));
        }
        [HttpGet]
        public ActionResult TaoMoiSanPham()
        {
            //load dropdownlist nhà cung cấp, loại sản phẩm, nhà sản xuất
            ViewBag.IdMLSP = new SelectList(db.MaLoaiSanPhams.OrderBy(n => n.TenLSP), "IdMLSP", "TenLSP");
            ViewBag.IdLoaiSanPham = new SelectList(db.LoaiSanPhams.OrderBy(n => n.IdLoaiSanPham), "IdLoaiSanPham", "TenLoai");

            return View();
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult TaoMoiSanPham(SanPham sanpham, HttpPostedFileBase[] HinhAnh)
        {
            //load dropdownlist nhà cung cấp, loại sản phẩm, nhà sản xuất
            ViewBag.IdMLSP = new SelectList(db.MaLoaiSanPhams.OrderBy(n => n.TenLSP), "IdMLSP", "TenLSP");
            ViewBag.IdLoaiSanPham = new SelectList(db.LoaiSanPhams.OrderBy(n => n.IdLoaiSanPham), "IdLoaiSanPham", "TenLoai");
            //Nếu thư mục chứa hình ảnh đó rồi thì xuất ra thông báo
            db.SanPhams.Add(sanpham);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult ChinhSuaSanPham(int? IdSanPham)
        {
            // lấy sản phẩm cần chỉnh dựa vào id
            if (IdSanPham == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            SanPham sanpham = db.SanPhams.SingleOrDefault(n => n.IdSanPham == IdSanPham);
            if (sanpham == null)
            {
                return HttpNotFound();
            }

            //load dropdownlist nhà cung cấp, loại sản phẩm, nhà sản xuất
            ViewBag.IdMLSP = new SelectList(db.MaLoaiSanPhams.OrderBy(n => n.TenLSP), "IdMLSP", "TenLSP", sanpham.IdSanPham);
            ViewBag.IdLoaiSanPham = new SelectList(db.LoaiSanPhams.OrderBy(n => n.IdLoaiSanPham), "IdLoaiSanPham", "TenLoai", sanpham.IdLoaiSanPham);

            return View(sanpham);
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult ChinhSuaSanPham(SanPham model)
        {

            ViewBag.IdMLSP = new SelectList(db.MaLoaiSanPhams.OrderBy(n => n.TenLSP), "IdMLSP", "TenLSP", model.IdSanPham);
            ViewBag.IdLoaiSanPham = new SelectList(db.LoaiSanPhams.OrderBy(n => n.IdLoaiSanPham), "IdLoaiSanPham", "TenLoai", model.IdLoaiSanPham);
            // nếu dử liệu đầu vào ok
            db.Entry(model).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult XoaSanPham(int? IdSanPham)
        {
            // lấy sản phẩm cần chỉnh dựa vào id
            if (IdSanPham == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            SanPham sanpham = db.SanPhams.SingleOrDefault(n => n.IdSanPham == IdSanPham);
            if (sanpham == null)
            {
                return HttpNotFound();
            }

            //load dropdownlist nhà cung cấp, loại sản phẩm, nhà sản xuất
            ViewBag.IdMLSP = new SelectList(db.MaLoaiSanPhams.OrderBy(n => n.TenLSP), "IdMLSP", "TenLSP", sanpham.IdSanPham);
            ViewBag.IdLoaiSanPham = new SelectList(db.LoaiSanPhams.OrderBy(n => n.IdLoaiSanPham), "IdLoaiSanPham", "TenLoai", sanpham.IdLoaiSanPham);
         
            return View(sanpham);
        }
        [HttpPost]
        public ActionResult XoaSanPham(int IdSanPham)
        {
            if (IdSanPham == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanpham = db.SanPhams.SingleOrDefault(n => n.IdSanPham == IdSanPham);
            if (sanpham == null)
            {
                return HttpNotFound();
            }
            db.SanPhams.Remove(sanpham);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
	}
}