using System;
using System.Net;
using System.Net.Sockets;

namespace Calculator
{
    class Divisor
    {
        static void Main(string[] args)
        {

            switch (args.Length)
            {
                case 0:
                    Console.WriteLine("ERROR: Invaid Numbers of Arguments given\nExpected: PORT");
                    Environment.Exit(-1);
                    break;
                case > 1:
                    Console.WriteLine("WARNING: More than 1 command line argments given\nonly using the first one");
                    break;
            }

            TcpListener server = null;
            try
            {
                Int32 port = int.Parse(args[0]);
                IPAddress localAddr = IPAddress.Parse("127.0.0.1");
                server = new TcpListener(localAddr, port);
                // Start listening for client requests.
                server.Start();
                Byte[] bytes = new Byte[256];
                String data = null;
                bool exitServer = false;

                while (!exitServer)
                {
                    Console.Write("Waiting for a connection... ");

                    TcpClient client = server.AcceptTcpClient();
                    Console.WriteLine("Connected!");

                    data = null;
                    // Get a stream object for reading and writing
                    NetworkStream stream = client.GetStream();

                    int i;

                    // Loop to receive all the data sent by the client.
                    while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                    {
                        // Translate data bytes to a ASCII string.
                        data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);

                        if (data == "exit")
                        {
                            client.Close();
                            exitServer = true;
                            break;
                        }

                        Console.WriteLine("Received: {0}", data);

                        // Process the data sent by the client.
                        string[] numlist = data.Split(' ');
                        float div = 0;
                        foreach (string item in numlist)
                        {
                            if (item == numlist[0])
                            {
                                div = float.Parse(item);
                                continue;
                            }
                            div = div / float.Parse(item);
                        }

                        data = div.ToString();
                        byte[] msg = System.Text.Encoding.ASCII.GetBytes(data);

                        // Send back a response.
                        stream.Write(msg, 0, msg.Length);
                        Console.WriteLine("Sent(DIVISION): {0}", data);
                    }

                    // Shutdown and end connection
                    client.Close();

                    if (exitServer == true)
                    {
                        server.Stop();
                        break;
                    }
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
            finally
            {
                // Stop listening for new clients.
                server.Stop();
            }

            Console.WriteLine("EXITING");
        }
    }
}