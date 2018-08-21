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
    class Style_Template_Form
    {
        private string Fore_Color = string.Empty;
        private string Back_Color = string.Empty;
        private string Font_Name = string.Empty;
        private int Font_size;        
        internal void Style(string name)
        {
            MessageBox.Show(name);
            if (name == "currenry")
            {
                currenry();
                
            }
        }
        internal void currenry()
        {
            this.Fore_Color = "Brown";
            this.Back_Color = "Yellow";
            this.Font_Name = "Roboto";
            this.Font_size = 10;
            Form1 f = new Form1();
            f.style(Fore_Color, Back_Color, Font_Name, Font_size);          
        }
    }
}
