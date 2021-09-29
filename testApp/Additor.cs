﻿using System;
using System.Net;
using System.Net.Sockets;

namespace Calculator
{
    class Additor
    {
        static void Main()
        {


            TcpListener server = null;
            try
            {
                Int32 port = 1022;
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
                        Console.WriteLine("Received: {0}", data);

                        // Process the data sent by the client.
                        int sum = 0;
                        string[] numlist = data.Split(' ');
                        foreach (string item in numlist)
                        {
                            sum = sum + int.Parse(item);
                        }

                        data = sum.ToString();
                        byte[] msg = System.Text.Encoding.ASCII.GetBytes(data);

                        // Send back a response.
                        stream.Write(msg, 0, msg.Length);
                        Console.WriteLine("Sent(sum): {0}", data);
                    }

                    // Shutdown and end connection
                    client.Close();
                    exitServer = true;
                    server.Stop();
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