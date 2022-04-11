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
using System.Data;
using Microsoft.Data.SqlClient;
using MySqlConnector;

namespace Login
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        readonly string strConn = "Data Source=127.0.0.1; Initial Catalog=tec; User ID=test1; Password=1234;";
        
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnEnviar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (MySqlConnection connection = new(strConn))
                {
                
                    connection.Open();
                    MessageBox.Show(connection.State.ToString());

                    /*****************************************************
                     * 
                     *  Montar if para SELECT con las credenciales 
                     *  
                     *  De los TextBoxs
                     *  
                     *  Si devuelve filas, login correcto
                     *  
                     *  Si no devuelve filas, Login erroneo
                     * 
                     * *****************************************************/

                    MySqlCommand cmd = connection.CreateCommand(); 
                    cmd.CommandText = "SELECT user, pwd FROM credenciales WHERE user='"+txtUserIn.Text+
                        "' AND pwd='"+txtPwdIn.Text+"';";
                    MessageBox.Show(cmd.CommandText.ToString());

                    MySqlDataReader dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        MessageBox.Show("LOGIN CORRECTO");
                    }
                    else
                    {
                        MessageBox.Show("USER/PASSWORD INCORRECTO");
                    }
                    
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
        
    }
    }
}
