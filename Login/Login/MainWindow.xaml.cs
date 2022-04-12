using Microsoft.Data.SqlClient;
using MySqlConnector;
using System;
using System.Windows;

namespace Login
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MySqlConnection connection = null;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnSubmit_Click(object sender, RoutedEventArgs e)
        {
            //This should be assigned in a private file for security
            //Define Datasource in app.config
            string strConn = "Data Source=127.0.0.1;";

            try
            {
                SqlConnectionStringBuilder builder =
            new SqlConnectionStringBuilder(strConn);

                // Supply the additional values.
                builder.UserID = txtUserIn.Text;
                builder.Password = txtPwdIn.Password;

                connection = new(builder.ConnectionString);

                connection.Open();
                grdMain.Visibility = System.Windows.Visibility.Collapsed;
                grdTools.Visibility = System.Windows.Visibility.Visible;
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        //TOOLS UI REGION
        private void BtnInsert_Click(object sender, RoutedEventArgs e)
        {
            grdTools.Visibility = System.Windows.Visibility.Collapsed;
            grdInsert.Visibility = System.Windows.Visibility.Visible;
        }

        private void BtnSelect_Click(object sender, RoutedEventArgs e)
        {
            grdTools.Visibility = System.Windows.Visibility.Collapsed;
            stkSelect.Visibility = System.Windows.Visibility.Visible;
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            grdTools.Visibility = System.Windows.Visibility.Collapsed;
            stkDelete.Visibility = System.Windows.Visibility.Visible;
        }

        private void BtnLogout_Click(object sender, RoutedEventArgs e)
        {
            connection.Close();
            this.Close();
        }


        //INSERT
        private void BtnInsertData_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show(connection.State.ToString());
            try
            {
                MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "INSERT INTO tec.credenciales (user, pwd) VALUES ('" + txtUserInsert.Text +
                    "','" + txtPassInsert.Text + "');";
                //MessageBox.Show(cmd.CommandText.ToString());

                cmd.ExecuteReader();
                MessageBox.Show("INSERT DONE");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        //DELETE
        private void BtnDeleteData_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "DELETE FROM tec.credenciales WHERE user='" + txtUserDelete.Text +
                    "';";
                //MessageBox.Show(cmd.CommandText.ToString());

                cmd.ExecuteReader();
                MessageBox.Show("DELETE DONE");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        //SELECT
        private void BtnSelectData_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "SELECT user, pwd FROM tec.credenciales WHERE user='" + txtUserSelect.Text +
                    "';";

                MySqlDataReader dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    dr.Read();
                    MessageBox.Show("USER =" + dr.GetString(0) + " PWD =" + dr.GetString(1));
                }
                else
                {
                    MessageBox.Show("USER NOT FOUND");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        //BACK
        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            grdTools.Visibility = System.Windows.Visibility.Visible;
            grdInsert.Visibility = System.Windows.Visibility.Collapsed;
            stkDelete.Visibility = System.Windows.Visibility.Collapsed;
            stkSelect.Visibility = System.Windows.Visibility.Collapsed;
        }
    }
}
