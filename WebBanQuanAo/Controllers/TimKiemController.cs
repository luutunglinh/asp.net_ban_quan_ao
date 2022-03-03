using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanQuanAo.Models;
using PagedList;

namespace WebBanQuanAo.Controllers
{
    public class TimKiemController : Controller
    {
        //
        // GET: /TimKiem/
        BanQuanAoEntities2 db = new BanQuanAoEntities2();
        [HttpGet]
        public ActionResult KQTimKiem(string sTuKhoa, int? page)
        {
            // tìm kiếm theo tên sản phẩm

            // thực hiện phân trang
            // tạo biến số sản phẩm trên trang
            if (Request.HttpMethod != "GET")
            {
                page = 1;
            }
            int Pagesize = 6;
            // tạo biến số trang hiện tại
            int PageNumber = (page ?? 1);
            var lstSanPham = db.SanPhams.Where(n => n.TenSanPham.Contains(sTuKhoa));
            ViewBag.TuKhoa = sTuKhoa;
            return View(lstSanPham.OrderBy(n => n.TenSanPham).ToPagedList(PageNumber, Pagesize));

        }
        [HttpPost]
        public ActionResult LayTuKhoaTimKiem(string sTuKhoa)
        {
            // tìm kiếm theo tên sản phẩm

            // thực hiện phân trang
            // tạo biến số sản phẩm trên trang
            //if (Request.HttpMethod != "GET")
            //{
            //    page = 1;
            //}
            //int Pagesize = 6;
            //// tạo biến số trang hiện tại
            //int PageNumber = (page ?? 1);
            //var lstSanPham = db.SanPhams.Where(n => n.TenSanPham.Contains(sTuKhoa));
            //ViewBag.TuKhoa = sTuKhoa;
            //return View(lstSanPham.OrderBy(n => n.TenSanPham).ToPagedList(PageNumber, Pagesize));
            // gọi hàm get tìm kiếm
            return RedirectToAction("KQTimKiem", new { @sTuKhoa = sTuKhoa });

        }
	}
}