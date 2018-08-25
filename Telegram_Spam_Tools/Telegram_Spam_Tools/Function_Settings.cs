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
        MainForm f = new MainForm();
        private string active = string.Empty;
        private string stop = string.Empty;
        private string searched = string.Empty;
        private CheckBox headerCheckBox = new CheckBox();      
        internal void search_value(string ser)
        {
            searched = ser;
        }
        //Function datagridview no insert checkbox (use reload data or Create datasource)
        #region reload
        internal void reload(DataGridView bunifuCustomDataGrid1, string select, int species)
        {
            SQLite_Funciton_Query sfq = new SQLite_Funciton_Query();
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
                sfq.SQLite_Query(bunifuCustomDataGrid1, select);
                add_checkbox(bunifuCustomDataGrid);               
            }
            if (species_use == 1)
            {
                sfq.SQLite_Query(bunifuCustomDataGrid1, select);
                add_checkbox(bunifuCustomDataGrid);
            }
        }
        #endregion
        //Mouse Event Click DataGridView (mouse.right, mouse.left)
        #region GridView MouseClick
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
        //Function datagridview and insert checkbox(use create datasource)
        #region Gridview
        internal void gridview(DataGridView bunifuCustomDataGrid1, string select, int species)
        {
            SQLite_Funciton_Query sfq = new SQLite_Funciton_Query();
            species_use = species;
            bunifuCustomDataGrid = bunifuCustomDataGrid1;
            selected = select;
            bunifuCustomDataGrid.DataSource = null;
            bunifuCustomDataGrid.ReadOnly = true;
            if (species_use == 0)
            {
                sfq.SQLite_Query(bunifuCustomDataGrid1, select);
                add_checkbox(bunifuCustomDataGrid);
            }
            if (species_use == 1)
            {
                sfq.SQLite_Query(bunifuCustomDataGrid1, select);
                add_checkbox(bunifuCustomDataGrid);
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
                headerCheckBox.Location = new Point(headerCellLocation.X + 2, headerCellLocation.Y + 15);
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
