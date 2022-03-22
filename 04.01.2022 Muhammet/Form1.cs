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
using SimpleTCP;

namespace _04._01._2022_Muhammet
{
    public partial class Form1 : Form
    {
        SimpleTcpServer server;
        string iptxt = "172.16.100.217";
        int port = 5454;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            server = new SimpleTcpServer();
            server.Delimiter = 0x13;
            server.StringEncoder = Encoding.UTF8;
            server.DataReceived += Server_DataReceived;
        }

        private void Server_DataReceived(object sender, SimpleTCP.Message e)
        {
            richTextBox1.Invoke((MethodInvoker)delegate ()
            {
                richTextBox1.Text += e.MessageString;
                e.ReplyLine(string.Format("Gelen Mesaj {0}", e.MessageString));

            });



        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                label2.Text = "Sistem Başlatılıyor";
                System.Net.IPAddress ip = System.Net.IPAddress.Parse(iptxt);
                server.Start(ip, port);
                Thread.Sleep(1000);
                label2.Text = "Bağlanı Gerçekleşti";
            }
            catch
            {
                label2.Text = "Hata";
            }
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (server.IsStarted)
            {
                label2.Text = "Sistem Durdu";
                server.Stop();
                Thread.Sleep(1000);
                label2.Text = "Sistem Kapatıldı";
            }
        }
    }
}
