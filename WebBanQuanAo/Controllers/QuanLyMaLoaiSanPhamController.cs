using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebBanQuanAo.Models;
namespace WebBanQuanAo.Controllers
{
    public class QuanLyMaLoaiSanPhamController : Controller
    {
        //
        // GET: /QuanLyDanhMucCap2/
        BanQuanAoEntities2 db = new BanQuanAoEntities2();
        public ActionResult Index()
        {
            return View(db.MaLoaiSanPhams.OrderBy(n => n.IdMLSP));
        }
        [HttpGet]
        public ActionResult TaoMoi()
        {
            return View();
        }
        [HttpPost]
        public ActionResult TaoMoi(MaLoaiSanPham mlsp)
        {
            db.MaLoaiSanPhams.Add(mlsp);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult ChinhSua(int IdMLSP)
        {
            if (IdMLSP == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            MaLoaiSanPham nsx = db.MaLoaiSanPhams.SingleOrDefault(n => n.IdMLSP == IdMLSP);
            if (nsx == null)
            {
                return HttpNotFound();
            }
            return View(nsx);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult ChinhSua(MaLoaiSanPham ncc)
        {
            db.MaLoaiSanPhams.Add(ncc);
            db.Entry(ncc).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Xoa(int? IdMLSP)
        {
            if (IdMLSP == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            MaLoaiSanPham nsx = db.MaLoaiSanPhams.SingleOrDefault(n => n.IdMLSP == IdMLSP);
            if (nsx == null)
            {
                return HttpNotFound();
            }
            return View(nsx); ;
        }
        [HttpPost]
        public ActionResult Xoa(int IdMLSP)
        {
            if (IdMLSP == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MaLoaiSanPham nsx = db.MaLoaiSanPhams.SingleOrDefault(n => n.IdMLSP == IdMLSP);
            if (nsx == null)
            {
                return HttpNotFound();
            }
            db.MaLoaiSanPhams.Remove(nsx);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
	}
}