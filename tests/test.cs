using System;
using System.Net.Sockets;
using System.Collections.Generic;
class Test
{
    static int Connect(int port, String[] message)
    {
        // String to store the response ASCII representation.
        string responseData = String.Empty;
        try
        {
            // Create a TcpClient.
            // Note, for this client to work you need to have a TcpServer
            // connected to the same address as specified by the server, port
            // combination.
            TcpClient client = new TcpClient("127.0.0.1", port);

            // Translate the passed message into ASCII and store it as a Byte array.
            string tosend = null;
            foreach (string item in message)
            {
                if (item == message[0])
                {
                    tosend = item;
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
            if (message[0] != "exit")
            {
                data = new Byte[256];

                // Read the first batch of the TcpServer response bytes.
                Int32 bytes = stream.Read(data, 0, data.Length);
                responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                Console.WriteLine("Received: {0}", responseData);
            }
            else
            {
                responseData = "0";
            }

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
    public enum exitCodes : int
    {
        AdditionError = -1,
        SubtractionError = -2,
        DivisionError = -3,
        MultiplicationError = -4
    }

    static void Main(string[] args)
    {

        switch (args.Length)
        {
            case <= 4:
                Console.WriteLine("ERROR: Invaid Numbers of Arguments given\nREQUIRED: PORT-ADDITION PORT-SUBTRACTION PORT-MULTIPICATION PORT-DIVISION NUMBERS");
                Environment.Exit(-1);
                break;
            case 5:
                Console.WriteLine("WARNING: Only One Number is given.");
                break;
        }
        List<string> parameters = new List<string>();
        for (int i = 4; i < args.Length; ++i)
        {
            parameters.Add(args[i]);
        }

        Console.WriteLine("Testing Addition service");
        float sum = 0;
        foreach (string item in parameters)
        {
            if (item == parameters[0])
            {
                sum = float.Parse(item);
                continue;
            }
            sum = sum + float.Parse(item);
        }
        if (sum == Connect(int.Parse(args[0]), parameters.ToArray()))
        {
            Console.WriteLine("Addition success");
        }
        else
        {
            Console.WriteLine("Addition failed");
            Environment.ExitCode = (int)Test.exitCodes.AdditionError;
            Environment.Exit(Environment.ExitCode);
        }
        Connect(int.Parse(args[0]), new string[] { "exit" });

        Console.WriteLine("Testing Subtraction service");
        sum = 0;
        foreach (var item in parameters)
        {

            if (item == parameters[0])
            {
                sum = float.Parse(item);
                continue;
            }
            sum = sum - float.Parse(item);
        }

        if (sum == Connect(int.Parse(args[1]), parameters.ToArray()))
        {
            Console.WriteLine("Subtraction success");
        }
        else
        {
            Console.WriteLine("Subtraction failed");
            Environment.ExitCode = (int)Test.exitCodes.SubtractionError;
            Environment.Exit(Environment.ExitCode);
        }
        Connect(int.Parse(args[1]), new string[] { "exit" });
        Console.WriteLine("Testing multiplication service");
        sum = 0;
        foreach (var item in parameters)
        {
            if (item == parameters[0])
            {
                sum = float.Parse(item);
                continue;
            }
            sum = sum * float.Parse(item);
        }
        if (sum == Connect(int.Parse(args[2]), parameters.ToArray()))
        {
            Console.WriteLine("multiplication success");
        }
        else
        {
            Console.WriteLine("multiplication failed");
            Environment.ExitCode = (int)Test.exitCodes.MultiplicationError;
            Environment.Exit(Environment.ExitCode);
        }
        Connect(int.Parse(args[2]), new string[] { "exit" });

        Console.WriteLine("Testing division service");
        sum = 0;
        foreach (var item in parameters)
        {
            if (item == parameters[0])
            {
                sum = float.Parse(item);
                continue;
            }
            sum = sum / float.Parse(item);
        }
        if (sum == Connect(int.Parse(args[3]), parameters.ToArray()))
        {
            Console.WriteLine("division success");
        }
        else
        {
            Console.WriteLine("division failed");
            Environment.ExitCode = (int)Test.exitCodes.DivisionError;
            Environment.Exit(Environment.ExitCode);
        }
        Connect(int.Parse(args[3]), new string[] { "exit" });
    }
}