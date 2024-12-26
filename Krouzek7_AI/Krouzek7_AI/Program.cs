using System.Net;
using System.Net.Sockets;
using System.Text.Json;

Node root;
object lockObject = new();

void Uloz(Node root)
{
    try
    {
        string json = JsonSerializer.Serialize(root, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText("data.json", json);
    }
    catch (Exception exc)
    {
        Console.WriteLine(exc);
    }
}

try
{
    string json = File.ReadAllText("data.json");
    root = JsonSerializer.Deserialize<Node>(json);
}
catch (Exception exc)
{
    Console.WriteLine(exc);
    root = new Node("Je to zvire?");
    root.otazkaAno = new Node("pes");
    root.otazkaNe = new Node("Andrej Babis");
}

Console.WriteLine("Server bezi...");

TcpListener listener = new TcpListener(IPAddress.Any, 12345);
listener.Start();

while (true)
{
    TcpClient client = listener.AcceptTcpClient();
    Thread clientThread = new Thread(() => { try
    {
        HandleClient(client);
    } catch (Exception exc) { Console.WriteLine($"Chyba: {exc.Message}"); } });
    clientThread.Start();
}

void HandleClient(TcpClient client)
{
    using NetworkStream stream = client.GetStream();
    using StreamReader reader = new StreamReader(stream);
    using StreamWriter writer = new StreamWriter(stream) { AutoFlush = true };

    Console.WriteLine("Client connected, trying to read message:");
    writer.WriteLine("Mysli na nejakou postavu:");
    Node node = root;

    while (true)
    {
        
            if (node.otazkaAno == null || node.otazkaNe == null)
            {
                writer.WriteLine($"Je to {node.otazka}?");
                string answer = reader.ReadLine();
                if (answer == "ano")
                {
                    writer.WriteLine("YES! Vyhral jsem!");
                    break;
                }
                else
                {
                    writer.WriteLine("Neuhadl jsem :( Na co jsi myslel?");
                    string novaPostava = reader.ReadLine();
                    writer.WriteLine($"Napis otazku, podle ktere poznam rozdil mezi {novaPostava} a {node.otazka}:");
                    string novaOtazka = reader.ReadLine();
                    writer.WriteLine($"Je odpoved na otazku {novaOtazka} pro {novaPostava} ano?");
                    answer = reader.ReadLine();
                    lock (lockObject)
                    {
                        if (answer == "ano")
                        {
                            node.otazkaAno = new Node(novaPostava);
                            node.otazkaNe = new Node(node.otazka);
                            node.otazka = novaOtazka;
                        }
                        else
                        {
                            node.otazkaAno = new Node(node.otazka);
                            node.otazkaNe = new Node(novaPostava);
                            node.otazka = novaOtazka;
                        }
                        node = root;
                        Uloz(root);
                    }
                    writer.WriteLine("Ted jsem chytrejsi, hraj znovu:");
                }
            }
            else
            {
                writer.WriteLine(node.otazka);
                string answer = reader.ReadLine();
                node = (answer == "ano") ? node.otazkaAno : node.otazkaNe;
            }
        
    }
}