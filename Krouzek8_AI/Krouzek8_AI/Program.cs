using System.Net.Sockets;

try
{
    using TcpClient client = new TcpClient("192.168.108.86", 12345);
    using NetworkStream stream = client.GetStream();
    using StreamReader reader = new StreamReader(stream);
    using StreamWriter writer = new StreamWriter(stream) { AutoFlush = true };

    Console.WriteLine(reader.ReadLine());

    string response;
    while ((response = reader.ReadLine()) != null)
    {
        Console.WriteLine(response);
        if (response.StartsWith("Ted jsem chytrejsi") || response.StartsWith("YES! Vyhral jsem!"))
        {
            break;
        }

        string input = Console.ReadLine();
        writer.WriteLine(input);
    }
}
catch (Exception ex)
{
    Console.WriteLine($"Chyba: {ex.Message}");
}
