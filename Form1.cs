using System;
using System.Windows.Forms;
using CCWin;
using System.Net;
using System.Diagnostics;

namespace CodeJudge
{
    public partial class Form1 : Skin_Mac
    {
        Stopwatch sw = new Stopwatch();
        TimeSpan ts;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        void wc_UploadProgressChanged(object sender, UploadProgressChangedEventArgs e)
        {
            skinProgressBar1.Maximum = (int)e.TotalBytesToSend;
            skinProgressBar1.Value = (int)e.BytesSent;
            ts = sw.Elapsed;
            double SendDataSize = (double)e.BytesSent;
            double speed=SendDataSize/ts.TotalMilliseconds; //KB/S
            skinButton1.Text = speed.ToString("0.00 KB/S");
        }


        private void skinButton1_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = ""; //对话框初始化
            openFileDialog1.ShowDialog();//显示对话框
            String total_filename=openFileDialog1.FileName;
            String short_filename=openFileDialog1.SafeFileName;
            //MessageBox.Show(total_filename);
            if (short_filename.ToString() != "")
            {
                String keyword = Microsoft.VisualBasic.Interaction.InputBox("请输入口令:", "安全验证"); //对输入的口令进行加密运算
                string md5_password = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(keyword.ToString(), "MD5");
                //MessageBox.Show(password);
                if (md5_password.ToString() == "16F09AE0A377EDE6206277FAD599F9A0")
                {

                    String url_link = "ftp://clouduser:" + keyword.ToString() + "@133.130.89.177/" + short_filename;
                    WebClient wc = new WebClient();
                    wc.UploadProgressChanged += new UploadProgressChangedEventHandler(wc_UploadProgressChanged);
                    wc.UploadFileCompleted += new UploadFileCompletedEventHandler(wc_UploadFileCompleted);
                    wc.UploadFileAsync(new Uri(url_link), total_filename);
                    skinButton1.Enabled = false;
                    skinButton1.ForeColor = System.Drawing.Color.Black;
                    //计算用时，计算上传速度
                    sw.Reset();
                    sw.Start();


                }
                else
                    MessageBox.Show("口令验证失败！");
            }
        }

        void wc_UploadFileCompleted(object sender, UploadFileCompletedEventArgs e)
        {
            skinButton1.Enabled = true;
            sw.Stop();
            skinButton1.Text = "选择文件";
            skinProgressBar1.Value = 0;
        }

        private void mouse_on(object sender, MouseEventArgs e)
        {
            skinButton1.BaseColor =System.Drawing.Color.DarkRed;
            skinButton1.ForeColor = System.Drawing.Color.White;
        }

        private void form_mouse_on(object sender, MouseEventArgs e)
        {
            skinButton1.BaseColor = System.Drawing.Color.Peru;
            skinButton1.ForeColor = System.Drawing.Color.Black;
        }
    }
}
