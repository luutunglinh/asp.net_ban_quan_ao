using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebBanQuanAo.Models;

namespace WebBanQuanAo.Controllers
{
    public class QuanLyDonHangController : Controller
    {
        //
        BanQuanAoEntities2 db = new BanQuanAoEntities2();
        // GET: /QuanLyDonHang/
        public ActionResult ChuaThanhToan()
        {

            // lấy danh sách đơn hàng chưa duyệt
            var lstDonHang = db.DonDatHangs.Where(n => n.DaThanhToan == false).OrderBy(n => n.NgayDat);
            return View(lstDonHang);
        }
        public ActionResult ChuaGiao()
        {
            // lấy danh sách đơn hàng chưa giao
            var lstChuaGiao = db.DonDatHangs.Where(n => n.TinhTrangGiaoHang == false).OrderBy(n => n.NgayGiao);
            return View(lstChuaGiao);
        }
        public ActionResult DaGiaoDaThanhToan()
        {
            var lstDaGiaoDaThanhToan = db.DonDatHangs.Where(n => n.TinhTrangGiaoHang == true && n.DaThanhToan == true);
            return View(lstDaGiaoDaThanhToan);
        }
        [HttpGet]
        public ActionResult DuyetDonHang(int? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DonDatHang model = db.DonDatHangs.SingleOrDefault(n => n.IdDDH == Id);
            if (model == null)
            {
                return HttpNotFound();
            }
            //lấy danh sách chi tiết
            var lstChiTietDH = db.ChiTietDonDatHangs.Where(n => n.IdDDH == Id);
            ViewBag.ListChiTietDH = lstChiTietDH;

            return View(model);
        }
        [HttpPost]
        public ActionResult DuyetDonHang(DonDatHang ddh)
        {
            //truy vấn cơ sở dử liệu
            DonDatHang ddhUpdate = db.DonDatHangs.SingleOrDefault(n => n.IdDDH == ddh.IdDDH);
            ddhUpdate.DaThanhToan = ddh.DaThanhToan;
            ddhUpdate.TinhTrangGiaoHang = ddh.TinhTrangGiaoHang;
            ddhUpdate.NgayGiao = ddh.NgayGiao;
            db.SaveChanges();
            //lấy danh sách chi tiết đơn đăt hàng để hiển thị cho ng dùng
            var lstChiTietDH = db.ChiTietDonDatHangs.Where(n => n.IdDDH == ddh.IdDDH);
            ViewBag.ListChiTietDH = lstChiTietDH;
    //        GuiEmail("Xác đơn hàng của bạn", "traitimcuagiotv19@gmail.com", "nhutthanh.nina@gmail.com", "windlov3sky", "Đơn hàng của bạn đã được đặt thành công!");
            return View(ddhUpdate);
        }
        
       /* public void GuiEmail(string Title, string ToEmail, string FromEmail, string PassWord, string Content)
        {
            // goi email
            MailMessage mail = new MailMessage();
            mail.To.Add(ToEmail); // Địa chỉ nhận
            mail.From = new MailAddress(ToEmail); // Địa chửi gửi
            mail.Subject = Title;  // tiêu đề gửi
            mail.Body = Content;                 // Nội dung
            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com"; // host gửi của Gmail
            smtp.Port = 587;               //port của Gmail
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential
            (FromEmail, PassWord);//Tài khoản password người gửi
            smtp.EnableSsl = true;   //kích hoạt giao tiếp an toàn SSL
            smtp.Send(mail);   //Gửi mail đi
        } */

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