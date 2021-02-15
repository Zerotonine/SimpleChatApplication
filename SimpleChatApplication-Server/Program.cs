using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;

namespace SimpleChatApplication_Server
{
    class Program
    {
        static TcpListener tcl;
        static List<Client> clients = new List<Client>();
        static void Main(string[] args)
        {
            Console.Write("Bitte Port eingeben: ");
            int port = int.Parse(Console.ReadLine());

            try{
                tcl = new TcpListener(IPAddress.Any, port);
                tcl.Start();
            } catch
            {
            }

            AcceptConnAndRead();
            Console.ReadKey();
        }

        static void AcceptConnAndRead()
        {
            string message;
            NetworkStream ns;
            while (true)
            {
                if (tcl.Pending())
                {
                    clients.Add(new Client(tcl.AcceptTcpClient()));
                }

                for(int i = 0; i < clients.Count; i++)
                {
                    try
                    {
                        ns = clients[i].client.GetStream();
                        if (ns.DataAvailable)
                        {
                            message = clients[i].sr.ReadLine();
                            Console.WriteLine(message);

                            Broadcast(message, clients[i]);
                        }
                    }
                    catch(InvalidOperationException)
                    {
                        clients.RemoveAt(i);
                    }
                }
            }
        }

        static async void Broadcast(string message, Client client)
        {
            foreach(Client c in clients.Where(x=> x != client))
            {
                try
                {
                    await c.sw.WriteLineAsync(message);
                    c.sw.Flush();
                }
                catch {}
            }
        }
    }
}
