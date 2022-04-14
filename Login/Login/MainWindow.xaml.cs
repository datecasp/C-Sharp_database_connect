﻿using System;
using System.Collections.Generic;
using System.Configuration;
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
using Login.Tools;

namespace Login
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MySqlConnection connection = null;
        private string strConn;
        private string finalStrConn;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnSubmit_Click(object sender, RoutedEventArgs e)
        {
            //Read DataSource from App.Config
            if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["DataSource"]))
            { 
                strConn = ConfigurationManager.AppSettings["DataSource"]; 
            }
            else
            {
                //Can´t read app.config data
                MessageBox.Show("NULL APPCONFIG");
            }

            if (txtUserIn.Text != "" && txtPwdIn.Password != "")
            {
                    SqlConnectionStringBuilder builder =
                new SqlConnectionStringBuilder(strConn);

                    // Supply the additional values.
                    builder.UserID = txtUserIn.Text;
                    builder.Password = txtPwdIn.Password;

                    connection = new(builder.ConnectionString);
                    finalStrConn = connection.ConnectionString; 

                    try
                    {
                        connection.Open();
                        grdMain.Visibility = System.Windows.Visibility.Collapsed;
                        grdTools.Visibility = System.Windows.Visibility.Visible;
                    }
                    catch
                    {
                        MessageBox.Show("USER/PASSWORD INCORRECT");
                    }            
            }
            else
            {
                MessageBox.Show("ENTER YOUR CREDENTILALS");
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
            grdTools.Visibility=System.Windows.Visibility.Collapsed;
            stkSelect.Visibility = System.Windows.Visibility.Visible;  
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            grdTools.Visibility=System.Windows.Visibility.Collapsed;
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
                connection.Close();
                connection.Open();

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
            Tools.Tools.ResetTextBoxes();
        }

        //DELETE
        private void BtnDeleteData_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                connection.Close();
                connection.Open();

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
            Tools.Tools.ResetTextBoxes();
        }

        //SELECT
        private void BtnSelectData_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                connection.Close();
                connection.Open();

                MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "SELECT user, pwd FROM tec.credenciales WHERE user='" + txtUserSelect.Text +
                    "';";

                MySqlDataReader dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    while(dr.Read())
                    {
                        MessageBox.Show("USER =" + dr.GetString(0) + " PWD =" + dr.GetString(1));
                    }
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
            Tools.Tools.ResetTextBoxes();

        }
    }
}
