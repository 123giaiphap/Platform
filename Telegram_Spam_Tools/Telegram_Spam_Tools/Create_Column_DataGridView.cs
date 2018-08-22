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
    class Create_Column_DataGridView
    {
        public void Create_Image_Column(DataGridViewImageColumn Image_Column, DataGridView Data_GridView, string Column_Name, string Column_HeaderText)
        {
            Image_Column.Name = Column_Name;
            Image_Column.HeaderText = Column_HeaderText;
            Data_GridView.Columns.Add(Image_Column);
        }
        public void Create_TextAndImageColumn(TextAndImageColumn Text_Image_Column, DataGridView Data_GridView, string Column_Name, string Column_HeaderText)
        {
            Text_Image_Column.Name = Column_Name;
            Text_Image_Column.HeaderText = Column_HeaderText;
            Data_GridView.Columns.Add(Text_Image_Column);
        }
    }
}
