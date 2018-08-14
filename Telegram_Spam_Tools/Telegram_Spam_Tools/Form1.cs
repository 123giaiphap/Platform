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

namespace Telegram_Spam_Tools
{
    public partial class Form1 : Form
    {
        private string connect = string.Empty;
        private int rowIndex;
        private int columnIndex;
        private int count_total;
        private int status_active;
        private int status_stop;
        private int grid = 0;
        private int grid1;
        private int species;
        private int gridview_tab;
        private string select = string.Empty;
        private string active = string.Empty;
        private string stop = string.Empty;
        private string delete = string.Empty;
        private string search = string.Empty;
        private DataGridView bunifuCustomDataGrid = new DataGridView();
        public Form1()
        {
            connect = @"Data Source = telegram.db;Verison=3";
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            Total_Status();
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
                bunifuCustomDataGrid.ColumnHeadersHeight = 30;
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
            this.active = "update User set Status = '2'  where IDUser ='";
            this.stop = "update User set Status = '1'  where IDUser ='";
            this.delete = "DELETE FROM User WHERE IDUser='";
            dta.status(this.active, this.stop);
            dta.delete(this.delete);
            this.select = "select User.IDUser, Status.Status, User.UserName, User.Password,User.Description, User.UserAgent, User.ProxyIP, User.ProxyPort, User.DeleteDay, User.CreateDay from User inner join Status on User.Status=Status.IDStatus;";
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
                    imgCol.HeaderText = "";

                    bunifuCustomDataGrid1.Columns.Add(imgCol);
                    try
                    {
                        for (int i = 0; i < bunifuCustomDataGrid1.RowCount; i++)
                        {
                            this.bunifuCustomDataGrid1.Rows[i].Cells[1] = new TextAndImageCell();
                            ((TextAndImageCell)bunifuCustomDataGrid1.Rows[i].Cells[1]).Image = (Image)imageList1.Images[1];
                            string val = bunifuCustomDataGrid1.Rows[i].Cells[2].Value.ToString();
                            if (val == "Active")
                            {
                                bunifuCustomDataGrid1.Rows[i].Cells[max].Value = Image.FromFile(@"image_on.png");
                                this.bunifuCustomDataGrid1.Columns[max].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                                //this.bunifuCustomDataGrid1.Columns[max - 1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                                //this.bunifuCustomDataGrid1.Columns[max - 1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                            }
                            if (val == "Stop")
                            {
                                bunifuCustomDataGrid1.Rows[i].Cells[max].Value = Image.FromFile(@"image_off.png"); ;
                            }
                        }
                    }
                    catch (Exception)
                    {

                    }
                }              
            }
        }
        private void bunifuCustomDataGrid1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (bunifuCustomDataGrid1.Columns[e.ColumnIndex].Name == "Status")
            {
                bunifuCustomDataGrid1.Columns[e.ColumnIndex].Visible = false;
            }
        }

        private void bunifuCustomDataGrid1_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                var hti = bunifuCustomDataGrid.HitTest(e.X, e.Y);
                this.bunifuCustomDataGrid.Rows[hti.RowIndex].Selected = true;
                this.rowIndex = hti.RowIndex;
                this.columnIndex = hti.ColumnIndex;
            }
            catch (Exception)
            {

            }

        }

        private void bunifuCustomDataGrid1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            Function_Settings fnc = new Function_Settings();
            for (int i = 0; i < bunifuCustomDataGrid1.RowCount; i++)
            {
                string val = bunifuCustomDataGrid1.Rows[i].Cells[2].Value.ToString();
                //MessageBox.Show(columnIndex.ToString());
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

        
    }
}
