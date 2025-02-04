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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;

namespace KTPMUD
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void register_Click(object sender, RoutedEventArgs e)
        {
            Trangdangky trangdangky = new Trangdangky();
            trangdangky.Show();
            Hide();
        }

        private void forgotPassword_Click(object sender, RoutedEventArgs e)
        {
            forgot quenmatkhau = new forgot();
            quenmatkhau.Show();
            Hide();
        }

        private void changePassword_Click(object sender, RoutedEventArgs e)
        {
            Change change = new Change();
            change.Show();
            this.Hide();
        }

        private void username_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void login_Click_1(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtUsername.Text) || string.IsNullOrEmpty(txtPassword.Password))
            {
                MessageBox.Show("Vui lòng nhập tên đăng nhập và mật khẩu!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string username = txtUsername.Text.Trim();
            string password = txtPassword.Password.Trim();
            string roles = "";

            if (rbCapXa.IsChecked == true) roles = "Cấp Xã";
            else if (rbCapHuyen.IsChecked == true) roles = "Cấp Huyện";
            else if (rbCoSoHatGiong.IsChecked == true) roles = "Cơ sở sản xuất giống cây"; // Đúng tên trong DB
            else if (rbCoSoGo.IsChecked == true) roles = "Cơ sở sản xuất gỗ"; // Đúng tên trong DB
            else if (rbCoSoDongVat.IsChecked == true) roles = "Cơ sở quản lý động vật"; // Đúng tên trong DB

            string connectionString = "Data Source=localhost;Initial Catalog=anphuc;Integrated Security=True;";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM users WHERE Username = @username AND Password = @password AND roles = @roles";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);
                cmd.Parameters.AddWithValue("@roles", roles);

                conn.Open();
                int count = (int)cmd.ExecuteScalar();

                if (count > 0)
                {
                    MessageBox.Show("Đăng nhập thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);

                    // Mở trang theo vai trò
                    switch (roles)
                    {
                        case "Cấp Xã":
                            new dongvat().Show();
                            this.Hide();
                            break;
                        case "Cấp Huyện":
                            new caphuyen().Show();
                            this.Hide();
                            break;
                        case "Cơ sở sản xuất giống cây":
                            new giongcaytrong().Show();
                            this.Hide(); // Ẩn cửa sổ đăng nhập
                            break;
                        case "Cơ sở sản xuất gỗ":
                            new quanlygo().Show();
                            this.Hide();
                            break;
                        case "Cơ sở quản lý động vật":
                            new dongvat().Show();
                            this.Hide();
                            break;
                    }
                }
                else
                {
                    MessageBox.Show("Sai tên đăng nhập, mật khẩu hoặc vai trò!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
