using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebBanQuanAo.Models;

namespace WebBanQuanAo.Controllers
{
    public class QuanLyDanhMucSanPhamController : Controller
    {
        //
        // GET: /QuanLyDanhMucSanPham/
        BanQuanAoEntities2 db = new BanQuanAoEntities2();
        public ActionResult Index()
        {
            return View(db.LoaiSanPhams.OrderBy(n => n.IdLoaiSanPham));
        }
         [HttpGet]
        public ActionResult TaoMoi()
        {
            return View();
        }
        [HttpPost]
        public ActionResult TaoMoi(LoaiSanPham loaisanpham)
        {
            db.LoaiSanPhams.Add(loaisanpham);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult ChinhSua(int IdLoaiSanPham)
        {
            if (IdLoaiSanPham == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            LoaiSanPham nsx = db.LoaiSanPhams.SingleOrDefault(n => n.IdLoaiSanPham == IdLoaiSanPham);
            if (nsx == null)
            {
                return HttpNotFound();
            }
            return View(nsx);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult ChinhSua(LoaiSanPham loaisanpham)
        {
            db.LoaiSanPhams.Add(loaisanpham);
            db.Entry(loaisanpham).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Xoa(int? IdLoaiSanPham)
        {
            if (IdLoaiSanPham == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            LoaiSanPham nsx = db.LoaiSanPhams.SingleOrDefault(n => n.IdLoaiSanPham == IdLoaiSanPham);
            if (nsx == null)
            {
                return HttpNotFound();
            }
            return View(nsx);
        }
        [HttpPost]
        public ActionResult Xoa(int IdLoaiSanPham)
        {
            if (IdLoaiSanPham == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiSanPham nsx = db.LoaiSanPhams.SingleOrDefault(n => n.IdLoaiSanPham == IdLoaiSanPham); if (nsx == null)
            {
                return HttpNotFound();
            }
            db.LoaiSanPhams.Remove(nsx);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
	}
}