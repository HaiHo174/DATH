using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace QL_ThuVien
{
    internal class XuLySach
    {
        private Dictionary<int, Sach> m_dsSach;

        public XuLySach()
        {
            m_dsSach = new Dictionary<int, Sach>();
        }
        public List<Sach> DS_Sach { get => m_dsSach.Values.ToList();  }
        public Sach Tim(int ma)
        {
            if (m_dsSach.ContainsKey(ma))
            {
                return m_dsSach[ma];
            }
            return null;
        }
        //public List<Sach> TimTheoTen(string tenSach)
        //{
        //    return m_dsSach.Values.Where(s => s.TenSach.Contains(tenSach,StringComparison.OrdinalIgnoreCase )).ToList();
        //}
        public List<Sach> TimTheoTen(string tenSach)
        {
            return m_dsSach.Values.Where(s => s.TenSach.ToLower().Contains(tenSach.ToLower())).ToList();
        }

        public bool Them(Sach sachCanThem)
        {
            Sach tim = Tim(sachCanThem.MaSach);

            if (tim != null) return false;

            m_dsSach.Add(sachCanThem.MaSach, sachCanThem);
            return true;
        }
        public bool Xoa(int maCanXoa)
        {
            Sach s = Tim(maCanXoa);

            if (s == null) return false;

            m_dsSach.Remove(maCanXoa);
            return true;
        }
        public bool Sua(Sach sachCanSua)
        {
            Sach sach = Tim(sachCanSua.MaSach);

            if (sach == null) return false;

            sach.MaSach = sachCanSua.MaSach;
            sach.TenSach = sachCanSua.TenSach;
            sach.TheLoai = sachCanSua.TheLoai;
            sach.NgayXuatBan = sachCanSua.NgayXuatBan;

            return true;
        }
        public bool DocFile(string filePath)
        {
            try
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Open))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    var dsSach = (List<Sach>)formatter.Deserialize(fs); // Giải tuần tự hóa danh sách sách

                    // Xóa danh sách hiện tại và thêm các sách từ tệp
                    m_dsSach.Clear();
                    foreach (var sach in dsSach)
                    {
                        m_dsSach.Add(sach.MaSach, sach);
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool GhiFile(string filePath)
        {
            try
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Create))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(fs, DS_Sach); // Tuần tự hóa danh sách sách
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
