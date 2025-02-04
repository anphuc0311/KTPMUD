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
    /// Interaction logic for giongcaytrong.xaml
    /// </summary>
    public partial class giongcaytrong : Window
    {
        private string connectionString = "Server=localhost;Database=anphuc;Trusted_Connection=True;";
        public giongcaytrong()
        {
            InitializeComponent();
            LoadData();
        }
        private void LoadData()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT ID, GiongCay, SoLuong, MoTa, csxs, soluongcs FROM giong";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    dataGridGiongCayTrong.ItemsSource = dataTable.DefaultView;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải dữ liệu: {ex.Message}");
            }
        }

        // Hàm thêm dữ liệu vào bảng
        private void BtnThem_Click(object sender, RoutedEventArgs e)
        {   
        ThemGiongCay themGiongCay = new ThemGiongCay();
        themGiongCay.ShowDialog();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Thêm dữ liệu mẫu (cần cập nhật giao diện nhập liệu tùy ý)
                    string insertQuery = "INSERT INTO giong (GiongCay, SoLuong, MoTa, csxs, soluongcs) VALUES (@giongCay, @soluong, @mota, @csxs, @soluongcs)";
                    SqlCommand command = new SqlCommand(insertQuery, connection);
                    command.Parameters.AddWithValue("@giongCay", themGiongCay.GiongCay); // Thay bằng giá trị người dùng nhập
                    command.Parameters.AddWithValue("@soluong", themGiongCay.SoLuong);       // Thay bằng giá trị người dùng nhập
                    command.Parameters.AddWithValue("@mota", themGiongCay.MoTa); // Thay bằng giá trị người dùng nhập
                    command.Parameters.AddWithValue("@csxs", themGiongCay.Csxs);   // Giá trị csxs
                    command.Parameters.AddWithValue("@soluongcs", themGiongCay.SoLuongCs);

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

        // Hàm xóa dữ liệu dựa trên ID
        private void BtnXoa_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dataGridGiongCayTrong.SelectedItem == null)
                {
                    MessageBox.Show("Vui lòng chọn dòng để xóa!");
                    return;
                }

                DataRowView selectedRow = (DataRowView)dataGridGiongCayTrong.SelectedItem;
                int id = Convert.ToInt32(selectedRow["ID"]);

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string deleteQuery = "DELETE FROM giong WHERE ID = @ID";
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

        private void dataGridGiongCayTrong_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void HistoryButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void GoBackButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
 
