using System;
using System.Net;
using System.Net.Sockets;
class test
{
    static int Connect(String server, String[] message)
    {
        // String to store the response ASCII representation.
        string responseData = String.Empty;
        try
        {
            // Create a TcpClient.
            // Note, for this client to work you need to have a TcpServer
            // connected to the same address as specified by the server, port
            // combination.
            Int32 port = int.Parse(message[0]);
            TcpClient client = new TcpClient(server, port);

            // Translate the passed message into ASCII and store it as a Byte array.
            string tosend = message[1];
            foreach (string item in message)
            {
                if (item == message[0] || item == message[1])
                {
                    continue;
                }
                tosend = tosend + " " + item;
            }
            Byte[] data = System.Text.Encoding.ASCII.GetBytes(tosend);

            // Get a client stream for reading and writing.
            //  Stream stream = client.GetStream();

            NetworkStream stream = client.GetStream();

            // Send the message to the connected TcpServer.
            stream.Write(data, 0, data.Length);

            Console.WriteLine("Sent: {0}", tosend);

            // Receive the TcpServer.response.

            // Buffer to store the response bytes.
            data = new Byte[256];

            // Read the first batch of the TcpServer response bytes.
            Int32 bytes = stream.Read(data, 0, data.Length);
            responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
            Console.WriteLine("Received: {0}", responseData);

            // Close everything.
            stream.Close();
            client.Close();
        }
        catch (ArgumentNullException e)
        {
            Console.WriteLine("ArgumentNullException: {0}", e);
        }
        catch (SocketException e)
        {
            Console.WriteLine("SocketException: {0}", e);
        }
        return int.Parse(responseData);
    }

    static int Main(string[] args)
    {
        int sum = 0;
        foreach (var item in args)
        {
            if(item == args[0])
            {
                continue;
            }
            sum = sum + int.Parse(item);
        }
        
        if (sum == Connect("127.0.0.1", args))
        {
            return 0;
        }
        else
        {
            return -1;
        }
    }
}