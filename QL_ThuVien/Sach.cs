using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QL_ThuVien
{
    [Serializable]
    internal class Sach
    {
        int _maSach;
        string _tenSach;
        string _theLoai;
        DateTime _ngayXuatBan;
        //int _soLuong;


        public int MaSach { get => _maSach; set => _maSach = value; }
        public string TenSach { get => _tenSach; set => _tenSach = value; }
        public string TheLoai { get => _theLoai; set => _theLoai = value; }
        public DateTime NgayXuatBan { get => _ngayXuatBan; set => _ngayXuatBan = value; }
        //public int SoLuong { get => _soLuong; set => _soLuong = value; }


        public Sach(int maSach, string tenSach, string theLoai, DateTime ngayXuatBan)
        {
            MaSach = maSach;
            TenSach = tenSach;
            TheLoai = theLoai;
            NgayXuatBan = ngayXuatBan;
            //SoLuong = soLuong;

        }

        public Sach(Sach sach) : this(sach.MaSach, sach.TenSach, sach.TheLoai, sach.NgayXuatBan) { }
    }
}
