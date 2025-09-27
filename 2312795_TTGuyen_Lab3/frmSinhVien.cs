using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2312795_TTGuyen_Lab3
{
    public partial class frmSinhvien : Form
    {
        private QLSinhVien qlSinhVien;
        private SinhVien sinhVienSelected; // Để cập nhật
        public frmSinhvien()
        {
            InitializeComponent();
            qlSinhVien = new QLSinhVien("TXT");
        }
        

        
       
    }
}
