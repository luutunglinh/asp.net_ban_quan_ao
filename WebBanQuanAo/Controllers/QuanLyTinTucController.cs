using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebBanQuanAo.Models;

namespace WebBanQuanAo.Controllers
{
    public class QuanLyTinTucController : Controller
    {
        //
        // GET: /QuanLyTinTuc/
        BanQuanAoEntities2 db = new BanQuanAoEntities2();
        public ActionResult Index()
        {
            return View(db.TinTucs.OrderByDescending(n => n.IdTinTuc));
        }
        [HttpGet]
        public ActionResult ThemTinTuc()
        {
            return View();
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult ThemTinTuc(TinTuc model, HttpPostedFileBase[] HinhAnhDaiDien)
        {
            // //kiểm tra hình ảnh đã tồn tại chưa
            if (HinhAnhDaiDien[0].ContentLength > 0)
            {
                //     //lấy tên hình ảnh
                var FileName = Path.GetFileName(HinhAnhDaiDien[0].FileName);
                //  
                // lấy đường dẩn hình ảnh
                var path = Path.Combine(Server.MapPath("~/Content/CssHome/HinhSanPham"), FileName);

                // // nếu thư mục chứa hình đã có hình

                    // lấy hình ảnh lưu vào thư mục hình ảnh
                    HinhAnhDaiDien[0].SaveAs(path);
                    model.HinhAnhDaiDien = HinhAnhDaiDien[0].FileName;
             
            }
            db.TinTucs.Add(model);
            db.SaveChanges();
            return RedirectToAction("Index", "QuanLyTinTuc");
        }
        public ActionResult ChinhSua(int IdTinTuc)
        {
            if (IdTinTuc == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            TinTuc tintuc = db.TinTucs.SingleOrDefault(n => n.IdTinTuc == IdTinTuc);
            if (tintuc == null)
            {
                return HttpNotFound();
            }
            return View(tintuc);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult ChinhSua(TinTuc model, HttpPostedFileBase HinhAnhDaiDien)
        {
            //     //lấy tên hình ảnh
            var FileName = Path.GetFileName(HinhAnhDaiDien.FileName);
            //  
            // lấy đường dẩn hình ảnh
            var path = Path.Combine(Server.MapPath("~/Content/CssHome/HinhSanPham"), FileName);
            // lấy hình ảnh lưu vào thư mục hình ảnh
            HinhAnhDaiDien.SaveAs(path);
            model.HinhAnhDaiDien = HinhAnhDaiDien.FileName;
            db.TinTucs.Add(model);
            db.Entry(model).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Xoa(int? IdTinTuc)
        {
            // lấy sản phẩm cần chỉnh dựa vào id
            if (IdTinTuc == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            TinTuc tintuc = db.TinTucs.SingleOrDefault(n => n.IdTinTuc == IdTinTuc);
            if (tintuc == null)
            {
                return HttpNotFound();
            }
            return View(tintuc);


        }
        [HttpPost]
        public ActionResult Xoa(int IdTinTuc)
        {
            if (IdTinTuc == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TinTuc tintuc = db.TinTucs.SingleOrDefault(n => n.IdTinTuc == IdTinTuc);
            if (tintuc == null)
            {
                return HttpNotFound();
            }
            db.TinTucs.Remove(tintuc);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
	}
}