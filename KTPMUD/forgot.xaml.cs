﻿using System;
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
    /// Interaction logic for forgot.xaml
    /// </summary>
    public partial class forgot : Window
    {
        private string connectionString = "Server=localhost;Database=anphuc;Trusted_Connection=True;";
        public forgot()
        {
            InitializeComponent();
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            string user = username.Text.Trim();
            string password = newpassword.Password.Trim();
            string news = newpass.Password.Trim();

            if(string.IsNullOrEmpty(user) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(news))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if(news != password)
            {
                MessageBox.Show("Mật khẩu không trùng khớp", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            try
            {
                // Kết nối tới cơ sở dữ liệu và cập nhật mật khẩu
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Kiểm tra tài khoản có tồn tại hay không
                    string checkUserQuery = "SELECT COUNT(*) FROM users WHERE username = @Username";
                    SqlCommand checkUserCommand = new SqlCommand(checkUserQuery, connection);
                    checkUserCommand.Parameters.AddWithValue("@Username", user);

                    int userExists = (int)checkUserCommand.ExecuteScalar();
                    if (userExists == 0)
                    {
                        MessageBox.Show("Tài khoản không tồn tại!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    // Cập nhật mật khẩu
                    string updatePasswordQuery = "UPDATE users SET password = @NewPassword WHERE username = @Username";
                    SqlCommand updateCommand = new SqlCommand(updatePasswordQuery, connection);
                    updateCommand.Parameters.AddWithValue("@NewPassword", news);
                    updateCommand.Parameters.AddWithValue("@Username", user);

                    updateCommand.ExecuteNonQuery();
                    MessageBox.Show("Đổi mật khẩu thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    MainWindow back = new MainWindow();
                    back.Show();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Có lỗi xảy ra: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            MainWindow back = new MainWindow();
            back.Show();
            this.Close();
        }
    }
}
