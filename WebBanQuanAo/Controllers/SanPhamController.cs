using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using WebBanQuanAo.Models;

namespace WebBanQuanAo.Controllers
{
    public class SanPhamController : Controller
    {
        //
        // GET: /SanPham/
        BanQuanAoEntities2 db = new BanQuanAoEntities2();
        [ChildActionOnly]
        public ActionResult SanPhamStyle1Partial()
        {
            return PartialView();
        }
        [ChildActionOnly]
        public ActionResult SanPhamStyle2Partial()
        {
            return PartialView();
        }
        public ActionResult XemChiTiet(int? Id, string TenSanPham)
        {
            // kiểm tra tham số truyền vào có rổng hay không
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanpham = db.SanPhams.SingleOrDefault(n => n.IdSanPham == Id && n.DaXoa == false);
            // kiểm tra id sản phẩm truyền  vào
            if (sanpham == null)
            {
                // thông báo ko tìm thấy
                return HttpNotFound();
            }
            return View(sanpham);
        }
        // xây dựng aciton load sản phẩm theo mã  sản phẩm và mã nhà sản xuất
        public ActionResult SanPham(int? IdLoaiSanPham, int? IdMLSP, int? page)
        {

            if (IdLoaiSanPham == null || IdMLSP == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            // load sản phẩm dựa vào loại sản phẩm và nhà sản xuất trong bảng sản phẩm
            var lstSanPham = db.SanPhams.Where(n => n.IdLoaiSanPham == IdLoaiSanPham && n.IdMLSP == IdMLSP);
            if (lstSanPham.Count() == 0)
            {
                // thông báo ko tìm thấy
                return HttpNotFound();
            }
            // thực hiện phân trang
            // tạo biến số sản phẩm trên trang
            if (Request.HttpMethod != "GET")
            {
                page = 1;
            }
            int Pagesize = 9;
            // tạo biến số trang hiện tại
            int PageNumber = (page ?? 1);
            ViewBag.IdLoaiSanPham = IdLoaiSanPham;
            ViewBag.IdMLSP = IdMLSP;
            return View(lstSanPham.OrderBy(n => n.IdSanPham).ToPagedList(PageNumber, Pagesize));


        }
        public ActionResult TrangSanPham(int? IdLoaiSanPham, int? page)
        {

            if (IdLoaiSanPham == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            // load sản phẩm dựa vào loại sản phẩm và nhà sản xuất trong bảng sản phẩm
            var lstSanPham = db.SanPhams.Where(n => n.IdLoaiSanPham == IdLoaiSanPham);
            if (lstSanPham.Count() == 0)
            {
                // thông báo ko tìm thấy
                return HttpNotFound();
            }
            // thực hiện phân trang
            // tạo biến số sản phẩm trên trang
            if (Request.HttpMethod != "GET")
            {
                page = 1;
            }
            int Pagesize = 9;
            // tạo biến số trang hiện tại
            int PageNumber = (page ?? 1);
            ViewBag.IdLoaiSanPham = IdLoaiSanPham;
            return View(lstSanPham.OrderBy(n => n.IdSanPham).ToPagedList(PageNumber, Pagesize));
        }
	}
}