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
using Microsoft.Data.SqlClient;
using MySqlConnector;

namespace Login
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

        private void BtnEnviar_Click(object sender, RoutedEventArgs e)
        {
            //This should be assigned in a private file for security
            //Define Datasource and root user in app.config

            string strConn = "Data Source=127.0.0.1;";
            try
            {
                SqlConnectionStringBuilder builder =
            new SqlConnectionStringBuilder(strConn);

                // Supply the additional values.
                builder.UserID = txtUserIn.Text;
                builder.Password = txtPwdIn.Password;

                using (MySqlConnection connection = new(builder.ConnectionString))
                {
                    connection.Open();
                    MessageBox.Show(connection.State.ToString());
                    connection.Close(); 
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
        
    }
    }
}
