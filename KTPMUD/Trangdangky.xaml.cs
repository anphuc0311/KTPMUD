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
using System.Data.SqlClient;
using System.Data;


namespace KTPMUD
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Trangdangky : Window
    {
       
         private string connectionString = "Server=localhost;Database=anphuc;Trusted_Connection=True;"; //tên database là gì thì tự sửa
        public Trangdangky()
        {
            InitializeComponent();
            LoadLocations();
        }
        private void LoadLocations()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT id, name FROM huyen";
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                   
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải dữ liệu: {ex.Message}");
            }

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            
        }

        private void button_Click_1(object sender, RoutedEventArgs e)
        {
            string fullName = hoten.Text.Trim();
            string username = taikhoan.Text.Trim();
            string password = matkhau.Text.Trim();
            string email = emails.Text.Trim();
            string roles = (quyen.SelectedItem as ComboBoxItem)?.Content.ToString();

            if (string.IsNullOrEmpty(fullName) || string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin!");
                return;
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string insertQuery = "INSERT INTO users (fullname, username, password, email, roles) " +
                                         "VALUES (@FullName, @Username, @Password, @Email, @roles);";
                    SqlCommand command = new SqlCommand(insertQuery, connection);
                    command.Parameters.AddWithValue("@FullName", fullName);
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Password", password);
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@roles",roles);

                    command.ExecuteNonQuery();
                    MessageBox.Show("Đăng ký thành công!");
                    MainWindow register = new MainWindow();
                    register.Show();
                    this.Hide();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi đăng ký: {ex.Message}");
            }
            
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow reg = new MainWindow(); 
            reg.Show();
            this.Hide();
        }
    }
    
}
