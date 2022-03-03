using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebBanQuanAo.Models
{
      [MetadataTypeAttribute(typeof(MaLoaiSanPhamMetadata))]

    public partial class MaLoaiSanPham
    {
          internal sealed class MaLoaiSanPhamMetadata
          {
              public int IdMLSP { get; set; }
              [DisplayName("Tên danh mục:")]
              public string TenLSP { get; set; }
               [DisplayName("Mô tả:")]
              public string ThongTin { get; set; }
              public string Logo { get; set; }
          }
    }
}