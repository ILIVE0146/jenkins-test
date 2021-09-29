// using System;
// using System.Net;
// using System.Net.Sockets;

// namespace Calculator
// {
//     class Sender
//     {
//         static void Connect(String server, String message, Int32 port)
//         {
//             try
//             {
//                 // Create a TcpClient.
//                 // Note, for this client to work you need to have a TcpServer
//                 // connected to the same address as specified by the server, port
//                 // combination.
//                 TcpClient client = new TcpClient(server, port);

//                 // Translate the passed message into ASCII and store it as a Byte array.
//                 Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);

//                 // Get a client stream for reading and writing.
//                 //  Stream stream = client.GetStream();

//                 NetworkStream stream = client.GetStream();

//                 // Send the message to the connected TcpServer.
//                 stream.Write(data, 0, data.Length);

//                 Console.WriteLine("Sent: {0}", message);

//                 // Receive the TcpServer.response.

//                 // Buffer to store the response bytes.
//                 data = new Byte[256];

//                 // String to store the response ASCII representation.
//                 String responseData = String.Empty;

//                 // Read the first batch of the TcpServer response bytes.
//                 Int32 bytes = stream.Read(data, 0, data.Length);
//                 responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
//                 Console.WriteLine("Received: {0}", responseData);

//                 // Close everything.
//                 stream.Close();
//                 client.Close();
//             }
//             catch (ArgumentNullException e)
//             {
//                 Console.WriteLine("ArgumentNullException: {0}", e);
//             }
//             catch (SocketException e)
//             {
//                 Console.WriteLine("SocketException: {0}", e);
//             }

//             Console.WriteLine("\n Press Enter to continue...");
//             Console.Read();
//         }
//         static void Main()
//         {
//             Sender.Connect("127.0.0.1","10 5",1022);
//         }
//     }
// }
using System;
using System.Net;
using System.Net.Sockets;
class test
{
    static void Connect(String server, String[] message)
    {
        try
        {
            // Create a TcpClient.
            // Note, for this client to work you need to have a TcpServer
            // connected to the same address as specified by the server, port
            // combination.
            Int32 port = 1022;
            TcpClient client = new TcpClient(server, port);

            // Translate the passed message into ASCII and store it as a Byte array.
            string tosend = message[0];
            foreach (string item in message)
            {
                if (item == message[0])
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

            // String to store the response ASCII representation.
            string responseData = String.Empty;

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

        Console.WriteLine("\n Press Enter to continue...");
        Console.Read();
    }

    static void Main(string[] args)
    {
        Connect("127.0.0.1", args);
    }
}