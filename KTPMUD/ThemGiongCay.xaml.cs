using System;
using System.Windows;

namespace KTPMUD
{
    public partial class ThemGiongCay : Window
    {
        public string GiongCay { get; private set; }
        public int SoLuong { get; private set; }
        public string MoTa { get; private set; }
        public string Csxs { get; private set; } 
        public int SoLuongCs { get; private set; }
        public bool IsSaved { get; private set; }

        public ThemGiongCay()
        {
            InitializeComponent();
            IsSaved = false; // Mặc định là chưa lưu
        }

        private void BtnLuu_Click(object sender, RoutedEventArgs e)
        {
            // Kiểm tra dữ liệu nhập
            if (string.IsNullOrEmpty(txtGiongCay.Text) ||
               string.IsNullOrEmpty(txtMoTa.Text) ||
                string.IsNullOrEmpty(csxs.Text) || // Kiểm tra csxs
                !int.TryParse(txtSoLuong.Text, out int soluong) ||
                !int.TryParse(txtsoluongcs.Text, out int soluongcs))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ và đúng định dạng!");
                return;
            }

            // Gán giá trị
            GiongCay = txtGiongCay.Text.Trim();
            SoLuong = soluong;
            MoTa = txtMoTa.Text.Trim();
            Csxs = csxs.Text.Trim();
            SoLuongCs = soluongcs;
            IsSaved = true;

            this.Close(); // Đóng cửa sổ
        }

        private void BtnHuy_Click(object sender, RoutedEventArgs e)
        {
            this.Close(); // Đóng cửa sổ mà không lưu
        }
    }
}
