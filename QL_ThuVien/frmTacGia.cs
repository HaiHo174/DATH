using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QL_ThuVien
{
    public partial class frmTacGia : Form
    {
        private XuLyTacGia xuLyTG;
        public frmTacGia()
        {
            InitializeComponent();
            xuLyTG = new XuLyTacGia();
        }

        private GioiTinh GetGioiTinh()
        {
            if (rdoNam.Checked)
                return GioiTinh.Nam;
            else return GioiTinh.Nu;
        }

        private void SetGioiTinh(GioiTinh gioiTinh)
        {
            switch (gioiTinh)
            {
                case GioiTinh.Nam:
                    rdoNam.Checked = true;
                    break;
                case GioiTinh.Nu:
                    rdoNu.Checked = true;
                    break;
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            TacGia tg = new TacGia(txtMaTacGia.Text, txtHoTen.Text, dtpNgaySinh.Value, txtSDT.Text, txtQuocTich.Text,
                GetGioiTinh(), dtpNgayBDViet.Value, txtTheLoai.Text, txtDiaChi.Text);

            xuLyTG.Them(tg);

            HienThi();
        }

        private void dgvTacGia_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvTacGia.SelectedRows.Count == 0) return;

            int rowIndex = dgvTacGia.SelectedRows[0].Index;
            string maTG = dgvTacGia.Rows[rowIndex].Cells[0].Value.ToString();

            TacGia tg = xuLyTG.TimTheoMa(maTG);

            txtMaTacGia.Text = tg.MaTacGia;
            txtHoTen.Text = tg.HoTen;
            dtpNgaySinh.Value = tg.NgaySinh;
            txtSDT.Text = tg.SoDienThoai;
            txtQuocTich.Text = tg.QuocTich;
            SetGioiTinh(tg.GioiTinh);
            dtpNgayBDViet.Value = tg.NgayBatDauViet;
            txtTheLoai.Text = tg.TheLoai;
            txtDiaChi.Text = tg.DiaChi;

        }

        private void frmTacGia_Load(object sender, EventArgs e)
        {
            BindingSource bs = new BindingSource();

            bs.DataSource = xuLyTG.DS_TacGia;

            dgvTacGia.DataSource = bs;
        }

        private void HienThi()
        {
            BindingSource bs = new BindingSource();
            {
                bs.DataSource = xuLyTG.DS_TacGia;
            };
            dgvTacGia.DataSource = bs;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvTacGia.SelectedRows.Count == 0) return;

            int rowIndex = dgvTacGia.SelectedRows[0].Index;
            string maTG = dgvTacGia.Rows[rowIndex].Cells[0].Value.ToString();

            xuLyTG.Xoa(maTG);
            HienThi();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dgvTacGia.SelectedRows.Count == 0) return;

            int rowIndex = dgvTacGia.SelectedRows[0].Index;
            string maTG = dgvTacGia.Rows[rowIndex].Cells[0].Value.ToString();

            TacGia tg = xuLyTG.TimTheoMa(maTG);

            tg.HoTen = txtHoTen.Text;
            tg.NgaySinh = dtpNgaySinh.Value;
            tg.SoDienThoai = txtSDT.Text;
            tg.QuocTich = txtQuocTich.Text;
            tg.GioiTinh = GetGioiTinh();
            tg.NgayBatDauViet = dtpNgayBDViet.Value;
            tg.TheLoai = txtTheLoai.Text;
            tg.DiaChi = txtDiaChi.Text;

            xuLyTG.Sua(tg);

            HienThi();
        }

        private void btnGhiFile_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Binary Files (*.bin)|*.bin|All Files (*.*)|*.*",
                Title = "Chọn nơi lưu danh sách sách"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = saveFileDialog.FileName;

                if (xuLyTG.GhiFile(filePath))
                {
                    MessageBox.Show("Ghi dữ liệu vào tệp thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Có lỗi xảy ra trong quá trình ghi tệp.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnDocFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Binary Files (*.bin)|*.bin|All Files (*.*)|*.*",
                Title = "Chọn tệp danh sách sách"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;

                if (xuLyTG.DocFile(filePath))
                {
                    MessageBox.Show("Đọc dữ liệu từ tệp thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    HienThi(); // Cập nhật lại DataGridView
                }
                else
                {
                    MessageBox.Show("Có lỗi xảy ra trong quá trình đọc tệp.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
