using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QL_ThuVien
{
    internal class XuLyTacGia
    {
        private Dictionary<string, TacGia> _dsTacGia;

        public XuLyTacGia()
        {
            _dsTacGia = new Dictionary<string, TacGia>();
        }

        public List<TacGia> DS_TacGia { get => _dsTacGia.Values.ToList(); }

        public TacGia TimTheoMa(string maCanTim)
        {
            if (_dsTacGia.ContainsKey(maCanTim))
                return _dsTacGia[maCanTim];

            return null;
        }

        public bool Them(TacGia tg)
        {
            TacGia tim = TimTheoMa(tg.MaTacGia);

            if (tim != null) return false;

            _dsTacGia.Add(tg.MaTacGia, tg);
            return true;
        }

        public bool Xoa(string maCanXoa)
        {
            TacGia tim = TimTheoMa(maCanXoa);

            if (tim == null) return false;

            _dsTacGia.Remove(maCanXoa);
            return true;
        }

        public bool Sua(TacGia tgCanSua)
        {
            TacGia tg = TimTheoMa(tgCanSua.MaTacGia);

            if (tg == null) return false;

            tg.MaTacGia = tgCanSua.MaTacGia;
            tg.HoTen = tgCanSua.HoTen;
            tg.NgaySinh = tgCanSua.NgaySinh;
            tg.SoDienThoai = tgCanSua.SoDienThoai;
            tg.QuocTich = tgCanSua.QuocTich;
            tg.GioiTinh = tgCanSua.GioiTinh;
            tg.NgaySinh = tgCanSua.NgayBatDauViet;
            tg.TheLoai = tgCanSua.TheLoai;
            tg.DiaChi = tg.DiaChi;
            return true;
        }

        public bool GhiFile(string filePath)
        {
            try
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Create))
                {
                    BinaryFormatter bf= new BinaryFormatter();
                    bf.Serialize(fs, DS_TacGia); // Tuần tự hóa danh sách sách
                }
                return true;
            }
            //catch
            //{
            //    return false;
            //}
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi ghi file: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public bool DocFile(string filePath)
        {
            try
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Open))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    var dsTG = (List<TacGia>)formatter.Deserialize(fs); // Giải tuần tự hóa danh sách sách

                    // Xóa danh sách hiện tại và thêm các sách từ tệp
                    _dsTacGia.Clear();
                    foreach (var tg in dsTG)
                    {
                        _dsTacGia.Add(tg.MaTacGia, tg);
                    }
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
