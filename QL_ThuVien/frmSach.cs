using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QL_ThuVien
{
    public partial class frmSach : Form
    {
        private XuLySach xuLySach;
        public frmSach()
        {
            InitializeComponent();
            xuLySach = new XuLySach();
        }
        //private void LoadData()
        //{
        //    xuLy.Them(new Sach(1, "Dac nhan tam", "Self-help", new DateTime(2024, 11, 16)));
        //    xuLy.Them(new Sach(2, "Nhung nguoi khon kho", "Tieu thuyet", new DateTime(2024, 11, 17)));
        //    xuLy.Them(new Sach(3, "Sapiens", "Lich su", new DateTime(2024, 11, 18)));
        //    xuLy.Them(new Sach(4, "Mini habit", "skill", new DateTime(2024, 11, 19)));

        //    HienThi();
        //}
        private void dgvSach_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dgvSach.SelectedRows.Count == 0) return;

            int rowIndex = dgvSach.SelectedRows[0].Index;
            int maSach = int.Parse(dgvSach.Rows[rowIndex].Cells[0].Value.ToString());

            Sach s = xuLySach.Tim(maSach);

            txtMaSach.Text = s.MaSach.ToString();
            txtTenSach.Text = s.TenSach.ToString();
            txtTheLoai.Text = s.TheLoai.ToString();
            dtpNgayXuatBan.Value = s.NgayXuatBan;
        }
        private void HienThi()
        {
            BindingSource bs = new BindingSource();

            bs.DataSource = xuLySach.DS_Sach;

            dgvSach.DataSource = bs;

            //dgvSach.DataSource = xuLy.DS_Sach.ToList();
        }
        private void frmSach_Load(object sender, EventArgs e)
        {
            BindingSource bs = new BindingSource();

            bs.DataSource = xuLySach.DS_Sach;

            dgvSach.DataSource = bs;
            //var dsSach = xuLy.DS_Sach;
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            Sach s = new Sach(int.Parse(txtMaSach.Text), txtTenSach.Text, txtTheLoai.Text,
                dtpNgayXuatBan.Value);

            xuLySach.Them(s);

            HienThi();
        }
        //private void btnThem_Click(object sender, EventArgs e)
        //{
        //    if (!int.TryParse(txtMaSach.Text, out int maSach))
        //    {
        //        MessageBox.Show("Mã sách phải là số nguyên!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        return;
        //    }
        //    Sach s = new Sach(maSach, txtTenSach.Text, txtTheLoai.Text, dtpNgayXuatBan.Value);
        //    if (!xuLy.Them(s))
        //    {
        //        MessageBox.Show("Mã sách đã tồn tại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        return;
        //    }
        //    HienThi();
        //}

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvSach.SelectedRows.Count == 0) return;

            int rowIndex = dgvSach.SelectedRows[0].Index;
            int maSach = int.Parse(dgvSach.Rows[rowIndex].Cells[0].Value.ToString());

            xuLySach.Xoa(maSach);
            HienThi();
        }

        

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dgvSach.SelectedRows.Count == 0) return;

            int rowIndex = dgvSach.SelectedRows[0].Index;
            int maSach = int.Parse(dgvSach.Rows[rowIndex].Cells[0].Value.ToString());

            Sach sach = xuLySach.Tim(maSach);

            sach.TenSach = txtTenSach.Text;
            sach.TheLoai = txtTheLoai.Text;
            sach.NgayXuatBan = dtpNgayXuatBan.Value;

            xuLySach.Sua(sach);

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

                if (xuLySach.GhiFile(filePath))
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

                if (xuLySach.DocFile(filePath))
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
