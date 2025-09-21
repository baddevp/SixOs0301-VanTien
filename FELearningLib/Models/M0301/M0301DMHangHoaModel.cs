using System.ComponentModel.DataAnnotations.Schema;

namespace FELearningLib.Models.M0301
{
    [Table("HH_DM_HangHoa")]
    public class M0301DMHangHoaModel
    {
        // Thông tin cơ bản và nhận dạng
        public long ID { get; set; }
        public long? IDNhomHang { get; set; }
        public string? MaHangHoa { get; set; }
        public string? TenHangHoa { get; set; }
        public string? MaBarCode { get; set; }
        public string? MaSKU { get; set; }
        public string? MaVach { get; set; }

        // Đơn vị và quy cách
        public long? IDDonViTinhNhap { get; set; }
        public long? IDDonViTinhXuat { get; set; }
        public double? SoLuongQuyDoi { get; set; }
        public string? QuyCachDongGoi { get; set; }

        // Thông tin sản xuất và thành phần
        public long? IDDuongDung { get; set; }
        public long? IDNuocSanXuat { get; set; }
        public long? IDHangSanXuat { get; set; }
        public string? HamLuong { get; set; }
        public string? HoatChat { get; set; } // Sẽ được cập nhật bằng chuỗi nối
        public string? MaPPCheBien { get; set; }
        public string? DangBaoChe { get; set; }

        // Thông tin liên quan đến y tế và đăng ký
        public long? IDNguonChiTra { get; set; }
        public string? MaThuoc { get; set; }
        public string? MaNhom { get; set; }
        public string? MaHieuSP { get; set; }
        public bool? BHYT { get; set; }
        public string? SoDangKy { get; set; }
        public string? ThongTinThau { get; set; }
        public double? GiaThau { get; set; }
        public double? TyLeBHYT { get; set; }
        public double? PhuThu { get; set; }
        public string? MaThuocDQG { get; set; }
        public long? IDNhaThau { get; set; }
        public bool? KhongXuatHoaDon { get; set; }
        public string? GhiChu { get; set; }

        // Giá và tỉ lệ
        public double? GiaBanLe { get; set; }
        public double? GiaBanSi { get; set; }
        public double? GiaBanLieu { get; set; }
        public double? TiLeBanLe { get; set; }
        public double? TiLeBanSi { get; set; }
        public double? TiLeBanLieu { get; set; }
        public bool? XuatGiaVon { get; set; }
        public double? TiLeThanhToan { get; set; }
        public double? TiLeLoiNhuan { get; set; }
        public double? GiaThauCu { get; set; }

        // Tồn kho và cảnh báo
        public double? SLMin { get; set; }
        public double? SLMax { get; set; }
        public double? TongTon { get; set; }
        public bool? NhapTay { get; set; }
        public int? SoNgayCanhBao { get; set; }
        public int? SoNgayDungThuoc { get; set; }

        // Ngày tháng và trạng thái
        public DateTime? NgayHieuLuc { get; set; }
        public DateTime? NgayChotTon { get; set; }
        public double? SoLuongChotTon { get; set; }
        public double? TriGiaChotTon { get; set; }
        public bool Active { get; set; }
        public DateTime? ThauTuNgay { get; set; }
        public DateTime? ThauDenNgay { get; set; }

        // Liên kết
        public long? IDHangHoaTuongUng { get; set; }
        public bool? HangHoaTuongUng { get; set; }
    }
}
