using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Telegram_Spam_Tools
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            pictureBox1.ImageLocation = @"image/lock.png";
            txtUser.Icon = Image.FromFile(@"image/user.png");
            txtPass.Icon = Image.FromFile(@"image/pass.png");
        }
    }
}
