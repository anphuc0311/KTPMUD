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
    /// Interaction logic for dongvat.xaml
    /// </summary>
    public partial class dongvat : Window
    {
        private string connectionString = "Server=localhost;Database=anphuc;Trusted_Connection=True;";
        public dongvat()
        {
            InitializeComponent();
            LoadData();
        }
        public class DongVat
        {
            public int ID { get; set; }
            public string TenDongVat { get; set; }
            public string LoaiDongVat { get; set; }
            public int SoLuong { get; set; }
            public string CoSoLuuTru { get; set; }
            public string BienDong { get; set; }
            public int ThongKeThang { get; set; }
            public int ThongKeQuy { get; set; }
            public int ThongKeNam { get; set; }
        }


        private void LoadData()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT * FROM DongVat";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    var dongVatList = dataTable.AsEnumerable().Select(row => new DongVat
                    {
                        ID = row.Field<int>("ID"),
                        TenDongVat = row.Field<string>("TenDongVat"),
                        LoaiDongVat = row.Field<string>("LoaiDongVat"),
                        SoLuong = row.Field<int>("SoLuong"),
                        CoSoLuuTru = row.Field<string>("CoSoLuuTru"),
                        BienDong = row.Field<string>("BienDong"),
                        ThongKeThang = row.Field<int>("ThongKeThang"),
                        ThongKeQuy = row.Field<int>("ThongKeQuy"),
                        ThongKeNam = row.Field<int>("ThongKeNam")
                    }).ToList();
                    dataGridDongvat.ItemsSource = dataTable.DefaultView;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải dữ liệu: {ex.Message}");
            }
        }

        private void AddNewButton_Click(object sender, RoutedEventArgs e)
        {   
            themdv themdv = new themdv();
            themdv.ShowDialog();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string insertQuery = "INSERT INTO DongVat (TenDongVat, LoaiDongVat, SoLuong, CoSoLuuTru, BienDong, ThongKeThang, ThongKeQuy, ThongKeNam) " +
                                         "VALUES (@tenDongVat, @loaiDongVat, @soLuong, @coSoLuuTru, @bienDong, @thongKeThang, @thongKeQuy, @thongKeNam)";
                    SqlCommand command = new SqlCommand(insertQuery, connection);
                    command.Parameters.AddWithValue("@tenDongVat", themdv.txtTenDongVat);
                    command.Parameters.AddWithValue("@loaiDongVat", themdv.txtLoaiDongVat);
                    command.Parameters.AddWithValue("@soLuong", themdv.txtSoLuong);
                    command.Parameters.AddWithValue("@coSoLuuTru", themdv.txtCoSoLuuTru);
                    command.Parameters.AddWithValue("@bienDong", themdv.txtBienDong);
                    command.Parameters.AddWithValue("@thongKeThang", themdv.txtThongKeThang);
                    command.Parameters.AddWithValue("@thongKeQuy", themdv.txtThongKeQuy);
                    command.Parameters.AddWithValue("@thongKeNam", themdv.txtThongKeNam);

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

        private void btnXoa_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dataGridDongvat.SelectedItem == null)
                {
                    MessageBox.Show("Vui lòng chọn dòng để xóa!");
                    return;
                }

                DataRowView selectedRow = (DataRowView)dataGridDongvat.SelectedItem;
                int id = Convert.ToInt32(selectedRow["ID"]);

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string deleteQuery = "DELETE FROM DongVat WHERE ID = @ID";
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

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string keyword = SearchTextBox.Text.Trim(); // Lấy nội dung ô tìm kiếm

            if (string.IsNullOrEmpty(keyword))
            {
                LoadData(); // Nếu rỗng, hiển thị toàn bộ dữ liệu
                return;
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT * FROM DongVat WHERE TenDongVat LIKE @keyword";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    adapter.SelectCommand.Parameters.AddWithValue("@keyword", "%" + keyword + "%");

                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    dataGridDongvat.ItemsSource = dataTable.DefaultView;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tìm kiếm: {ex.Message}");
            }
        }


        private void GoBackButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void HistoryButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void dataGriddongvat_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

      
    }
}
