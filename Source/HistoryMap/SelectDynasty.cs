using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace HistoryMap
{
    public partial class SelectDynasty : Form
    {
        public SelectDynasty()
        {
            InitializeComponent();
        }

        private void btXiHan_Click(object sender, EventArgs e)
        {
            if (sender is Button)
            {
                string txt = (sender as Button).Text;
                MainMap frmMapForm = new MainMap();
                switch (txt)
                {
                    case "中华人民共和国":
                        frmMapForm.DynastyPath = "01中华人民共和国";
                        break;
                    case "秦":
                        frmMapForm.DynastyPath = "02秦历史地图";
                        break;
                    case "西汉":
                        frmMapForm.DynastyPath = "03东汉历史地图";
                        break;
                    case "东汉":
                        frmMapForm.DynastyPath = "04西汉历史地图";
                        break;
                }
                this.Hide();
                frmMapForm.ShowDialog();
            }
        }
    }
}
