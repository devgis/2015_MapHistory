using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace HistoryMap
{
    public partial class InfoWindow : Form
    {
        string _mapType=string.Empty;
        public string MultiMediaPath = string.Empty;
        public InfoWindow(string MapType)
        {
            InitializeComponent();
            _mapType = MapType;
        }

        private void InfoWindow_Load(object sender, EventArgs e)
        {
            if (!Directory.Exists(MultiMediaPath))
                return;
            string txtPath = Directory.GetFiles(MultiMediaPath,"*.txt")[0];
            string imgPath = Directory.GetFiles(MultiMediaPath, "*.jpg")[0];
            //string mp3Path = Directory.GetFiles(MultiMediaPath, "*.mp3")[0];

            //switch (_mapType)
            //{
            //    case "秦":
            //        txtPath = Path.Combine(Application.StartupPath, @"其他\文字\秦.txt");
            //        imgPath = Path.Combine(Application.StartupPath, @"其他\图片\秦——秦始皇.jpg");
            //        mp3Path = Path.Combine(Application.StartupPath, @"其他\音频\西汉雄风.mp3");
            //        break;
            //    case "西汉":
            //        txtPath = Path.Combine(Application.StartupPath, @"其他\文字\西汉.txt");
            //        imgPath = Path.Combine(Application.StartupPath, @"其他\图片\西汉-汉武帝.jpg");
            //        mp3Path = Path.Combine(Application.StartupPath, @"其他\音频\西汉雄风.mp3");
            //        break;
            //    case "东汉":
            //        txtPath = Path.Combine(Application.StartupPath, @"其他\文字\东汉.txt");
            //        imgPath = Path.Combine(Application.StartupPath, @"其他\图片\东汉-造纸术.jpg");
            //        mp3Path = Path.Combine(Application.StartupPath, @"其他\音频\东汉兴衰.mp3");
            //        break;
            //    case "当今":
            //        txtPath=Path.Combine(Application.StartupPath,"txtimagemv\\q.txt");
            //        imgPath = Path.Combine(Application.StartupPath, @"其他\图片\秦——兵马俑.jpg");
            //        mp3Path = Path.Combine(Application.StartupPath, @"其他\音频\西汉雄风.mp3");
            //        break;
            //}

            textBox1.Text=File.ReadAllText(txtPath,Encoding.Default);
            pictureBox1.Image = Image.FromFile(imgPath);
            //Mp3Player mp3 = new Mp3Player(mp3Path, Handle);
            //mp3.Open();

            //this.panel1.Controls.Clear();
            //vsFlexLib.
            //AxShockwaveFlashObjects.AxShockwaveFlash axShockwaveFlash1 = new AxShockwaveFlashObjects.AxShockwaveFlash();
            //((System.ComponentModel.ISupportInitialize)(axShockwaveFlash1)).BeginInit();
            //this.panel1.Controls.Add(axShockwaveFlash1);
            //this.panel1.Visible = true;
            //axShockwaveFlash1.Name = "axShockwaveFlash1";
            //axShockwaveFlash1.Size = new System.Drawing.Size(323, 262);
            //axShockwaveFlash1.TabIndex = 28;
            //((System.ComponentModel.ISupportInitialize)(axShockwaveFlash1)).EndInit();

            //axShockwaveFlash1.FlashVars = @"file=" + constant.STARTPATH + "/pic/a.flv" + @"&type=video&autostart=true&repeat=true&skin=" + constant.STARTPATH + @"/Resources/glow.zip";
            //axShockwaveFlash1.LoadMovie(0, constant.STARTPATH + @"/Resources/flvplayer.swf");
            //axShockwaveFlash1.Play();

        }

        private void btPlay_Click(object sender, EventArgs e)
        {
            //string flvPath = Path.Combine(Application.StartupPath, @"其他\视频\西汉-丝绸之路- （零成本历史科普记录片）.flv");
            string flvPath = Directory.GetFiles(MultiMediaPath, "*.flv")[0];
            System.Diagnostics.ProcessStartInfo info = new System.Diagnostics.ProcessStartInfo();
            info.FileName = flvPath; //"iexplore.exe";
            //info.Arguments = flvPath;
            //info.WorkingDirectory = "c:\\";
            System.Diagnostics.Process pro;
            try
            {
                pro = System.Diagnostics.Process.Start(info);
            }
            catch (System.ComponentModel.Win32Exception ex)
            {
                MessageBox.Show("系统找不到指定的文件。\r{0}", ex.ToString());
                return;
            }
        }
    }
}
