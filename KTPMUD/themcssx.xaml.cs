using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace KTPMUD
{
    /// <summary>
    /// Interaction logic for themcssx.xaml
    /// </summary>



    public partial class themcssx : Window
    {
        public string loaiHinh { get; set; }
        public string coSo { get; set; }
        public string hinhThucHoatDong { get; set; }
        public int thongKeThang { get; set; }
        public int thongKeQuy { get; set; }
        public int thongKeNam { get; set; }

        public themcssx()
        {
            InitializeComponent();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            // Lấy dữ liệu từ các TextBox và gán vào các thuộc tính
            loaiHinh = txtLoaiHinh.Text;
            coSo = txtCoSo.Text;
            hinhThucHoatDong = txtHinhThuc.Text;
            thongKeThang = int.Parse(txtThongKeThang.Text);
            thongKeQuy = int.Parse(txtThongKeQuy.Text);
            thongKeNam = int.Parse(txtThongKeNam.Text);

            this.DialogResult = true; // Đóng cửa sổ và thông báo thành công
        }

        private void BtnHuy_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false; // Nếu người dùng hủy thì cửa sổ sẽ đóng mà không làm gì
        }

        
    }
}
    