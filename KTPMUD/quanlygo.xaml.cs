using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
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
    /// Interaction logic for quanlygo.xaml
    /// </summary>
    public partial class quanlygo : Window
    {
        private string connectionString = "Server=localhost;Database=anphuc;Trusted_Connection=True;";
        public quanlygo()
        {
            InitializeComponent();
            LoadData();
        }

        private void HistoryButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string searchKeyword = SearchTextBox.Text.Trim();

            if (string.IsNullOrEmpty(searchKeyword))
            {
                LoadData(); // Nếu không nhập gì, hiển thị toàn bộ dữ liệu
                return;
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT * FROM LoaiHinhSanXuat WHERE LoaiHinhSanXuat LIKE @keyword OR ThongTinCoSo LIKE @keyword";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    adapter.SelectCommand.Parameters.AddWithValue("@keyword", "%" + searchKeyword + "%");

                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    dataGridGo.ItemsSource = dataTable.DefaultView;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tìm kiếm dữ liệu: {ex.Message}");
            }
        }




        private void GoBackButton_Click(object sender, RoutedEventArgs e)
        {

        }

 

        private void dataGridGiongCayTrong_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void LoadData()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT * FROM LoaiHinhSanXuat";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    dataGridGo.ItemsSource = dataTable.DefaultView;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải dữ liệu: {ex.Message}");
            }
        }

        private void AddNewButton_Click(object sender, RoutedEventArgs e)
        {
            themcssx themcssx = new themcssx();
            themcssx.ShowDialog();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string insertQuery = "INSERT INTO LoaiHinhSanXuat (LoaiHinhSanXuat, ThongTinCoSo, HinhThucHoatDong, ThongKeThang, ThongKeQuy, ThongKeNam) VALUES (@loai, @coSo, @hoatDong, @thang, @quy, @nam)";
                    SqlCommand command = new SqlCommand(insertQuery, connection);
                    command.Parameters.AddWithValue("@loai", themcssx.loaiHinh);
                    command.Parameters.AddWithValue("@coSo",themcssx.coSo);
                    command.Parameters.AddWithValue("@hoatDong", themcssx.hinhThucHoatDong);
                    command.Parameters.AddWithValue("@thang", themcssx.thongKeThang);
                    command.Parameters.AddWithValue("@quy", themcssx.thongKeQuy);
                    command.Parameters.AddWithValue("@nam", themcssx.thongKeNam);

                    command.ExecuteNonQuery();
                    MessageBox.Show("Thêm thành công!");
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi thêm dữ liệu: {ex.Message}");
            }
        }

        private void BtnXoa_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dataGridGo.SelectedItem == null)
                {
                    MessageBox.Show("Vui lòng chọn dòng để xóa!");
                    return;
                }

                DataRowView selectedRow = (DataRowView)dataGridGo.SelectedItem;

                // Kiểm tra tên cột là "ID" hay tên cột khác trong bảng của bạn
                int id = Convert.ToInt32(selectedRow["ID"]);

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string deleteQuery = "DELETE FROM LoaiHinhSanXuat WHERE ID = @ID";
                    SqlCommand command = new SqlCommand(deleteQuery, connection);
                    command.Parameters.AddWithValue("@ID", id);
                    command.ExecuteNonQuery();

                    MessageBox.Show("Xóa thành công!");
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi xóa dữ liệu: {ex.Message}");
            }
        }

        private void dataGridGo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
    
