using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebBanQuanAo.Models
{
     [MetadataTypeAttribute(typeof(TinTucMetadata))]

    public partial class TinTuc
    {
         internal sealed class TinTucMetadata
         {
             public int IdTinTuc { get; set; }
              [DisplayName("Tiêu đề:")]
             public string TieuDe { get; set; }
              [DisplayName("Nội dung:")]
              [AllowHtml] 
             public string NoiDung { get; set; }
              [DisplayName("Hình ảnh:")]
             public string HinhAnhDaiDien { get; set; }
              [DisplayName("Mới:")]
             public Nullable<bool> Moi { get; set; }
              [DisplayName("Nổi bật:")]
             public Nullable<bool> NoiBat { get; set; }
              [DisplayName("Ngày đăng:")]

             public Nullable<System.DateTime> NgayDang { get; set; }
                [DisplayName("Nội dung ngắn:")]
                [AllowHtml] 
             public string MoTa { get; set; }

         }
    }
}