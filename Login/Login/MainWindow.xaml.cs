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


        /**************************************************************************************
         * 
         * 
         *  CAMBIAR USUARIO CONTRASEÑA PARA QUE ENTRE LOS DATOS DE RELLENO EN STRCONN
         * 
         *  https://docs.microsoft.com/en-us/dotnet/framework/data/adonet/connection-string-builders
         * 
         * 
         * *************************************************************************************/




        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnEnviar_Click(object sender, RoutedEventArgs e)
        {
            //This should be assigned in a private file for security
            string strConn = "Data Source=127.0.0.1;"; //  User ID="+txtUserIn.Text.ToString()+ "; Password="+txtPwdIn.Password.ToString()+";";
            
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

                    /*****************************************************
                     * 
                     *  
                     *  to check if user credentials are ok, create a SELECT command to use
                     *  
                     *  reading the txtBox filled with user´s data
                     *  
                     *  With a datareader ask the database.
                     *  
                     *  If user filled data matchs a register,
                     *  
                     *  the datareader will have rows, 
                     *  
                     *  so User/Password are ok and Login is correct
                     * 
                     * ****************************************************

                    MySqlCommand cmd = connection.CreateCommand(); 
                    cmd.CommandText = "SELECT user, pwd FROM tec.credenciales WHERE user='"+txtUserIn.Text+
                        "' AND pwd='"+txtPwdIn.Password.ToString()+"';";

                    MySqlDataReader dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        MessageBox.Show("LOGIN OK");
                    }
                    else
                    {
                        MessageBox.Show("WRONG USER/PASSWORD");
                    }
                    */

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
