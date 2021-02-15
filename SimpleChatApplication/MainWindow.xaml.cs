using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace SimpleChatApplication
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        TcpClient server = new TcpClient();
        StreamWriter sw;
        StreamReader sr;
        string username;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnConnect_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(txtName.Text))
                    username = "Vollpfosten: ";
                else
                    username = txtName.Text + ": ";

                server.Connect(txtServer.Text, int.Parse(txtPort.Text));

                sw = new StreamWriter(server.GetStream());
                sr = new StreamReader(server.GetStream());

                btnSend.IsEnabled = true;
                btnConnect.IsEnabled = false;

                ReadData();
            } catch
            {
                MessageBox.Show("Generische Fehlermeldung bitte hier einfügen!");
            }
        }

        async void ReadData()
        {
            string receivedString = await sr.ReadLineAsync();
            txtChat.Text += receivedString + Environment.NewLine;
            txtChat.ScrollToEnd();
            ReadData();
        }

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(txtEingabe.Text))
            {
                sw.WriteLine(username + txtEingabe.Text);
                sw.Flush();

                txtChat.Text += "Ich: " + txtEingabe.Text + Environment.NewLine;
                txtEingabe.Text = "";
                txtChat.ScrollToEnd();
                txtEingabe.Focus();
            }
        }
    }
}
