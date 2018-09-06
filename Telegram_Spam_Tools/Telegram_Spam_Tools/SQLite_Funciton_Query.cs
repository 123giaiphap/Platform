using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Data;

namespace Telegram_Spam_Tools
{
    class SQLite_Funciton_Query
    {
        private string connect = @"Data Source = telegram.db;Verison=3";
        public void SQLite_Query_Status(string Select_Querry, string Location_String )
        {
            using (SQLiteConnection sqlConn = new SQLiteConnection(connect))
            {
                try
                {
                    string sql = Select_Querry + Location_String + "'";
                    using (var cmd = new SQLiteCommand(sql, sqlConn))
                    {
                        sqlConn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        public void SQLite_Query_ListBox(ListBox List_Box, string Select_Querry, string Location_String)
        {
            using (SQLiteConnection con = new SQLiteConnection(connect))
            {
                con.Open();
                string stm = Select_Querry + Location_String + "'";
                try
                {
                    using (SQLiteCommand cmd = new SQLiteCommand(stm, con))
                    {
                        using (SQLiteDataReader rdr = cmd.ExecuteReader())
                        {
                            while (rdr.Read())
                            {
                                List_Box.Items.Add(rdr.GetString(0));
                            }
                        }
                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        public void SQLite_Query_ListBox_Inverse(ListBox List_Box, string Select_Querry, string Location_String)
        {
            using (SQLiteConnection con = new SQLiteConnection(connect))
            {
                con.Open();
                string stm = Select_Querry + Location_String + "')";
                try
                {
                    using (SQLiteCommand cmd = new SQLiteCommand(stm, con))
                    {
                        using (SQLiteDataReader rdr = cmd.ExecuteReader())
                        {
                            while (rdr.Read())
                            {
                                List_Box.Items.Add(rdr.GetString(0));
                            }
                        }
                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        public void SQLite_Query_Delete(string Select_Querry, string Location_String)
        {
            using (SQLiteConnection sqlConn = new SQLiteConnection(connect))
            {
                try
                {
                    string sql = Select_Querry + Location_String + "'";
                    using (var cmd = new SQLiteCommand(sql, sqlConn))
                    {
                        sqlConn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        public void SQLite_Query(DataGridView Data_GridView, string Select_Querry)
        {
            using (SQLiteConnection con = new SQLiteConnection(connect))
            {
                //Add a CheckBox Column to the DataGridView Header Cell.
                con.Open();
                try
                {
                    using (SQLiteCommand cmd = new SQLiteCommand(Select_Querry, con))
                    {
                        SQLiteDataAdapter da = new SQLiteDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        try
                        {
                            da.Fill(ds);
                            DataTable dt = ds.Tables[0];
                            Data_GridView.DataSource = dt;

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        public void SQLite_Query_Log_Database(DataGridView Data_GridView, string Select_Querry)
        {
            using (SQLiteConnection con = new SQLiteConnection(connect))
            {
                //Add a CheckBox Column to the DataGridView Header Cell.
                con.Open();
                try
                {
                    string sql = Select_Querry;
                    using (var cmd = new SQLiteCommand(sql, con))
                    {                       
                        cmd.ExecuteNonQuery();
                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        public void SQLite_Query_Combox(ComboBox Combo_Box, string Select_Querry)
        {
            using (SQLiteConnection con = new SQLiteConnection(connect))
            {
                con.Open();
                string stm = Select_Querry;
                try
                {
                    using (SQLiteCommand cmd = new SQLiteCommand(stm, con))
                    {
                        using (SQLiteDataReader rdr = cmd.ExecuteReader())
                        {
                            while (rdr.Read())
                            {
                                Combo_Box.Items.Add(rdr.GetString(0));
                            }
                        }
                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
