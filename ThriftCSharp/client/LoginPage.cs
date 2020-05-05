using app.client;
using app.Model;
using client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Thrift.Protocol;
using Thrift.Transport;

namespace app.client
{
    public partial class LoginPage : Form
    {
        //private ClientCtrl ctrl;
        public static int port;


        //public LoginPage(ClientCtrl ctrl)
        //{
       //     InitializeComponent();
       //     this.ctrl = ctrl;
       // }
        public LoginPage()
        {
            InitializeComponent();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
           
        }
        private int getFreePort(int start)
        {
            int port = start;
            bool isFree = false;

            while (!isFree)
            {
                isFree = true;

                using (TcpClient tcpClient = new TcpClient())
                {
                    try
                    {
                        tcpClient.Connect("127.0.0.1", port);
                        isFree = false;
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Couldn't connect to port " + port);
                    }
                }

                if (!isFree)
                {
                    port++;
                }
                else
                {
                    return port;
                }
            }
            return -1;
        }

        /*private void button1_Click(object sender, EventArgs e)
        {
            string username = usernameBox.Text;
            string password = passwordBox.Text;
            Employee es = new Employee() { Username = username, Password = password };
            if (username!=null && password!= null){
                try
                {
                    ctrl.login(es);
                    //MessageBox.Show("Login succeded");
                    mainPage chatWin = new mainPage(es,ctrl,this);
                    chatWin.Text = "Chat window for " + username;
                    chatWin.Show();
                    this.Hide();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Nu exista cont cu aceste date de logare! Va rugam incercati din nou!");
                    return;
                }
            }
        }*/
        private void button1_Click(object sender, EventArgs e)
        {
            string username = usernameBox.Text;
            string password = passwordBox.Text;
            try
            {
                TTransport transport = new TSocket("localhost", 9091);
                TProtocol protocol = new TBinaryProtocol(transport);
                transport.Open();

                AppService.Client client = new AppService.Client(protocol);
                port = getFreePort(9092);

                String response = client.login(username, password, "localhost", port);
                transport.Close();

                if (response.Equals("loggedIn"))
                {
                    mainPage chatWin = new mainPage(port, this,username);
                    chatWin.Text = "Window for " + username;
                    chatWin.Show();
                    this.Hide();
                }
                else if (response.Equals("alreadyLoggedIn"))
                {
                    throw new Exception("Utilizator deja conectat!");
                }
                else 
                {
                    throw new Exception("Nu exista cont cu aceste date de logare! Va rugam incercati din nou!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            
        }
    }
}
