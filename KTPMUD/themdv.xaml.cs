using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
    /// Interaction logic for themdv.xaml
    /// </summary>
    public partial class themdv : Window
    {
        private string connectionString = "Server=localhost;Database=anphuc;Trusted_Connection=True;";
       
        public themdv()
        {
            InitializeComponent();
            
        }

        // Sự kiện khi nhấn nút "Thêm"
        private void btnThem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string tenDongVat = txtTenDongVat.Text;
                string loaiDongVat = txtLoaiDongVat.Text;
                int soLuong = int.Parse(txtSoLuong.Text);
                string coSoLuuTru = txtCoSoLuuTru.Text;
                string bienDong = txtBienDong.Text;
                int thongKeThang = int.Parse(txtThongKeThang.Text);
                int thongKeQuy = int.Parse(txtThongKeQuy.Text);
                int thongKeNam = int.Parse(txtThongKeNam.Text);

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string insertQuery = "INSERT INTO DongVat (TenDongVat, LoaiDongVat, SoLuong, CoSoLuuTru, BienDong, ThongKeThang, ThongKeQuy, ThongKeNam) " +
                                         "VALUES (@tenDongVat, @loaiDongVat, @soLuong, @coSoLuuTru, @bienDong, @thongKeThang, @thongKeQuy, @thongKeNam)";
                    SqlCommand command = new SqlCommand(insertQuery, connection);
                    command.Parameters.AddWithValue("@tenDongVat", tenDongVat);
                    command.Parameters.AddWithValue("@loaiDongVat", loaiDongVat);
                    command.Parameters.AddWithValue("@soLuong", soLuong);
                    command.Parameters.AddWithValue("@coSoLuuTru", coSoLuuTru);
                    command.Parameters.AddWithValue("@bienDong", bienDong);
                    command.Parameters.AddWithValue("@thongKeThang", thongKeThang);
                    command.Parameters.AddWithValue("@thongKeQuy", thongKeQuy);
                    command.Parameters.AddWithValue("@thongKeNam", thongKeNam);

                    command.ExecuteNonQuery();
                    MessageBox.Show("Động vật đã được thêm!");
                    this.Close(); // Đóng cửa sổ sau khi thêm thành công
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi thêm động vật: {ex.Message}");
            }
        }

        private void btnHuy_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}