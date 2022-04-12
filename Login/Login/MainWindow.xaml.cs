﻿using System;
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

<<<<<<< HEAD


        /********************************************************************************
         * 
         * https://stackoverflow.com/questions/29729658/wpf-open-new-window-in-wpf
         * 
         * Para sustituir UI de login por UI de herramientas SQL
         * 
         * ******************************************************************************/

=======
>>>>>>> a62690b1b3689de145a2b26cb3ca8465d958d744
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

                    //CAMBIO DE INTERFAZ MAIN A HERRAMIENTAS SQL

                    grdMain.Visibility = System.Windows.Visibility.Collapsed;
                    grdTools.Visibility = System.Windows.Visibility.Visible;
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
