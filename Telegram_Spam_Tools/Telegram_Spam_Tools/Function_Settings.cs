using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Data.SQLite;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;

namespace Telegram_Spam_Tools
{
    class Function_Settings
    {
        private string connect = @"Data Source = telegram.db;Verison=3";
        private DataGridView bunifuCustomDataGrid = new DataGridView();
        private int species_use;
        private string selected = string.Empty;
        private string deleted = string.Empty;
        private TabControl tabcol;
        private string name = string.Empty;
        Form1 f = new Form1();
        private string active = string.Empty;
        private string stop = string.Empty;
        private string searched = string.Empty;
        private int rowIndex;
        private int columnIndex;
        private int stt;
        private CheckBox headerCheckBox = new CheckBox();
        private ContextMenuStrip m = new ContextMenuStrip();
        internal void status(string atc, string stp)
        {
            active = atc;
            stop = stp;
        }
        internal void delete(string del)
        {
            deleted = del;
        }
        internal void search_value(string ser)
        {
            searched = ser;
        }
        internal void select_tab(TabControl tabcontrol, string nametab)
        {
            this.tabcol = tabcontrol;
            this.name = nametab;
        }
        //Function datagridview no insert checkbox (use reload data or Create datasource)
        #region reload
        internal void reload(DataGridView bunifuCustomDataGrid1, string select, int species)
        {
            this.species_use = species;
            this.bunifuCustomDataGrid = bunifuCustomDataGrid1;
            this.selected = select;
            bunifuCustomDataGrid.ClearSelection();
            bunifuCustomDataGrid.DataSource = null;
            bunifuCustomDataGrid.Rows.Clear();
            bunifuCustomDataGrid.Columns.Clear();
            bunifuCustomDataGrid.Refresh();
            bunifuCustomDataGrid.ReadOnly = true;
            if (species_use == 0)
            {
                using (SQLiteConnection con = new SQLiteConnection(connect))
                {
                    //Add a CheckBox Column to the DataGridView Header Cell.
                    con.Open();
                    string stm = select;
                    try
                    {
                        using (SQLiteCommand cmd = new SQLiteCommand(stm, con))
                        {
                            SQLiteDataAdapter da = new SQLiteDataAdapter(cmd);
                            DataSet ds = new DataSet();
                            try
                            {
                                da.Fill(ds);
                                DataTable dt = ds.Tables[0];
                                bunifuCustomDataGrid1.DataSource = dt;

                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Errrrrrrror");
                            }
                        }
                        con.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                add_checkbox(bunifuCustomDataGrid);
                m.Items.Clear();
                ToolStripItem item0 = m.Items.Add("Edit ");
                item0.Click += new EventHandler(item0_Click);
                ToolStripItem item1 = m.Items.Add("Delete ");
                item1.Click += new EventHandler(item1_Click);
                ToolStripItem item2 = m.Items.Add("Active ");
                item2.Click += new EventHandler(item2_Click);
                bunifuCustomDataGrid.MouseClick += new MouseEventHandler(bunifuCustomDataGrid_MouseClick);
                bunifuCustomDataGrid.CellFormatting += new DataGridViewCellFormattingEventHandler(bunifuCustomDataGrid_CellFormatting);
            }
            if (species_use == 1)
            {
                using (SQLiteConnection con = new SQLiteConnection(connect))
                {
                    //Add a CheckBox Column to the DataGridView Header Cell.
                    con.Open();
                    string stm = select;
                    try
                    {
                        using (SQLiteCommand cmd = new SQLiteCommand(stm, con))
                        {
                            SQLiteDataAdapter da = new SQLiteDataAdapter(cmd);
                            DataSet ds = new DataSet();
                            try
                            {
                                da.Fill(ds);
                                DataTable dt = ds.Tables[0];
                                bunifuCustomDataGrid1.DataSource = dt;

                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Errrrrrrror");
                            }
                        }
                        con.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                add_checkbox(bunifuCustomDataGrid);
                m.Items.Clear();
                ToolStripItem item0 = m.Items.Add("Edit ");
                item0.Click += new EventHandler(item0_Click);
                ToolStripItem item1 = m.Items.Add("Delete ");
                item1.Click += new EventHandler(item1_Click);
                ToolStripItem item2 = m.Items.Add("Active ");
                item2.Click += new EventHandler(item2_Click);
                bunifuCustomDataGrid.MouseClick += new MouseEventHandler(bunifuCustomDataGrid_MouseClick);
                bunifuCustomDataGrid.CellFormatting += new DataGridViewCellFormattingEventHandler(bunifuCustomDataGrid_CellFormatting);
            }
        }
        #endregion
        //Mouse Event Click DataGridView (mouse.right, mouse.left)
        #region GridView MouseClick
        private void bunifuCustomDataGrid_MouseClick(object sender, MouseEventArgs e)
        {
            stt = 0;
            m.Items[2].Visible = false;
            //Check to ensure that the row CheckBox is clicked.
            if (e.Button == MouseButtons.Right)
            {
                try
                {
                    MessageBox.Show(species_use.ToString());
                    bunifuCustomDataGrid.ClearSelection();
                    var hti = bunifuCustomDataGrid.HitTest(e.X, e.Y);
                    this.bunifuCustomDataGrid.Rows[hti.RowIndex].Selected = true;
                    this.rowIndex = hti.RowIndex;
                    this.columnIndex = hti.ColumnIndex;
                    this.m.Show(this.bunifuCustomDataGrid, e.Location);
                    if (bunifuCustomDataGrid.Rows[this.rowIndex].Cells[this.columnIndex].Value.ToString() == "Active")
                    {
                        stt = 1;
                        m.Items[2].Visible = true;
                        m.Items[2].Text = "Stop";
                    }
                    if (bunifuCustomDataGrid.Rows[this.rowIndex].Cells[this.columnIndex].Value.ToString() == "Stop")
                    {
                        stt = 2;
                        m.Items[2].Visible = true;
                        m.Items[2].Text = "Active";
                    }
                    m.Show(Cursor.Position);
                }
                catch (Exception)
                {

                }
            }
        }
        #endregion
        //Search Value in Datagridview
        #region Search
        internal void search_item(DataGridView bunifuCustomDataGrid1, string sch)
        {
            sch = searched;
            using (SQLiteConnection con = new SQLiteConnection(connect))
            {
                //Add a CheckBox Column to the DataGridView Header Cell.
                con.Open();
                string stm = sch;
                try
                {
                    using (SQLiteCommand cmd = new SQLiteCommand(stm, con))
                    {
                        SQLiteDataAdapter da = new SQLiteDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        try
                        {
                            da.Fill(ds);
                            DataTable dt = ds.Tables[0];
                            bunifuCustomDataGrid1.DataSource = dt;

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
        #endregion
        //Passwordchar in datagridview
        #region Gridview Format Cells
        private void bunifuCustomDataGrid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (bunifuCustomDataGrid.Columns[e.ColumnIndex].Name == "Password" && e.Value != null)
            {
                bunifuCustomDataGrid.Rows[e.RowIndex].Tag = e.Value;
                e.Value = new String('*', e.Value.ToString().Length);
            }
        }
        #endregion
        //Contextmenutrip Event
        #region Contextmenutrip Event
        //Contextmenutrip Event Edit item click
        private void item0_Click(object sender, EventArgs e)
        {
            ToolStripItem clickedItem = sender as ToolStripItem;
            tabcol.SelectTab(name);

        }
        //Contextmenutrip Event Delete item click
        private void item1_Click(object sender, EventArgs e)
        {
            ToolStripItem clickedItem = sender as ToolStripItem;
            delete_item(bunifuCustomDataGrid);
            reload(bunifuCustomDataGrid, selected, species_use);
            // your code here
        }
        //Contextmenutrip Event Active item click
        private void item2_Click(object sender, EventArgs e)
        {
            ToolStripItem clickedItem = sender as ToolStripItem;
            if (stt == 1)
            {
                status_active(bunifuCustomDataGrid);
                reload(bunifuCustomDataGrid, selected, species_use);
            }
            if (stt == 2)
            {
                status_stop(bunifuCustomDataGrid);
                reload(bunifuCustomDataGrid, selected, species_use);
            }

        }
        //Contextmenutrip Event delete item after clicking
        private void delete_item(DataGridView bunifuCustomDataGrid)
        {
            using (SQLiteConnection con = new SQLiteConnection(connect))
            {
                try
                {
                    string sql = deleted + bunifuCustomDataGrid.Rows[this.rowIndex].Cells[1].Value.ToString() + "'";
                    using (var cmdd = new SQLiteCommand(sql, con))
                    {
                        con.Open();
                        cmdd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            bunifuCustomDataGrid.Rows.RemoveAt(this.rowIndex);
            reload(bunifuCustomDataGrid, selected, species_use);
        }
        //Contextmenutrip Event Active-Active item after clicking
        internal void status_active(DataGridView bunifuCustomDataGrid1)
        {
            using (SQLiteConnection sqlConn = new SQLiteConnection(connect))
            {
                try
                {
                    string sql = active + bunifuCustomDataGrid.Rows[this.rowIndex].Cells[1].Value.ToString() + "'";
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
            //  m.Items[2].Text = "Stop";
        }
        //Contextmenutrip Event Active-Stop item after clicking
        internal void status_stop(DataGridView bunifuCustomDataGrid1)
        {
            using (SQLiteConnection sqlConn = new SQLiteConnection(connect))
            {
                try
                {

                    string sql = stop + bunifuCustomDataGrid.Rows[this.rowIndex].Cells[1].Value.ToString() + "'";
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
            // m.Items[2].Text = "Active";
        }
        #endregion
        //Function datagridview and insert checkbox(use create datasource)
        #region gridview
        internal void gridview(DataGridView bunifuCustomDataGrid1, string select, int species)
        {
            species_use = species;
            bunifuCustomDataGrid = bunifuCustomDataGrid1;
            selected = select;
            bunifuCustomDataGrid.DataSource = null;
            bunifuCustomDataGrid.ReadOnly = true;
            if (species_use == 0)
            {
                using (SQLiteConnection con = new SQLiteConnection(connect))
                {
                    //Add a CheckBox Column to the DataGridView Header Cell.
                    con.Open();
                    string stm = select;
                    try
                    {
                        using (SQLiteCommand cmd = new SQLiteCommand(stm, con))
                        {
                            SQLiteDataAdapter da = new SQLiteDataAdapter(cmd);
                            DataSet ds = new DataSet();
                            try
                            {
                                da.Fill(ds);
                                DataTable dt = ds.Tables[0];
                                bunifuCustomDataGrid1.DataSource = dt;

                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Errrrrrrror");
                            }
                        }
                        con.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                add_checkbox(bunifuCustomDataGrid);
                m.Items.Clear();
                ToolStripItem item0 = m.Items.Add("Edit ");
                item0.Click += new EventHandler(item0_Click);
                ToolStripItem item1 = m.Items.Add("Delete ");
                item1.Click += new EventHandler(item1_Click);
                ToolStripItem item2 = m.Items.Add("Active ");
                item2.Click += new EventHandler(item2_Click);
                bunifuCustomDataGrid.MouseClick += new MouseEventHandler(bunifuCustomDataGrid_MouseClick);
                bunifuCustomDataGrid.CellFormatting += new DataGridViewCellFormattingEventHandler(bunifuCustomDataGrid_CellFormatting);
            }
            if (species_use == 1)
            {
                using (SQLiteConnection con = new SQLiteConnection(connect))
                {
                    //Add a CheckBox Column to the DataGridView Header Cell.
                    con.Open();
                    string stm = select;
                    try
                    {
                        using (SQLiteCommand cmd = new SQLiteCommand(stm, con))
                        {
                            SQLiteDataAdapter da = new SQLiteDataAdapter(cmd);
                            DataSet ds = new DataSet();
                            try
                            {
                                da.Fill(ds);
                                DataTable dt = ds.Tables[0];
                                bunifuCustomDataGrid1.DataSource = dt;

                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Errrrrrrror");
                            }
                        }
                        con.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }

                add_checkbox(bunifuCustomDataGrid);
                m.Items.Clear();
                ToolStripItem item0 = m.Items.Add("Edit ");
                item0.Click += new EventHandler(item0_Click);
                ToolStripItem item1 = m.Items.Add("Delete ");
                item1.Click += new EventHandler(item1_Click);
                ToolStripItem item2 = m.Items.Add("Active ");
                item2.Click += new EventHandler(item2_Click);
                bunifuCustomDataGrid.MouseClick += new MouseEventHandler(bunifuCustomDataGrid_MouseClick);
                bunifuCustomDataGrid.CellFormatting += new DataGridViewCellFormattingEventHandler(bunifuCustomDataGrid_CellFormatting);
            }
        }
        #endregion
        //Function checkbox
        #region checkbox
        //Function Insert checkbox in datagridview
        private void add_checkbox(DataGridView bunifuCustomDataGrid1)
        {
            try
            {
                Point headerCellLocation = bunifuCustomDataGrid1.GetCellDisplayRectangle(0, -1, true).Location;
                //Place the Header CheckBox in the Location of the Header Cell.
                headerCheckBox.Location = new Point(headerCellLocation.X + 8, headerCellLocation.Y + 13);
                headerCheckBox.BackColor = Color.SeaGreen;
                headerCheckBox.Size = new Size(15, 15);
                ////Assign Click event to the Header CheckBox.
                headerCheckBox.Click += new EventHandler(HeaderCheckBox_Clicked);
                bunifuCustomDataGrid1.Controls.Add(headerCheckBox);
                //Add a CheckBox Column to the DataGridView at the first position
                DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
                checkBoxColumn.HeaderText = "";
                checkBoxColumn.Width = 30;
                checkBoxColumn.Name = "checkBoxColumn";
                bunifuCustomDataGrid1.Columns.Insert(0, checkBoxColumn);
                //Assign Click event to the DataGridView Cell.
                bunifuCustomDataGrid1.CellContentClick += new DataGridViewCellEventHandler(DataGridView_CellClick);
            }
            catch (Exception ex)
            {
                
            }
            
        }
        //Function header checkbox when click
        private void HeaderCheckBox_Clicked(object sender, EventArgs e)
        {
            //Necessary to end the edit mode of the Cell.
            bunifuCustomDataGrid.EndEdit();
            //Loop and check and uncheck all row CheckBoxes based on Header Cell CheckBox.
            foreach (DataGridViewRow row in bunifuCustomDataGrid.Rows)
            {
                DataGridViewCheckBoxCell checkBox = (row.Cells["checkBoxColumn"] as DataGridViewCheckBoxCell);
                checkBox.Value = headerCheckBox.Checked;
            }
        }
        //Function checkbox all row datagridview
        private void DataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //Check to ensure that the row CheckBox is clicked.
            if (e.RowIndex >= 0 && e.ColumnIndex == 0)
            {
                //Loop to verify whether all row CheckBoxes are checked or not.
                bool isChecked = true;
                foreach (DataGridViewRow row in bunifuCustomDataGrid.Rows)
                {
                    if (Convert.ToBoolean(row.Cells["checkBoxColumn"].EditedFormattedValue) == false)
                    {
                        isChecked = false;
                        break;
                    }
                }
                headerCheckBox.Checked = isChecked;
            }
        }
        #endregion

    }
}
