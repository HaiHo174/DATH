using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QL_ThuVien
{
    [Serializable]
    public enum GioiTinh { Nam, Nu};
    [Serializable]
    internal class TacGia
    {
        private string _maTacGia;
        private string _hoTen;
        private DateTime _ngaySinh;
        private string _soDienThoai;
        private string _quocTich;
        private GioiTinh _gioiTinh;
        private DateTime _ngayBatDauViet;
        private string _theLoai;
        private string _diaChi;

        public TacGia(string maTacGia, string hoTen, DateTime ngaySinh, string soDienThoai, string quocTich, 
            GioiTinh gioiTinh, DateTime ngayBatDauViet, string theLoai, string diaChi)
        {
            _maTacGia = maTacGia;
            _hoTen = hoTen;
            _ngaySinh = ngaySinh;
            _soDienThoai = soDienThoai;
            _quocTich = quocTich;
            _gioiTinh = gioiTinh;
            _ngayBatDauViet = ngayBatDauViet;
            _theLoai = theLoai;
            _diaChi = diaChi;
        }

        public TacGia():this("", "", DateTime.Now, "", "", GioiTinh.Nam, DateTime.Now, "", "") { }

        public TacGia(TacGia tg) : this(tg.MaTacGia, tg.HoTen, tg.NgaySinh, tg.SoDienThoai, tg.QuocTich,
            tg.GioiTinh, tg.NgayBatDauViet, tg.TheLoai, tg.DiaChi) { }

        public string MaTacGia { get => _maTacGia; set => _maTacGia = value; }
        public string HoTen { get => _hoTen; set => _hoTen = value; }
        public DateTime NgaySinh { get => _ngaySinh; set => _ngaySinh = value; }
        public string SoDienThoai { get => _soDienThoai; set => _soDienThoai = value; }
        public string QuocTich { get => _quocTich; set => _quocTich = value; }
        public GioiTinh GioiTinh { get => _gioiTinh; set => _gioiTinh = value; }
        public DateTime NgayBatDauViet { get => _ngayBatDauViet; set => _ngayBatDauViet = value; }
        public string TheLoai { get => _theLoai; set => _theLoai = value; }
        public string DiaChi { get => _diaChi; set => _diaChi = value; }
    }
}
