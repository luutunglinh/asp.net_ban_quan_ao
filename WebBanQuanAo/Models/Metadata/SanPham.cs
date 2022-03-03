using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebBanQuanAo.Models
{
    [MetadataTypeAttribute(typeof(SanPhamMetadata))]

    public partial class SanPham
    {
        internal sealed class SanPhamMetadata
        {

            public int IdSanPham { get; set; }
            [DisplayName("Tên Sản Phẩm:")]
            public string TenSanPham { get; set; }
            [DisplayName("Đơn Giá:")]
            public Nullable<decimal> DonGia { get; set; }
            [DisplayName("Ngày Cập Nhật:")]
            public Nullable<System.DateTime> NgayCapNhat { get; set; }
            [DisplayName("Mô Tả:")]
            public string MoTa { get; set; }
            [DisplayName("Hình Ảnh:")]
            public string HinhAnh { get; set; }
            [DisplayName("Hình Ảnh 1:")]
            public string HinhAnh1 { get; set; }
            [DisplayName("Hình Ảnh 2:")]
            public string HinhAnh2 { get; set; }
            [DisplayName("Số lượng nhập:")]
            public Nullable<int> SoLuongTon { get; set; }
        
            [DisplayName("Lượt Xem:")]
            public Nullable<int> LuotXem { get; set; }
           
            [DisplayName("Mới:")]
            public Nullable<int> Moi { get; set; }

            [DisplayName("Mã loại sản phẩm:")]
            public Nullable<int> IdMLSP { get; set; }
     
            [DisplayName("Loại Sản Phẩm:")]
            public Nullable<int> IdLoaiSanPham { get; set; }
            [DisplayName("Đã Xóa:")]
            public Nullable<bool> DaXoa { get; set; }
        }
    }
}