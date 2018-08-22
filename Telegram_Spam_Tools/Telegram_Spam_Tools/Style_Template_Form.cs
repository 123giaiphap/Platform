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
        public string Fore_Color = string.Empty;
        public string Back_Color = string.Empty;
        public string Font_Name = string.Empty;
        public int Font_size;        
        internal void Style(string name)
        {
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
        }
    }
}
