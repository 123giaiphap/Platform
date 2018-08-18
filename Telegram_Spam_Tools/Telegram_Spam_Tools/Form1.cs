using System;
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
    public partial class Form1 : Form
    {
        private ContextMenuStrip m = new ContextMenuStrip();
        private string connect = string.Empty;
        private int rowIndex;
        private int columnIndex;
        private int i = 0;
        private int grid = 0;
        private int grid1;
        private int species;
        internal int gridview_tab;
        private string save_name = string.Empty;
        private string save_link = string.Empty;
        private string listbox1_item_sql = string.Empty;
        private string listbox2_item_sql = string.Empty;
        private string select = string.Empty;
        private string active = string.Empty;
        private string stop = string.Empty;
        private string delete = string.Empty;
        private string search = string.Empty;
        private DataGridView bunifuCustomDataGrid = new DataGridView();
        System.Media.SoundPlayer player = new System.Media.SoundPlayer("click.wav");
        public Form1()
        {
            connect = @"Data Source = telegram.db;Verison=3";
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            bunifuTextbox1.text = "Enter some text here";
            bunifuTextbox2._TextBox.PasswordChar = '*';
            Total_Status();
            //Lang_Image_Font();
        }
        private void hahaha()
        {
            try
            {
                this.bunifuCustomDataGrid = bunifuCustomDataGrid1;
                bunifuCustomDataGrid.DataSource = null;
                bunifuCustomDataGrid.Refresh();
                bunifuCustomDataGrid.RowHeadersVisible = false;
                bunifuCustomDataGrid.AllowUserToAddRows = false;
                bunifuCustomDataGrid.ColumnHeadersHeight = 45;
                bunifuCustomDataGrid1.HeaderBgColor = Color.SeaGreen;
                bunifuCustomDataGrid1.HeaderForeColor = Color.White;
                bunifuCustomDataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnUser_Click(object sender, EventArgs e)
        {
            this.species = 0;
            lblTitle.Text = "TELEGRAM - USER";
            hahaha();
            tabControl1.SelectTab("tabPage1");
            string name = "tabPage2";
            Function_Settings dta = new Function_Settings();
            dta.select_tab(tabControl1, name);
            this.listbox1_item_sql = "select Link.LinkName from LinkUser left join Link on LinkUser.IDLink=Link.IDLink where LinkUser.IDUser='";
            this.listbox2_item_sql = "select Link.LinkName from Link where Link.IDLink NOT IN (select Link.IDLink from LinkUser left join Link on LinkUser.IDLink=Link.IDLink where LinkUser.IDUser='";
            this.active = "update User set Status = '2'  where IDUser ='";
            this.stop = "update User set Status = '1'  where IDUser ='";
            this.delete = "DELETE FROM User WHERE IDUser='";
            dta.status(this.active, this.stop);
            dta.delete(this.delete);
            this.select = "select User.IDUser, Status.Status, User.UserName,  User.Password, User.UserAgent, User.ProxyIP, User.ProxyPort, User.Description, User.DeleteDay, User.CreateDay,(select count(*) from LinkUser where LinkUser.IDUser=user.IDUser) as 'Link User' from  User left join Status, LinkUser on User.Status=Status.IDStatus group by User.IDUser";
            if (grid1 == 0)
            {
                dta.gridview(bunifuCustomDataGrid1, this.select, this.species);
                bunifuCustomDataGrid1.ClearSelection();
                this.grid1 = 1;
            }
            else
            {
                dta.reload(bunifuCustomDataGrid1, this.select, this.species);
                bunifuCustomDataGrid1.ClearSelection();
            }
            this.gridview_tab = 1;
            color();
        }

        private void btnLink_Click(object sender, EventArgs e)
        {
            this.species = 0;
            lblTitle.Text = "TELEGRAM - LINK";
            hahaha();
            tabControl1.SelectTab("tabPage1");
            string name = "tabPage2";
            Function_Settings dta = new Function_Settings();
            dta.select_tab(tabControl1, name);
            this.active = "update Link set Status = '2'  where IDLink ='";
            this.stop = "update Link set Status = '1'  where IDLink ='";
            this.delete = "DELETE FROM Link WHERE IDLink='";
            dta.status(this.active, this.stop);
            dta.delete(this.delete);
            this.select = "select Link.IDLink,Status.Status,Link.LinkName, Link.Description,Link.CreatedDate,(select count(*) from LinkGroup where LinkGroup.IDLink=link.IDLink) as 'Group Used' ,(select count(*) from LinkComment where LinkComment.IDLink=link.IDLink) as 'Comment Used',(select count(*) from LinkUser where LinkUser.IDLink=Link.IDLink) as 'User Used' from Link left join Status on Link.Status=Status.IDStatus";
            if (grid1 == 0)
            {
                dta.gridview(bunifuCustomDataGrid1, this.select, this.species);
                bunifuCustomDataGrid1.ClearSelection();
                this.grid1 = 1;
            }
            else
            {
                dta.reload(bunifuCustomDataGrid1, this.select, this.species);
                bunifuCustomDataGrid1.ClearSelection();
            }
            this.gridview_tab = 2;
            color();
        }

        private void btnGroup_Click(object sender, EventArgs e)
        {
            this.species = 1;
            lblTitle.Text = "TELEGRAM - GROUP";
            hahaha();
            tabControl1.SelectTab("tabPage1");
            string name = "tabPage2";
            Function_Settings dta = new Function_Settings();
            dta.select_tab(tabControl1, name);
            this.listbox1_item_sql = "select Link.LinkName from LinkGroup left join Link on LinkGroup.IDLink=Link.IDLink where LinkGroup.IDGroup='";
            this.listbox2_item_sql = "select Link.LinkName from Link where Link.IDLink NOT IN (select Link.IDLink from LinkGroup left join Link on LinkGroup.IDLink=Link.IDLink where LinkGroup.IDGroup='";
            this.active = "update ListGroup set Status = '2'  where IDGroup ='";
            this.stop = "update ListGroup set Status = '1'  where IDGroup ='";
            this.delete = "DELETE FROM ListGroup WHERE IDGroup='";
            dta.status(this.active, this.stop);
            dta.delete(this.delete);
            this.select = "select ListGroup.IDGroup, Status.Status, ListGroup.GroupName, ListGroup.Password, ListGroup.CreatedDate,(select count(*) from LinkGroup where LinkGroup.IDGroup=ListGroup.IDGroup) as 'Link Used' from ListGroup , LinkGroup inner JOIN Status on ListGroup.Status=Status.IDStatus group by ListGroup.IDGroup";
            if (grid1 == 0)
            {
                dta.gridview(bunifuCustomDataGrid1, this.select, this.species);
                bunifuCustomDataGrid1.ClearSelection();
                this.grid1 = 1;
            }
            else
            {
                dta.reload(bunifuCustomDataGrid1, this.select, this.species);
                bunifuCustomDataGrid1.ClearSelection();
            }
            this.gridview_tab = 4;
            color();
        }
        private void btnDashboard_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab("tabPage2");
            //player.Play();
        }
        private void btnCmt_Click(object sender, EventArgs e)
        {
            this.species = 1;
            lblTitle.Text = "TELEGRAM - COMMENT";
            hahaha();
            tabControl1.SelectTab("tabPage1");
            string name = "tabPage2";
            Function_Settings dta = new Function_Settings();
            dta.select_tab(tabControl1, name);
            this.listbox1_item_sql = "select Link.LinkName from LinkComment left join Link on LinkComment.IDLink=Link.IDLink where LinkComment.IDComment='";
            this.listbox2_item_sql = "select Link.LinkName from Link where Link.IDLink NOT IN (select Link.IDLink from LinkComment left join Link on LinkComment.IDLink=Link.IDLink where LinkComment.IDComment='";
            this.active = "update Comment set Status = '2'  where IDCmt ='";
            this.stop = "update Comment set Status = '1'  where IDCmt ='";
            this.delete = "DELETE FROM Comment WHERE IDCmt='";
            dta.status(this.active, this.stop);
            dta.delete(this.delete);
            this.select = "select Comment.IDCmt, Status.Status, Comment.Comment,  Comment.Description , Comment.CreatedDate,(select count(*) from LinkComment where LinkComment.IDComment=Comment.IDCmt) as 'Link Used' from  Comment left join Status, LinkComment on Comment.Status=Status.IDStatus group by Comment.IDCmt";
            if (grid1 == 0)
            {
                dta.gridview(bunifuCustomDataGrid1, this.select, this.species);
                bunifuCustomDataGrid1.ClearSelection();
                this.grid1 = 1;
            }
            else
            {
                dta.reload(bunifuCustomDataGrid1, this.select, this.species);
                bunifuCustomDataGrid1.ClearSelection();
            }
            this.gridview_tab = 3;
            color();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Environment.Exit(-1);
        }
        internal void Total_Status()
        {
            //string TotalUser = "select count(User.IDUser) from User";
            //string User_active = "select count(user.Status) from User where user.Status='1'";
            //string User_stop = "select count(user.Status) from User where user.Status='2'";
            //count(TotalUser, User_active, User_stop);
            //this.lblTotalUser.Text = count_total.ToString();
            //this.lblStatusUser.Text = status_active.ToString() + "/" + status_stop.ToString();
            //string TotalLink = "select count(Link.IDLink) from Link";
            //string Link_active = "select count(Link.Status) from Link where Link.Status='1'";
            //string Link_stop = "select count(Link.Status) from Link where Link.Status='2'";
            //count(TotalLink, Link_active, Link_stop);
            //this.lblTotalLink.Text = count_total.ToString();
            //this.lblStatusLink.Text = status_active.ToString() + "/" + status_stop.ToString();
            //string TotalGroup = "select count(ListGroup.IDGroup) from ListGroup";
            //string Group_active = "select count(ListGroup.Status) from ListGroup where ListGroup.Status='1'";
            //string Group_stop = "select count(ListGroup.Status) from ListGroup where ListGroup.Status='2'";
            //count(TotalGroup, Group_active, Group_stop);
            //this.lblTotalGroup.Text = count_total.ToString();
            //this.lblStatusGroup.Text = status_active.ToString() + "/" + status_stop.ToString();
            //string TotalCmt = "select count(Comment.IDCmt) from Comment";
            //string Cmt_active = "select count(Comment.Status) from Comment where Comment.Status='1'";
            //string Cmt_stop = "select count(Comment.Status) from Comment where Comment.Status='2'";
            //count(TotalCmt, Cmt_active, Cmt_stop);
            //this.lblTotalCmt.Text = count_total.ToString();
            //this.lblStatusCmt.Text = status_active.ToString() + "/" + status_stop.ToString();
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            Function_Settings dta = new Function_Settings();
            dta.status(this.active, this.stop);
            dta.delete(this.delete);
            Total_Status();
            if (this.grid1 == 1)
            {
                dta.reload(bunifuCustomDataGrid1, this.select, this.species);
                bunifuCustomDataGrid1.ClearSelection();
            }
            else
            {
                MessageBox.Show("dashboard");
            }
            color();
        }
        private void color()
        {
            m.Items.Clear();
            ToolStripItem item0 = m.Items.Add("Edit ");
            item0.Click += new EventHandler(item0_Click);
            ToolStripItem item1 = m.Items.Add("Delete ");
            item1.Click += new EventHandler(item1_Click);
            if (species == 0)
            {
                for (int i = 0; i < bunifuCustomDataGrid1.RowCount; i++)
                {
                    string val = bunifuCustomDataGrid1.Rows[i].Cells[2].Value.ToString();
                    if (val == "Active")
                    {
                        bunifuCustomDataGrid.Rows[i].Cells[3].Style.ForeColor = Color.Brown;
                    }
                    if (val == "Stop")
                    {
                        bunifuCustomDataGrid.Rows[i].Cells[3].Style.ForeColor = Color.Red;
                    }
                }
            }
            if (species == 1)
            {
                int max = 0;
                for (int j = 0; j < bunifuCustomDataGrid1.ColumnCount; j++)
                {
                    max++;
                }
                //MessageBox.Show(max.ToString());
                if (max > 0)
                {
                    DataGridViewImageColumn imgCol = new DataGridViewImageColumn();
                    imgCol.Name = "abc";
                    imgCol.HeaderText = "Image";
                    bunifuCustomDataGrid1.Columns.Add(imgCol);
                    TextAndImageColumn txtcol = new TextAndImageColumn();
                    txtcol.Name = "anhxa";
                    txtcol.HeaderText = "";
                    bunifuCustomDataGrid1.Columns.Add(txtcol);
                    this.bunifuCustomDataGrid1.Columns[max].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                    this.bunifuCustomDataGrid1.Columns[max + 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                    TextAndImageColumn txtcol1 = new TextAndImageColumn();
                    txtcol1.Name = "anhxa1";
                    txtcol1.HeaderText = "";
                    bunifuCustomDataGrid1.Columns.Add(txtcol1);
                    this.bunifuCustomDataGrid1.Columns[max + 2].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                    this.grid = max + 2;
                    TextAndImageColumn txtcol2 = new TextAndImageColumn();
                    txtcol2.Name = "anhxa2";
                    txtcol2.HeaderText = "";
                    bunifuCustomDataGrid1.Columns.Add(txtcol2);
                    this.bunifuCustomDataGrid1.Columns[max + 3].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                    try
                    {
                        for (int i = 0; i < bunifuCustomDataGrid1.RowCount; i++)
                        {
                            this.bunifuCustomDataGrid1.Rows[i].Cells[1] = new TextAndImageCell();
                            ((TextAndImageCell)bunifuCustomDataGrid1.Rows[i].Cells[1]).Image = (Image)imageList1.Images[1];
                            string val = bunifuCustomDataGrid1.Rows[i].Cells[2].Value.ToString();
                            bunifuCustomDataGrid1.Rows[i].Cells[max + 1].Value = "12";
                            bunifuCustomDataGrid1.Rows[i].Cells[max + 2].Value = "12";
                            bunifuCustomDataGrid1.Rows[i].Cells[max + 3].Value = "12";
                            //((TextAndImageCell)bunifuCustomDataGrid1.Rows[i].Cells[max+1]).Image = (Image)imageList1.Images[1];
                            if (val == "Active")
                            {
                                bunifuCustomDataGrid1.Rows[i].Cells[max].Value = Image.FromFile(@"image_on.png");
                                //this.bunifuCustomDataGrid1.Columns[max].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                            }
                            if (val == "Stop")
                            {
                                bunifuCustomDataGrid1.Rows[i].Cells[max].Value = Image.FromFile(@"image_off.png"); ;
                            }
                            //bunifuCustomDataGrid1.Rows[i].Cells[max + 1].Value = bunifuCustomDataGrid1.Rows[i].Cells[1].Value;                     
                            ((TextAndImageCell)bunifuCustomDataGrid1.Rows[i].Cells[max + 1]).Image = (Image)imageList1.Images[1];
                            ((TextAndImageCell)bunifuCustomDataGrid1.Rows[i].Cells[max + 2]).Image = (Image)imageList1.Images[2];
                            ((TextAndImageCell)bunifuCustomDataGrid1.Rows[i].Cells[max + 3]).Image = (Image)imageList1.Images[3];
                        }
                    }
                    catch (Exception)
                    {

                    }
                }
            }
        }
        private void bunifuCustomDataGrid1_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                bunifuCustomDataGrid.ClearSelection();
                var hti = bunifuCustomDataGrid.HitTest(e.X, e.Y);
                this.bunifuCustomDataGrid.Rows[hti.RowIndex].Selected = true;
                this.rowIndex = hti.RowIndex;
                this.columnIndex = hti.ColumnIndex;
            }
            catch (Exception)
            {

            }
            if (e.Button == MouseButtons.Right)
            {
                try
                {
                    bunifuCustomDataGrid.ClearSelection();
                    var hti = bunifuCustomDataGrid.HitTest(e.X, e.Y);
                    this.bunifuCustomDataGrid.Rows[hti.RowIndex].Selected = true;
                    this.rowIndex = hti.RowIndex;
                    this.columnIndex = hti.ColumnIndex;
                    this.m.Show(this.bunifuCustomDataGrid, e.Location);
                    m.Show(Cursor.Position);
                }
                catch (Exception)
                {

                }
            }

        }

        private void bunifuCustomDataGrid1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            for (int i = 0; i < bunifuCustomDataGrid1.RowCount; i++)
            {
                string val = bunifuCustomDataGrid1.Rows[i].Cells[2].Value.ToString();
                if (bunifuCustomDataGrid1.Columns[columnIndex].Name == "abc")
                {

                    Bitmap a = (Bitmap)Image.FromFile(@"image_on.png", true);
                    Bitmap b = (Bitmap)Image.FromFile(@"image_off.png", true);
                    if (val == "Active")
                    {

                        this.rowIndex = e.RowIndex;
                        this.columnIndex = e.ColumnIndex;
                        bunifuCustomDataGrid1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = b;
                        status_actived(bunifuCustomDataGrid1);
                        bunifuCustomDataGrid1.Rows[i].Cells[2].Value = "Stop";
                    }
                    if (val == "Stop")
                    {
                        this.rowIndex = e.RowIndex;
                        this.columnIndex = e.ColumnIndex;
                        bunifuCustomDataGrid1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = a;
                        status_stoped(bunifuCustomDataGrid1);
                        bunifuCustomDataGrid1.Rows[i].Cells[2].Value = "Active";
                    }

                }
            }
        }
        internal void status_actived(DataGridView bunifuCustomDataGrid1)
        {
            using (SQLiteConnection sqlConn = new SQLiteConnection(connect))
            {
                try
                {
                    string sql = this.active + bunifuCustomDataGrid.Rows[this.rowIndex].Cells[1].Value.ToString() + "'";
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
        internal void status_stoped(DataGridView bunifuCustomDataGrid1)
        {
            using (SQLiteConnection sqlConn = new SQLiteConnection(connect))
            {
                try
                {

                    string sql = this.stop + bunifuCustomDataGrid.Rows[this.rowIndex].Cells[1].Value.ToString() + "'";
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
        public void ImageRowDisplay()
        {
            ((TextAndImageCell)bunifuCustomDataGrid1.Rows[0].Cells[0]).Image = (Image)imageList1.Images[1];
        }
        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                if (this.gridview_tab == 1)
                {
                    this.search = "select * from User where UserName Like '%" + txtSearch.Text + "%'";
                }
                if (this.gridview_tab == 2)
                {
                    this.search = "select * from Link where LinkName Like '%" + txtSearch.Text + "%'";
                }
                if (this.gridview_tab == 3)
                {
                    this.search = "select * from Comment where Comment Like '%" + txtSearch.Text + "%'";
                }
                if (this.gridview_tab == 4)
                {
                    this.search = "select * from ListGroup where GroupName Like '%" + txtSearch.Text + "%'";
                }
                Function_Settings fnc = new Function_Settings();
                fnc.search_value(this.search);
                fnc.search_item(bunifuCustomDataGrid1, this.search);
            }
        }

        private void bunifuCustomDataGrid1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex == grid - 1 && e.RowIndex > -1 && this.species == 1)
            {
                e.AdvancedBorderStyle.Left = DataGridViewAdvancedCellBorderStyle.None;
                e.AdvancedBorderStyle.Right = DataGridViewAdvancedCellBorderStyle.None;
            }
            if (e.ColumnIndex == grid && e.RowIndex > -1 && this.species == 1)
            {
                e.AdvancedBorderStyle.Left = DataGridViewAdvancedCellBorderStyle.None;
                e.AdvancedBorderStyle.Right = DataGridViewAdvancedCellBorderStyle.None;
            }
        }

        private void bunifuCustomDataGrid1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (bunifuCustomDataGrid.Columns[e.ColumnIndex].Name == "Password" && e.Value != null)
            {
                bunifuCustomDataGrid.Rows[e.RowIndex].Tag = e.Value;
                e.Value = new String('*', e.Value.ToString().Length);
            }
            if (bunifuCustomDataGrid.Columns[e.ColumnIndex].Name == "Status")
            {
                bunifuCustomDataGrid.Columns[e.ColumnIndex].Visible = false;
            }
        }
        private void listbox_item()
        {
            using (SQLiteConnection con = new SQLiteConnection(connect))
            {
                //Add a CheckBox Column to the DataGridView Header Cell.
                con.Open();
                string stm = listbox1_item_sql + bunifuCustomDataGrid.Rows[this.rowIndex].Cells[1].Value.ToString() + "'";
                try
                {
                    using (SQLiteCommand cmd = new SQLiteCommand(stm, con))
                    {
                        using (SQLiteDataReader rdr = cmd.ExecuteReader())
                        {
                            while (rdr.Read())
                            {
                                listBox1.Items.Add(rdr.GetString(0));
                                i++;
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
            using (SQLiteConnection con = new SQLiteConnection(connect))
            {
                con.Open();
                string stm = listbox2_item_sql + bunifuCustomDataGrid.Rows[this.rowIndex].Cells[1].Value.ToString() + "')";
                try
                {
                    using (SQLiteCommand cmd = new SQLiteCommand(stm, con))
                    {
                        using (SQLiteDataReader rdr = cmd.ExecuteReader())
                        {
                            while (rdr.Read())
                            {
                                listBox2.Items.Add(rdr.GetString(0));
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
        private void list_box_inverse(int i)
        {
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            if (i == 1)
            {
                using (SQLiteConnection con = new SQLiteConnection(connect))
                {
                    con.Open();
                    string stm = listbox2_item_sql + bunifuCustomDataGrid.Rows[this.rowIndex].Cells[1].Value.ToString() + "')";
                    try
                    {
                        using (SQLiteCommand cmd = new SQLiteCommand(stm, con))
                        {
                            using (SQLiteDataReader rdr = cmd.ExecuteReader())
                            {
                                while (rdr.Read())
                                {
                                    listBox2.Items.Add(rdr.GetString(0));
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
                using (SQLiteConnection con = new SQLiteConnection(connect))
                {
                    //Add a CheckBox Column to the DataGridView Header Cell.
                    con.Open();
                    string stm = listbox1_item_sql + bunifuCustomDataGrid.Rows[this.rowIndex].Cells[1].Value.ToString() + "'";
                    try
                    {
                        using (SQLiteCommand cmd = new SQLiteCommand(stm, con))
                        {
                            using (SQLiteDataReader rdr = cmd.ExecuteReader())
                            {
                                while (rdr.Read())
                                {
                                    listBox2.Items.Add(rdr.GetString(0));
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
            if (i == 2)
            {
                using (SQLiteConnection con = new SQLiteConnection(connect))
                {
                    //Add a CheckBox Column to the DataGridView Header Cell.
                    con.Open();
                    string stm = listbox1_item_sql + bunifuCustomDataGrid.Rows[this.rowIndex].Cells[1].Value.ToString() + "'";
                    try
                    {
                        using (SQLiteCommand cmd = new SQLiteCommand(stm, con))
                        {
                            using (SQLiteDataReader rdr = cmd.ExecuteReader())
                            {
                                while (rdr.Read())
                                {
                                    listBox1.Items.Add(rdr.GetString(0));
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
                using (SQLiteConnection con = new SQLiteConnection(connect))
                {
                    con.Open();
                    string stm = listbox2_item_sql + bunifuCustomDataGrid.Rows[this.rowIndex].Cells[1].Value.ToString() + "')";
                    try
                    {
                        using (SQLiteCommand cmd = new SQLiteCommand(stm, con))
                        {
                            using (SQLiteDataReader rdr = cmd.ExecuteReader())
                            {
                                while (rdr.Read())
                                {
                                    listBox1.Items.Add(rdr.GetString(0));
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
        private void item0_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            ToolStripItem clickedItem = sender as ToolStripItem;
            if (gridview_tab == 1)
            {
                lbltp2Tittle.Text = "User";
                chktp2.Text = "Edit Link-User";
                listbox_item();
                txttp2Name.Text = bunifuCustomDataGrid1.Rows[rowIndex].Cells[3].Value.ToString();
            }
            if (gridview_tab == 3)
            {
                lbltp2Tittle.Text = "Comment";
                chktp2.Text = "Edit Link-Comment";
                listbox_item();
                txttp2Name.Text = bunifuCustomDataGrid1.Rows[rowIndex].Cells[3].Value.ToString();
            }
            if (gridview_tab == 4)
            {
                chktp2.Text = "Edit Link-Group";
                listbox_item();
                txttp2Name.Text = bunifuCustomDataGrid1.Rows[rowIndex].Cells[3].Value.ToString();
            }
            check_item_listbox();
            tabControl1.SelectTab("tabPage2");
            if (gridview_tab == 2)
            {
                tabControl1.SelectTab("tabPage3");
            }
        }
        //Contextmenutrip Event Delete item click
        private void item1_Click(object sender, EventArgs e)
        {
            Function_Settings fnc = new Function_Settings();
            ToolStripItem clickedItem = sender as ToolStripItem;
            delete_item(bunifuCustomDataGrid);
            fnc.reload(bunifuCustomDataGrid, select, species);
            // your code here
        }
        private void delete_item(DataGridView bunifuCustomDataGrid)
        {
            Function_Settings fnc = new Function_Settings();
            using (SQLiteConnection con = new SQLiteConnection(connect))
            {
                try
                {
                    string sql = delete + bunifuCustomDataGrid.Rows[this.rowIndex].Cells[1].Value.ToString() + "'";
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
            fnc.reload(bunifuCustomDataGrid, select, species);
        }
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

        private void btnNextAll_Click(object sender, EventArgs e)
        {
            list_box_inverse(1);
            check_item_listbox();
        }

        private void btnBackAll_Click(object sender, EventArgs e)
        {
            list_box_inverse(2);
            check_item_listbox();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Click");
            //using (SQLiteConnection sqlConn = new SQLiteConnection(connect))
            //{
            //    try
            //    {
            //        string sql = this.active + bunifuCustomDataGrid.Rows[this.rowIndex].Cells[1].Value.ToString() + "'";
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
        private string encode(string read)
        {
            byte[] bytes = Encoding.Default.GetBytes(read);
            read = Encoding.UTF8.GetString(bytes);
            return read;
        }
        //private void Lang_Image_Font()
        //{
        //    PrivateFontCollection pfc = new PrivateFontCollection();
        //    pfc.AddFontFile(@"font/Roboto-Black.ttf");
        //    var lang = new IniFile("Language.ini");
        //    //Language
        //    btnApply.ButtonText =  encode(lang.Read("btnApply", "Language"));
        //    btnDashboard.LabelText = encode(lang.Read("btnDashBoard", "Language"));
        //    btnUser.LabelText= encode(lang.Read("btnFuncA", "Language"));
        //    btnLink.LabelText = encode(lang.Read("btnFuncB", "Language"));
        //    btnGroup.LabelText = encode(lang.Read("btnFuncC", "Language"));
        //    btnCmt.LabelText = encode(lang.Read("btnFuncD", "Language"));
        //    txtSearch.Text = encode(lang.Read("txtSearch", "Language"));
        //    lblTitle.Text = encode(lang.Read("Title", "Language"));
        //    //Image_Button
        //    btnStart.Image= (Bitmap)Image.FromFile(@"image/image-start.png", true); 
        //    btnStop.Image= (Bitmap)Image.FromFile(@"image/image-stop.png", true);
        //    btnReload.Image= (Bitmap)Image.FromFile(@"image/image-reload.png", true);
        //    btnDelete.Image= (Bitmap)Image.FromFile(@"image/image-delete.png", true);
        //    btnNew.Image= (Bitmap)Image.FromFile(@"image/image-new.png", true);
        //    btnSetting.Image= (Bitmap)Image.FromFile(@"image/image-setting.png", true);
        //    pictureBox1.Image = (Bitmap)Image.FromFile(@"image/logo.png", true);
        //    //Font_Family
        //    lblTitle.Font = new Font(pfc.Families[0], 10, FontStyle.Regular);
        //    btnDashboard.Font = new Font(pfc.Families[0], 10, FontStyle.Regular);
        //    btnUser.Font = new Font(pfc.Families[0], 10, FontStyle.Regular);
        //    btnLink.Font = new Font(pfc.Families[0], 10, FontStyle.Regular);
        //    btnGroup.Font = new Font(pfc.Families[0], 10, FontStyle.Regular);
        //    btnCmt.Font = new Font(pfc.Families[0], 10, FontStyle.Regular);
        //    txtSearch.Font = new Font(pfc.Families[0], 10, FontStyle.Regular);
        //    bunifuCustomDataGrid1.ColumnHeadersDefaultCellStyle.Font = new Font(pfc.Families[0], 10, FontStyle.Regular);
        //    //this.bunifuCustomDataGrid1.DefaultCellStyle.Font = new Font(pfc.Families[0], 10, FontStyle.Regular);
        //}

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

        private void bunifuCustomLabel4_Click(object sender, EventArgs e)
        {

        }

        private void bunifuTextbox3_OnTextChange(object sender, EventArgs e)
        {

        }

        private void bunifuCustomLabel5_Click(object sender, EventArgs e)
        {

        }

        private void bunifuTextbox4_OnTextChange(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

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

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnMenu_Click_1(object sender, EventArgs e)
        {
            if (Panelmenu.Width == 50)
            {
                PanelNew.Width = 1;
                Panelmenu.Visible = false;
                Panelmenu.Width = 218;
                AddNewAnimation.ShowSync(Panelmenu);
                MenuAnimation.ShowSync(label);
            }
            else
            {
                PanelNew.Width = 1;
                MenuAnimation.HideSync(label);
                Panelmenu.Visible = false;
                Panelmenu.Width = 50;
                AddNewAnimation.ShowSync(Panelmenu);
            }
        }
    }   
}
 