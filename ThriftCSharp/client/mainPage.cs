using app.Model;
using app.services;
using client;
using model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Thrift.Protocol;
using Thrift.Transport;

namespace app.client
{
    public partial class mainPage : Form
    {
        public static Boolean needsUpdate { get; set; }
        private IList<ShowDTO> showData;
        private int port;
        private string currentClient;
        SqlDataAdapter adapter = new SqlDataAdapter();
        DataSet set = new DataSet();
        ClientCtrl ctrl;
        LoginPage loginWindow;
       // private Employee CurrentUser { get; set; }
        
        //public event EventHandler<UserEventArgs> updateEvent; //ctrl calls it when it has received an update
        
            
        //public mainPage(Employee client, ClientCtrl ct, LoginPage loginPage)
        public mainPage(int port, LoginPage loginPage,String currentClient)
        {
            InitializeComponent();
            this.port = port;
            Task.Run(() => pageNeedsUpdate());
            this.loginWindow = loginPage;
            this.currentClient = currentClient;
            /*this.ctrl = ct;
            showData = ctrl.getAllShows().ToList<Show>();
            PopulateShowTable();
            this.loginWindow = loginPage;

            this.CurrentUser = client;
            ctrl.updateEvent += userUpdate;*/
        }
        /*protected virtual void OnUserEvent(UserEventArgs e)
        {
            if (updateEvent == null) return;
            updateEvent(this, e);
            Console.WriteLine("Update Event called");
        }*/


        private void mainPage_Load(object sender, EventArgs e)
        {
            CallPopulateTable();
            NotifyServer notifyServer= new NotifyServer(port);
        }

        private void pageNeedsUpdate()
        {
            while (true)
            {
                Thread.Sleep(1000);
                if (needsUpdate)
                {
                    needsUpdate = false;
                    CallPopulateTable();
                }
            }
        }


        public void logout()
        {
            //Console.WriteLine("Ctrl logout");
            // ctrl.logout(CurrentUser);
            // CurrentUser = null;
            // ctrl.updateEvent -= userUpdate;
            TTransport transport = new TSocket("localhost", 9091);
            TProtocol protocol = new TBinaryProtocol(transport);
            transport.Open();

            AppService.Client client = new AppService.Client(protocol);

            String response = client.logOut(this.currentClient, "localhost", port);
            transport.Close();

            if (!response.Equals("loggedOut"))
            {
                throw new Exception("Eroare la logout!");
            }
            loginWindow.Enabled = true;
            loginWindow.Show();
        }


        private void CallPopulateTable(Boolean isUpdated = false)
        {
           if (listaShow.InvokeRequired)
            {
                listaShow.Invoke(new MethodInvoker(delegate
                {
                    PopulateShowTable(isUpdated);
                }));
            }
            else
            {
                PopulateShowTable(isUpdated);
            }
        }

        private void PopulateArtistTable(string data)
        {
            listaArtist.Items.Clear();
            foreach (ShowDTO s in showData.ToList<ShowDTO>())
            {
                if (s.DataTimp.Split('T')[0] == data)
                {
                    var row = new string[] {s.Id.ToString(),s.ArtistName, s.Location.ToString(), s.DataTimp.Split('T')[1], s.NrAvailableSeats.ToString()};
                   var lvi = new ListViewItem(row);
                   listaArtist.Items.Add(lvi);
                  
             }
           }
        }



        private void PopulateShowTable(bool isUpdated)
        {
            if (isUpdated)
            {
                foreach (ShowDTO s in showData.ToList<ShowDTO>())
                {
                    var row = new string[] {s.Id.ToString(), s.ArtistName, s.Location,
                s.DataTimp.Split('T')[0],s.NrAvailableSeats.ToString(),s.NrSoldSeats.ToString()};
                    var lvi = new ListViewItem(row);
                    if (s.NrAvailableSeats == 0)
                        lvi.BackColor = Color.Red;
                    listaShow.Items.Add(lvi);
                }
            }
            else
            {
                TTransport transport = new TSocket("localhost", 9091);
                TProtocol protocol = new TBinaryProtocol(transport);
                transport.Open();

                AppService.Client client = new AppService.Client(protocol);
                showData = client.findAllShows();
                transport.Close();
                listaShow.Items.Clear();
                foreach (ShowDTO s in showData.ToList<ShowDTO>())
                {
                    var row = new string[] {s.Id.ToString(), s.ArtistName, s.Location,
                    s.DataTimp.Split('T')[0],s.NrAvailableSeats.ToString(),s.NrSoldSeats.ToString()};
                    var lvi = new ListViewItem(row);
                    if (s.NrAvailableSeats == 0)
                        lvi.BackColor = Color.Red;
                    listaShow.Items.Add(lvi);
                }
            }

        }

        private void tabelShow_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void locuriBox_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (numeCBox.Text != null && locuriCBox.Text != null && listaShow.SelectedItems.Count == 1)
            {
                try
                {
                    int nrLocuri = int.Parse(locuriCBox.Text);
                    int idShow = int.Parse(listaShow.SelectedItems[0].SubItems[0].Text);
                    int locuriDisp= int.Parse(listaShow.SelectedItems[0].SubItems[4].Text);
                    if (locuriDisp >= nrLocuri) {
                        //Show s = new Show() { Id = idShow };
                        string numeC = numeCBox.Text;
                        //Ticket t = new Ticket() { IdShow = idShow, NrSeatsWanted = nrLocuri, BuyerName = numeC };
                        ShowDTO showDTO = new ShowDTO();
                        showDTO.Id = idShow;
                        TicketDTO ticketDTO = new TicketDTO();
                        ticketDTO.IdShow = idShow;
                        ticketDTO.NrWantedSeats = nrLocuri;
                        ticketDTO.BuyerName = numeC;
                        TTransport transport = new TSocket("localhost", 9091);
                        TProtocol protocol = new TBinaryProtocol(transport);
                        transport.Open();

                        AppService.Client client = new AppService.Client(protocol);
                        client.ticketsSold(showDTO, ticketDTO);
                        transport.Close();
                        CallPopulateTable();
                        //this.ctrl.ticketsSold(s, t);
                    }
                       else
                          MessageBox.Show("Nu avem suficiente locuri libere!");
                    numeCBox.Clear();
                    locuriCBox.Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ati introdus gresit datele in campul de locuri dorite!");
                }
            }
            else
            {
                MessageBox.Show("Va rugam completati toate campurile si selectati un spectacol din lista");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DateTime dataTime = dateTimePicker.Value.Date;
            PopulateArtistTable(dataTime.ToString("yyyy-MM-dd"));
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.logout();
            this.Close();
        }

       /* private void updateListView(ListView listView, IList<Show> newData)
        {
            listView.Items.Clear();

            foreach (Show s in newData.ToList<Show>())
            {
                var row = new string[] {s.Id.ToString(), s.ArtistName, s.Location,
                s.ShowDateTime.ToShortDateString(),s.NrAvailableSeats.ToString(),s.NrSoldSeats.ToString()};
                var lvi = new ListViewItem(row);
                if (s.NrAvailableSeats == 0)
                    lvi.BackColor = Color.Red;
                listView.Items.Add(lvi);

            }
        }

        public delegate void UpdateListViewCallback(ListView list, IList<Show> data);*/



        /*public void userUpdate(object sender, UserEventArgs e)
        {

            if (e.UserEventType == UpdateType.TICKETS_SOLD)
            {
                String showInfo = e.Data.ToString();

                Show showUpdated = (Show)e.Data;
                foreach (Show s in showData)
                {
                    if (s.Id == showUpdated.Id)
                    {
                        showData.Remove(s);
                        break;
                    }
                }
                    showData.Add(showUpdated);

                    Console.WriteLine("[MainWindow] Updated Meci " + showInfo);
                    listaShow.BeginInvoke(new UpdateListViewCallback(this.updateListView), new Object[] { listaShow, showData });
                }
            }*/
        }

    }

