using System;
using System.Net.Sockets;
class Test
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
    public enum exitCodes: int
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
            case 0:
                Console.WriteLine("ERROR: Invaid Numbers of Arguments given\nExpected: PORT OPERATOR NUMBERS");
                Environment.Exit(-1);
                break;
            case 1:
                Console.WriteLine("ERROR: Invaid Numbers of Arguments given\nExpected: OPERATOR NUMBERS");
                Environment.Exit(-1);
                break;
            case 2:
                Console.WriteLine("ERROR: Invaid Numbers of Arguments given\nExpected: NUMBERS");
                Environment.Exit(-1);
                break;
            case 3:
                Console.WriteLine("WARNING: Only One Number is given.");
                break;
        }

        Console.WriteLine("Testing Addition service");
        float sum = 0;
        foreach (var item in args)
        {
            if (item == args[0])
            {
                sum = float.Parse(item);
                continue;
            }
            sum = sum + float.Parse(item);
        }
        if (sum == Connect("127.0.0.1", args))
        {
            Console.WriteLine("Addition success");
        }
        else
        {
            Console.WriteLine("Addition failed");
            Environment.ExitCode = (int) Test.exitCodes.AdditionError;
            Environment.Exit(Environment.ExitCode);
        }

        Console.WriteLine("Testing Subtraction service");
        sum = 0;
        foreach (var item in args)
        {
            if (item == args[0])
            {
                sum = float.Parse(item);
                continue;
            }
            sum = sum - float.Parse(item);
        }
        if (sum == Connect("127.0.0.1", args))
        {
            Console.WriteLine("Subtraction success");
        }
        else
        {
            Console.WriteLine("Subtraction failed");
            Environment.ExitCode = (int) Test.exitCodes.SubtractionError;
            Environment.Exit(Environment.ExitCode);
        }

        Console.WriteLine("Testing multiplication service");
        sum = 0;
        foreach (var item in args)
        {
            if (item == args[0])
            {
                sum = float.Parse(item);
                continue;
            }
            sum = sum * float.Parse(item);
        }
        if (sum == Connect("127.0.0.1", args))
        {
            Console.WriteLine("multiplication success");
        }
        else
        {
            Console.WriteLine("multiplication failed");
            Environment.ExitCode = (int) Test.exitCodes.MultiplicationError;
            Environment.Exit(Environment.ExitCode);
        }

        Console.WriteLine("Testing division service");
        sum = 0;
        foreach (var item in args)
        {
            if (item == args[0])
            {
                sum = float.Parse(item);
                continue;
            }
            sum = sum / float.Parse(item);
        }
        if (sum == Connect("127.0.0.1", args))
        {
            Console.WriteLine("division success");
        }
        else
        {
            Console.WriteLine("division failed");
            Environment.ExitCode = (int) Test.exitCodes.DivisionError;
            Environment.Exit(Environment.ExitCode);
        }
    }
}