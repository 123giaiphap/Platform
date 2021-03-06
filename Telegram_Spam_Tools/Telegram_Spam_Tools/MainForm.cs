﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Resources;
using System.Drawing.Text;

namespace Telegram_Spam_Tools
{
    public partial class MainForm : Form
    {

        private string DBConnect = string.Empty;

        private ContextMenuStrip m = new ContextMenuStrip();

        private int Row_Index;
        private int Column_Index;

        private int Default_GridView;
        internal int GridView_Index;

        private int Multiple_Column_Merged_GridView = 0;
        private int View_Column_Type;//Hien thi column theo hinh anh

        private string Left_Item_ListBox_SQL = string.Empty;
        private string Righ_Item_ListBox_SQL = string.Empty;
        private string select = string.Empty;//Select_All_GridView_SQL
        private string active = string.Empty;//Active_Status_GridView_SQL
        private string stop = string.Empty;//Disable_Status_GridView_SQL
        private string delete = string.Empty;//Delete_Row_GridView_SQL
        private string search = string.Empty;//Search_Row_GridView_SQL

        private string Cell_Back_Color = string.Empty;

        private string Font_Fore_Color = string.Empty;
        private string Font_Name = string.Empty;
        private int Font_Size;
        private string Log_Text_Concatenate;//Log+Text
        private string User_ACL = "Administrator";

        System.Media.SoundPlayer Clicked_Sound = new System.Media.SoundPlayer("click.wav");

        public MainForm()
        {
            DBConnect = @"Data Source = telegram.db;Verison=3";
            InitializeComponent();
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            bunifuTextbox1.text = "Enter some text here";
            bunifuTextbox2._TextBox.PasswordChar = '*';
            Total_Status();
            Lang_Image_Font();
        }
        private void Waiting_Proccessing_Loading()
        {
            for (int i = 0; i <= 500; i++)
                Thread.Sleep(10);
        }
        #region Format DataGridView
        private void Default_Template_Gridview()
        {
            try
            {
                Main_GridView.DataSource = null;
                Main_GridView.Refresh();
                Main_GridView.RowHeadersVisible = false;
                Main_GridView.AllowUserToAddRows = false;
                Log_GridView.RowHeadersVisible = false;
                Log_GridView.AllowUserToAddRows = false;
                Main_GridView.ColumnHeadersHeight = 45;
                Main_GridView.HeaderBgColor = Color.SeaGreen;
                Main_GridView.HeaderForeColor = Color.White;
                Log_GridView.HeaderForeColor = Color.White;
                Main_GridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                Log_GridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion
        #region Button A
        private void btnUser_Click(object sender, EventArgs e)
        {
            this.Left_Item_ListBox_SQL = SQL_ListBox("Link", "LinkName", "IDLink", "LinkUser", "IDUser", 0);
            this.Righ_Item_ListBox_SQL = SQL_ListBox("Link", "LinkName", "IDLink", "LinkUser", "IDUser", 1);
            this.select = SQL_Select("User", new string[] { "IDUser", "Status", "UserName", "Password", "UserAgent", "ProxyIP", "ProxyPort" +
                "", "Description", "DeleteDay", "CreateDay" }, new string[] { "LinkUser" }, new string[] { "IDUser" }, new string[] { "Link Used"},0);
            button_fuction(0,"tabPage1",Main_GridView,1);

        }
        #endregion
        #region Button B
        private void btnLink_Click(object sender, EventArgs e)
        {
            this.select = SQL_Select("Link", new string[] { "IDLink", "Status", "LinkName", "Description" +
                "", "CreatedDate" }, new string[] { "LinkGroup", "LinkComment", "LinkUser" }, new string[] { "IDLink", "IDLink", "IDLink" }, new string[] { "Group Used", "Comment Used", "User Used" },1);
            button_fuction(0, "tabPage1", Main_GridView,2);
        }
        #endregion
        #region Button C
        private void btnGroup_Click(object sender, EventArgs e)
        {
            this.Left_Item_ListBox_SQL = SQL_ListBox("Link","LinkName","IDLink","LinkGroup","IDGroup",0);
            this.Righ_Item_ListBox_SQL = SQL_ListBox("Link", "LinkName", "IDLink", "LinkGroup", "IDGroup", 1);
            this.select = SQL_Select("ListGroup", new string[] { "IDGroup", "Status", "GroupName", "Password", "CreatedDate" +
                "" }, new string[] { "LinkGroup" }, new string[] { "IDGroup" }, new string[] { "Link Used" }, 1);
            button_fuction(1, "tabPage1", Main_GridView,3);
        }
        #endregion       
        #region Button D
        private void btnDashboard_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab("tabPage2");
            //player.Play();
        }
        #endregion
        #region Button E 
        private void btnCmt_Click(object sender, EventArgs e)
        {
            this.Left_Item_ListBox_SQL = SQL_ListBox("Link", "LinkName", "IDLink", "LinkComment", "IDComment", 0);
            this.Righ_Item_ListBox_SQL = SQL_ListBox("Link", "LinkName", "IDLink", "LinkComment", "IDComment", 1);
            this.select = SQL_Select("Comment", new string[] { "IDCmt", "Status", "Comment", "Description", "CreatedDate" +
                "" }, new string[] { "LinkComment" }, new string[] { "IDComment" }, new string[] { "Link Used" }, 1);
            button_fuction(1, "tabPage1", Main_GridView,4);
        }
        #endregion
        #region Button Start
        private void btnStart_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab("tabPage4");
            Default_Template_Gridview();
            this.select = "select * from  Log";
            Function_Settings dta = new Function_Settings();
            SQLite_Funciton_Query sfq = new SQLite_Funciton_Query();
            dta.gridview(Log_GridView, this.select, this.View_Column_Type);
            sfq.SQLite_Query_Combox(cb1, "SELECT DISTINCT Action FROM Log;");
            sfq.SQLite_Query_Combox(cb2, "SELECT DISTINCT Name FROM Log;");
            this.Log_GridView.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
        }
        #endregion
        private void btnStop_Click(object sender, EventArgs e)
        {
            using (WaitForm frm = new WaitForm(Waiting_Proccessing_Loading))
            {
                frm.ShowDialog(this);
            }
        }
        #region Button Exit-Logout
        private void btnExit_Click(object sender, EventArgs e)
        {
            Environment.Exit(-1);
        }
        #endregion
        internal void Total_Status()
        {
            
        }
        #region Button Reload
        private void btnReload_Click(object sender, EventArgs e)
        {
            Function_Settings dta = new Function_Settings();
            Total_Status();
            if (this.Default_GridView == 1)
            {
                dta.reload(Main_GridView, this.select, this.View_Column_Type);
                Main_GridView.ClearSelection();
            }
            else
            {
                MessageBox.Show("dashboard");
            }
            color();
        }
        #endregion
        #region Format Column DataGridView, Insert Image, Icon, Create Column
        private void color()
        {
            Create_Column_DataGridView cld = new Create_Column_DataGridView();
            m.Items.Clear();
            ToolStripItem item0 = m.Items.Add("Edit ");
            item0.Click += new EventHandler(Menu_Item_0_Click);
            ToolStripItem item1 = m.Items.Add("Delete ");
            item1.Click += new EventHandler(item1_Click);
            if (View_Column_Type == 0)
            {
                for (int i = 0; i < Main_GridView.RowCount; i++)
                {
                    string val = Main_GridView.Rows[i].Cells[2].Value.ToString();
                    if (val == "Active")
                    {
                        Cells(Main_GridView.Rows[i].Cells[3], "currenry");
                    }
                    if (val == "Stop")
                    {
                        Main_GridView.Rows[i].Cells[3].Style.ForeColor = Color.Red;
                    }
                }
            }
            if (View_Column_Type == 1)
            {
                int max = 0;
                for (int j = 0; j < Main_GridView.ColumnCount; j++)
                {
                    max++;
                }

                if (max > 0)
                {
                    //MessageBox.Show(max.ToString());
                    DataGridViewImageColumn imgCol = new DataGridViewImageColumn();
                    cld.Create_Image_Column(imgCol, Main_GridView, "abc", "Image");
                    TextAndImageColumn txtcol = new TextAndImageColumn();
                    cld.Create_TextAndImageColumn(txtcol, Main_GridView, "anhxa", "");
                    TextAndImageColumn txtcol1 = new TextAndImageColumn();
                    cld.Create_TextAndImageColumn(txtcol1, Main_GridView, "anhxa1", "");
                    TextAndImageColumn txtcol2 = new TextAndImageColumn();
                    cld.Create_TextAndImageColumn(txtcol2, Main_GridView, "anhxa2", "");
                    this.Main_GridView.Columns[max].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                    this.Main_GridView.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                    this.Main_GridView.Columns[max + 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                    this.Main_GridView.Columns[max + 2].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                    this.Main_GridView.Columns[max + 3].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                    //bunifuCustomDataGrid1.Columns[max + 1].HeaderCell.Style.BackColor = Color.Transparent;
                    //bunifuCustomDataGrid1.Columns[max + 2].HeaderCell.Style.BackColor = Color.Transparent;
                    //bunifuCustomDataGrid1.Columns[max + 3].HeaderCell.Style.BackColor = Color.Transparent;
                    this.Multiple_Column_Merged_GridView = max + 2;
                    try
                    {
                        for (int i = 0; i < Main_GridView.RowCount; i++)
                        {
                            this.Main_GridView.Rows[i].Cells[1] = new TextAndImageCell();
                            string val = Main_GridView.Rows[i].Cells[2].Value.ToString();
                            Main_GridView.Rows[i].Cells[max + 1].Value = "12";
                            Main_GridView.Rows[i].Cells[max + 2].Value = "12";
                            Main_GridView.Rows[i].Cells[max + 3].Value = "12";
                            if (val == "Active")
                            {
                                Main_GridView.Rows[i].Cells[max].Value = Image.FromFile(@"image_on.png");
                            }
                            if (val == "Stop")
                            {
                                Main_GridView.Rows[i].Cells[max].Value = Image.FromFile(@"image_off.png"); ;
                            }
                            ((TextAndImageCell)Main_GridView.Rows[i].Cells[1]).Image = (Image)imageList1.Images[1];
                            ((TextAndImageCell)Main_GridView.Rows[i].Cells[max + 1]).Image = (Image)imageList1.Images[1];
                            ((TextAndImageCell)Main_GridView.Rows[i].Cells[max + 2]).Image = (Image)imageList1.Images[2];
                            ((TextAndImageCell)Main_GridView.Rows[i].Cells[max + 3]).Image = (Image)imageList1.Images[3];
                        }
                    }
                    catch (Exception)
                    {

                    }
                }
            }
        }
        #endregion
        #region Mouse Click DataGridView Event
        private void bunifuCustomDataGrid1_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                Main_GridView.ClearSelection();
                var hti = Main_GridView.HitTest(e.X, e.Y);
                this.Main_GridView.Rows[hti.RowIndex].Selected = true;
                this.Row_Index = hti.RowIndex;
                this.Column_Index = hti.ColumnIndex;
            }
            catch (Exception)
            {

            }
            if (e.Button == MouseButtons.Right)
            {
                try
                {
                    Main_GridView.ClearSelection();
                    var hti = Main_GridView.HitTest(e.X, e.Y);
                    this.Main_GridView.Rows[hti.RowIndex].Selected = true;
                    this.Row_Index = hti.RowIndex;
                    this.Column_Index = hti.ColumnIndex;
                    this.m.Show(this.Main_GridView, e.Location);
                    m.Show(Cursor.Position);
                }
                catch (Exception)
                {

                }
            }

        }
        #endregion
        #region Event Cell Content Click DataGridView
        private void bunifuCustomDataGrid1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            for (int i = 0; i < Main_GridView.RowCount; i++)
            {
                string val = Main_GridView.Rows[i].Cells[2].Value.ToString();
                if (Main_GridView.Columns[Column_Index].Name == "abc")
                {

                    Bitmap a = (Bitmap)Image.FromFile(@"image_on.png", true);
                    Bitmap b = (Bitmap)Image.FromFile(@"image_off.png", true);
                    if (val == "Active")
                    {

                        this.Row_Index = e.RowIndex;
                        this.Column_Index = e.ColumnIndex;
                        Main_GridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = b;
                        status_actived();
                        Main_GridView.Rows[i].Cells[2].Value = "Stop";
                    }
                    if (val == "Stop")
                    {
                        this.Row_Index = e.RowIndex;
                        this.Column_Index = e.ColumnIndex;
                        Main_GridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = a;
                        status_stoped();
                        Main_GridView.Rows[i].Cells[2].Value = "Active";
                    }

                }
            }
        }
        #endregion
        #region Event Status Active
        internal void status_actived()
        {
            SQLite_Funciton_Query sfq = new SQLite_Funciton_Query();
            sfq.SQLite_Query_Status(this.active, Main_GridView.Rows[this.Row_Index].Cells[1].Value.ToString());
            Log_DataBase("Status Active","Active "+ Main_GridView.Rows[this.Row_Index].Cells[3].Value.ToString());

            //  m.Items[2].Text = "Stop";
        }
        #endregion
        #region Event Status Stop
        internal void status_stoped()
        {
            SQLite_Funciton_Query sfq = new SQLite_Funciton_Query();
            sfq.SQLite_Query_Status(this.stop, Main_GridView.Rows[this.Row_Index].Cells[1].Value.ToString());
            Log_DataBase("Status Stop", "Stop " + Main_GridView.Rows[this.Row_Index].Cells[3].Value.ToString());
        }
        #endregion
        #region Event Press Enter TextBox Search
        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                if (this.GridView_Index == 1)
                {
                    this.search = "select * from User where UserName Like '%" + txtSearch.Text + "%'";
                }
                if (this.GridView_Index == 2)
                {
                    this.search = "select * from Link where LinkName Like '%" + txtSearch.Text + "%'";
                }
                if (this.GridView_Index == 3)
                {
                    this.search = "select * from Comment where Comment Like '%" + txtSearch.Text + "%'";
                }
                if (this.GridView_Index == 4)
                {
                    this.search = "select * from ListGroup where GroupName Like '%" + txtSearch.Text + "%'";
                }
                Function_Settings fnc = new Function_Settings();
                fnc.search_value(this.search);
                fnc.search_item(Main_GridView, this.search);
            }
        }
        #endregion
        #region Painting Cell DataGridView
        private void bunifuCustomDataGrid1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex == Multiple_Column_Merged_GridView - 1 && e.RowIndex > -1 && this.View_Column_Type == 1)
            {
                e.AdvancedBorderStyle.Left = DataGridViewAdvancedCellBorderStyle.None;
                e.AdvancedBorderStyle.Right = DataGridViewAdvancedCellBorderStyle.None;
            }
            if (e.ColumnIndex == Multiple_Column_Merged_GridView && e.RowIndex > -1 && this.View_Column_Type == 1)
            {
                e.AdvancedBorderStyle.Left = DataGridViewAdvancedCellBorderStyle.None;
                e.AdvancedBorderStyle.Right = DataGridViewAdvancedCellBorderStyle.None;
            }
        }
        #endregion
        #region Format Cell DataGridView
        private void bunifuCustomDataGrid1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (Main_GridView.Columns[e.ColumnIndex].Name == "Password" && e.Value != null)
            {
                Main_GridView.Rows[e.RowIndex].Tag = e.Value;
                e.Value = new String('*', e.Value.ToString().Length);
            }
            if (Main_GridView.Columns[e.ColumnIndex].Name == "Status")
            {
                Main_GridView.Columns[e.ColumnIndex].Visible = false;
            }
        }
        #endregion
        #region Listbox 
        private void ListBox_Item()
        {
            SQLite_Funciton_Query sfq = new SQLite_Funciton_Query();
            sfq.SQLite_Query_ListBox(listBox1, Left_Item_ListBox_SQL, Main_GridView.Rows[this.Row_Index].Cells[1].Value.ToString());
            sfq.SQLite_Query_ListBox_Inverse(listBox2, Righ_Item_ListBox_SQL, Main_GridView.Rows[this.Row_Index].Cells[1].Value.ToString());
        }
        #endregion
        #region Listbox Inverse
        private void ListBox_Item_Inverse(int i)
        {
            SQLite_Funciton_Query sfq = new SQLite_Funciton_Query();
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            if (i == 1)
            {
                sfq.SQLite_Query_ListBox_Inverse(listBox2, Righ_Item_ListBox_SQL, Main_GridView.Rows[this.Row_Index].Cells[1].Value.ToString());
                sfq.SQLite_Query_ListBox(listBox2, Left_Item_ListBox_SQL, Main_GridView.Rows[this.Row_Index].Cells[1].Value.ToString());
            }
            if (i == 2)
            {
                sfq.SQLite_Query_ListBox(listBox1, Left_Item_ListBox_SQL, Main_GridView.Rows[this.Row_Index].Cells[1].Value.ToString());
                sfq.SQLite_Query_ListBox_Inverse(listBox1, Righ_Item_ListBox_SQL, Main_GridView.Rows[this.Row_Index].Cells[1].Value.ToString());
            }
        }
        #endregion
        #region Edit Item DataGridview after click
        private void Menu_Item_0_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            ToolStripItem clickedItem = sender as ToolStripItem;
            if (GridView_Index == 1)
            {
                lbltp2Tittle.Text = "User";
                chktp2.Text = "Edit Link-User";
                ListBox_Item();
                txttp2Name.Text = Main_GridView.Rows[Row_Index].Cells[3].Value.ToString();
            }
            if (GridView_Index == 3)
            {
                lbltp2Tittle.Text = "Comment";
                chktp2.Text = "Edit Link-Comment";
                ListBox_Item();
                txttp2Name.Text = Main_GridView.Rows[Row_Index].Cells[3].Value.ToString();
            }
            if (GridView_Index == 4)
            {
                chktp2.Text = "Edit Link-Group";
                ListBox_Item();
                txttp2Name.Text = Main_GridView.Rows[Row_Index].Cells[3].Value.ToString();
            }
            check_item_listbox();
            tabControl1.SelectTab("tabPage2");
            if (GridView_Index == 2)
            {
                tabControl1.SelectTab("tabPage3");
            }
        }
        //Contextmenutrip Event Delete item click
        #endregion
        #region Delete Item DataGridview after click
        private void item1_Click(object sender, EventArgs e)
        {
            ToolStripItem clickedItem = sender as ToolStripItem;
            delete_item();
        }
        #endregion
        #region Delete Item Function
        private void delete_item()
        {
            Function_Settings fnc = new Function_Settings();
            SQLite_Funciton_Query sfq = new SQLite_Funciton_Query();
            sfq.SQLite_Query_Delete(this.delete, Main_GridView.Rows[this.Row_Index].Cells[1].Value.ToString());
            Log_DataBase("Delete data","Delete"+ Main_GridView.Rows[this.Row_Index].Cells[3].Value.ToString());
            Main_GridView.Rows.RemoveAt(this.Row_Index);
            fnc.reload(Main_GridView, select, View_Column_Type);
        }
        #endregion
        #region Check amount item in Listbox 
        private void check_item_listbox()
        {
            int total = 0;
            int total2 = 0;
            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                total++;
            }
            for (int i = 0; i < listBox2.Items.Count; i++)
            {
                total2++;
            }
            if (total == 0)
            {
                btnNext.Enabled = false;
                btnNext.BackColor = Color.White;
                btnNextAll.Enabled = false;
                btnNextAll.BackColor = Color.White;
            }
            if (total2 == 0)
            {
                btnBack.Enabled = false;
                btnBack.BackColor = Color.White;
                btnBackAll.Enabled = false;
                btnBackAll.BackColor = Color.White;
            }
            if (total != 0)
            {
                btnNext.Enabled = true;
                //btnNext.BackColor = Color.White;
                btnNextAll.Enabled = true;
                //btnNextAll.BackColor = Color.White;
            }
            if (total2 != 0)
            {
                btnBack.Enabled = true;
                btnBack.BackColor = Color.White;
                btnBackAll.Enabled = true;
                btnBackAll.BackColor = Color.White;
            }
        }
        #endregion
        #region Button Back Event
        private void btnBack_Click(object sender, EventArgs e)
        {
            String SelectedItem = listBox2.GetItemText(listBox2.SelectedItem);
            if (SelectedItem != "")
            {
                listBox1.Items.Add(SelectedItem);
                listBox2.Items.Remove(SelectedItem);
            }
            check_item_listbox();
        }
        #endregion
        #region Button Next Event
        private void btnNext_Click(object sender, EventArgs e)
        {
            String SelectedItem = listBox1.GetItemText(listBox1.SelectedItem);
            if (SelectedItem != "")
            {
                listBox2.Items.Add(SelectedItem);
                listBox1.Items.Remove(SelectedItem);
            }
            check_item_listbox();
        }
        #endregion
        #region Button Next All Event
        private void btnNextAll_Click(object sender, EventArgs e)
        {
            ListBox_Item_Inverse(1);
            check_item_listbox();
        }
        #endregion
        #region Button Back All Event
        private void btnBackAll_Click(object sender, EventArgs e)
        {
            ListBox_Item_Inverse(2);
            check_item_listbox();
        }
        #endregion
        #region Button Apply Event
        private void btnApply_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Click");
            Log_DataBase("Apply Choise Link");
            //using (SQLiteConnection sqlConn = new SQLiteConnection(connect))
            //{
            //    try
            //    {
            //        string sql = this.active + bunifuCustomDataGrid.Rows[this.Row_Index].Cells[1].Value.ToString() + "'";
            //        using (var cmd = new SQLiteCommand(sql, sqlConn))
            //        {
            //            sqlConn.Open();
            //            cmd.ExecuteNonQuery();

            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show(ex.Message);
            //    }
            //}
        }
        #endregion
        #region Function UTF-8 String
        private string encode(string read)
        {
            byte[] bytes = Encoding.Default.GetBytes(read);
            read = Encoding.UTF8.GetString(bytes);
            return read;
        }
        #endregion
        #region Fucntion Language-Image-FontStyle Default
        private void Lang_Image_Font()
        {
            PrivateFontCollection pfc = new PrivateFontCollection();
            pfc.AddFontFile(@"font/BungeeInline-Regular.ttf");
            PrivateFontCollection pfc2 = new PrivateFontCollection();
            pfc2.AddFontFile(@"font/RobotoCondensed-Regular.ttf");
            var lang = new IniFile("Language.ini");
            //Language
            btnApply.ButtonText = "         " + encode(lang.Read("btnApply", "Language"));
            btnDashBoard.Text = "         " + encode(lang.Read("btnDashBoard", "Language"));
            btnUser.Text = "         " + encode(lang.Read("btnFuncA", "Language"));
            btnLink.Text = "         " + encode(lang.Read("btnFuncB", "Language"));
            btnGroup.Text = "         " + encode(lang.Read("btnFuncC", "Language"));
            btnComment.Text = "         " + encode(lang.Read("btnFuncD", "Language"));
            txtSearch.Text = encode(lang.Read("txtSearch", "Language"));
            lblTitle.Text = encode(lang.Read("Title", "Language"));
            //Image_Button
            btnStart.Image = (Bitmap)Image.FromFile(@"image/image-start.png", true);
            btnStop.Image = (Bitmap)Image.FromFile(@"image/image-stop.png", true);
            btnReload.Image = (Bitmap)Image.FromFile(@"image/image-reload.png", true);
            btnDelete.Image = (Bitmap)Image.FromFile(@"image/image-delete.png", true);
            btnNew.Image = (Bitmap)Image.FromFile(@"image/image-new.png", true);
            btnSetting.Image = (Bitmap)Image.FromFile(@"image/image-setting.png", true);
            //Font_Family and ImageZoom Button
            lblTitle.Font = new Font(pfc.Families[0], 10);
            btnDashBoard.TextFont = new Font(pfc2.Families[0], 10);
            btnDashBoard.IconZoom = 50;
            btnUser.TextFont = new Font(pfc2.Families[0], 10);
            btnUser.IconZoom = 50;
            btnLink.TextFont = new Font(pfc2.Families[0], 10);
            btnLink.IconZoom = 50;
            btnGroup.TextFont = new Font(pfc2.Families[0], 10);
            btnGroup.IconZoom = 50;
            btnComment.TextFont = new Font(pfc2.Families[0], 10);
            btnComment.IconZoom = 50;
            txtSearch.Font = new Font(pfc2.Families[0], 10);
            Main_GridView.ColumnHeadersDefaultCellStyle.Font = new Font("Roboto", 10, FontStyle.Bold);
            Main_GridView.DefaultCellStyle.Font = new Font("RobotoCondensed", 10, FontStyle.Regular);
        }
        #endregion
        private void bunifuTextbox1_Leave(object sender, EventArgs e)
        {
            if (bunifuTextbox1.text == "")
            {
                bunifuTextbox1.text = "Enter some text here";
                bunifuTextbox1.ForeColor = Color.Gray;
            }
        }

        private void bunifuTextbox1_Enter(object sender, EventArgs e)
        {
            if (bunifuTextbox1.text == "Enter some text here")
            {
                bunifuTextbox1.text = "";
            }
        }
        #region Button New
        private void btnNew_Click(object sender, EventArgs e)
        {
            if (PanelNew.Width == 1)
            {
                Panelmenu.Width = 50;
                PanelNew.Visible = false;
                PanelNew.Width = 122;
                AddNewAnimation.ShowSync(PanelNew);
            }
            else
            {
                Panelmenu.Width = 50;
                PanelNew.Visible = false;
                PanelNew.Width = 1;
                AddNewAnimation.ShowSync(PanelNew);
            }
        }
        #endregion
        #region Button Menu 
        private void btnMenu_Click_1(object sender, EventArgs e)
        {
            if (Panelmenu.Width == 50)
            {
                PanelNew.Width = 1;
                Panelmenu.Visible = false;
                lblTitle.Visible = true;
                Panelmenu.Width = 218;
               // AddNewAnimation.ShowSync(Panelmenu);
                MenuAnimation.ShowSync(label);
            }
            else
            {
                PanelNew.Width = 1;
                lblTitle.Visible = false;
                MenuAnimation.HideSync(label);
                Panelmenu.Visible = false;
                Panelmenu.Width = 50;
               // AddNewAnimation.ShowSync(Panelmenu);
            }
        }
        #endregion        
        #region Style Form like css
        private void Cells(DataGridViewCell Location, string Type_Style = null) // Style Cells DataGridView
        {
            Style_Template_Form stf = new Style_Template_Form();
            stf.Style(Type_Style);
            style(stf);
            Location.Style.ForeColor = Color.FromName(this.Font_Fore_Color);
            Location.Style.BackColor = Color.FromName(this.Cell_Back_Color);
            Location.Style.Font = new Font(this.Font_Name, this.Font_Size);
        }
        private void Rows(DataGridViewRow Location, string Type_Style = null) // Style Rows DataGridView
        {
            Style_Template_Form stf = new Style_Template_Form();
            stf.Style(Type_Style);
            style(stf);
            Location.DefaultCellStyle.ForeColor = Color.FromName(this.Font_Fore_Color);
            Location.DefaultCellStyle.BackColor = Color.FromName(this.Cell_Back_Color);
            Location.DefaultCellStyle.Font = new Font(this.Font_Name, this.Font_Size);
        }
        private void style(Style_Template_Form stf) // Origin Style 
        {
            this.Font_Fore_Color = stf.Fore_Color;
            this.Cell_Back_Color = stf.Back_Color;
            this.Font_Name = stf.Font_Name;
            this.Font_Size = stf.Font_Size;
        }
        #endregion

        private void bunifuCustomDataGrid1_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            Main_GridView.Columns[e.Column.Index].SortMode = DataGridViewColumnSortMode.NotSortable;
        }
        private void Create_Sql_menu_db()
        {

        }
        #region Write Log DataBase
        private void Log_DataBase(string fucntion,string Note=null)
        {
            if (GridView_Index == 1)
            {
                this.Log_Text_Concatenate = " in User";
            }
            if (GridView_Index == 2)
            {
                this.Log_Text_Concatenate = " in Link";
            }
            if (GridView_Index == 3)
            {
                this.Log_Text_Concatenate = " in Comment";
            }
            if (GridView_Index == 4)
            {
                this.Log_Text_Concatenate = " in Group";
            }
            string select = "INSERT INTO Log (Name, Time, Action, Note) VALUES('" + User_ACL.ToString() + "', '" + DateTime.Now.ToString() + "', '" + fucntion.ToString() + "','"+ Note.ToString() + Log_Text_Concatenate + "');";
            SQLite_Funciton_Query sfq = new SQLite_Funciton_Query();
            sfq.SQLite_Query_Log_Database(Main_GridView, select);
        }
        #endregion
        private void btnFilter_Click(object sender, EventArgs e)
        {
            this.search = "SELECT Name, Time, Action,Note FROM Log WHERE Action LIKE '%" + cb1.Text + "%' and Time BETWEEN '" +
                    "" + dateTimePicker1.Value.ToString() + "' AND '" + dateTimePicker2.Value.ToString() + "'AND Name LIKE '%" + cb2.Text + "%'";
            Function_Settings fnc = new Function_Settings();
            fnc.search_value(this.search);
            fnc.search_item(Log_GridView, this.search);
            
        }
        #region SQL Select Column
        private string SQL_Select(string Table_Name, string[] Column_Name, string[] Table_Name_Count, string[] ID_Table_Name_Count, string []As_Name,int group_by)
        {
            string select = string.Empty;
            string select_querry = "SELECT ";
            string select_count = "";
            string selects = "";
            string table_name_count = "";
            for (int i = 0; i < Column_Name.Length; i++)
            {
                if (Column_Name[i].ToString() == "Status")
                {
                    select_querry = select_querry + "Status" + "." + Column_Name[i].ToString() + ", ";
                }
                else
                    select_querry = select_querry + Table_Name + "." + Column_Name[i].ToString() + ", ";
            }
            for (int j = 0; j < Table_Name_Count.Length; j++)
            {
                if(j < Table_Name_Count.Length - 1)
                {
                    table_name_count = table_name_count  + Table_Name_Count[j] + ", ";
                    select_count = select_count + "(SELECT COUNT(*) FROM " + Table_Name_Count[j] + " WHERE " + Table_Name_Count[j] +
                        "." + ID_Table_Name_Count[j] + "=" + Table_Name + "." + Column_Name[0].ToString() + ") as '" + As_Name[j] + "',";
                }
                else
                {
                    table_name_count = table_name_count + Table_Name_Count[j];
                    select_count = select_count + "(SELECT COUNT(*) FROM " + Table_Name_Count[j] + " WHERE " + Table_Name_Count[j] +
                        "." + ID_Table_Name_Count[j] + "=" + Table_Name + "." + Column_Name[0].ToString() + ") as '" + As_Name[j] + "'";
                }                  
            }
            if (group_by == 0)
            {
                 selects = " FROM " + Table_Name + " LEFT JOIN Status, " + table_name_count + " ON " + Table_Name + ".Status=Status.ID" + Column_Name[1].ToString() +
                                " GROUP BY " + Table_Name + "." + Column_Name[0].ToString();
            }
            else
            {
                selects = " FROM " + Table_Name + " LEFT JOIN Status " + " ON " + Table_Name + ".Status=Status.ID" + Column_Name[1].ToString();       
            }       
            select = select_querry + select_count + selects; 
            this.active = "update "+ Table_Name + " set Status = '2'  where " + Column_Name[0].ToString() +" ='";
            this.stop = "update "+ Table_Name + " set Status = '1'  where " + Column_Name[0].ToString() + " ='";
            this.delete = "DELETE FROM "+ Table_Name + " WHERE " + Column_Name[0].ToString() + "='";
            return select;
        }
        #endregion
        #region SQL ListBox Function Select Item
        private string SQL_ListBox(string Table_Name, string Column_Name, string ID_Table_Name, string Table_Name_From, string ID_Table_Name_From, int NOT_IN)
        {
            string querry = string.Empty;
            if (NOT_IN == 0)
            {
                querry = "SELECT " + Table_Name + "." + Column_Name + " FROM " + Table_Name_From + " LEFT JOIN " + Table_Name +
                    " ON " + Table_Name_From + "." + ID_Table_Name + "=" + Table_Name + "." +
                    ID_Table_Name + " WHERE " + Table_Name_From + "." + ID_Table_Name_From + "='";
            }
            else
            {
                querry = "SELECT " + Table_Name + "." + Column_Name + " FROM " + Table_Name + " WHERE " + Table_Name + "." + ID_Table_Name + " NOT IN (SELECT " + Table_Name + "." + ID_Table_Name +
                     " FROM " + Table_Name_From + " LEFT JOIN " + Table_Name + " ON " + Table_Name_From + "." + ID_Table_Name + "=" + Table_Name + "." +
                     ID_Table_Name + " WHERE " + Table_Name_From + "." + ID_Table_Name_From + "='";
            }
            return querry;
        }
        #endregion
        #region Button Fuction DataGridView
        private void button_fuction(int view_column_type, string tabpage,DataGridView name,int GridViewIndex)
        {
            this.View_Column_Type = view_column_type;
            Default_Template_Gridview();
            tabControl1.SelectTab(tabpage);
            Function_Settings dta = new Function_Settings();
            if (Default_GridView == 0)
            {
                MessageBox.Show(select);
                dta.gridview(name, this.select, this.View_Column_Type);
                name.ClearSelection();
                this.Default_GridView = 1;
            }
            else
            {
                MessageBox.Show(select);
                dta.reload(name, this.select, this.View_Column_Type);
                name.ClearSelection();
            }
            this.GridView_Index = GridViewIndex;
            color();
        }
        #endregion
    }
}
 