using System.Net.Sockets;
using System.Text.Json;

class Character
{
    public string Name { get; set; }
    public int Health { get; set; }
    public int Strength { get; set; }

    public Character(string name, int health, int strength)
    {
        this.Name = name;
        this.Health = health;
        this.Strength = strength;
    }
}

class StudentClient
{
    static void Main(string[] args)
    {
        Character character = new Character("Golem", 500, 10);

        // Připojení k serveru
        string serverIp = "192.168.108.86"; // Místní server
        int serverPort = 12345; // Port serveru

        try
        {
            using (TcpClient client = new TcpClient(serverIp, serverPort))
            using (NetworkStream stream = client.GetStream())
            using (var writer = new StreamWriter(stream))
            {
                // Serializace objektu postavy na JSON a odeslání na server
                string json = JsonSerializer.Serialize(character);
                writer.WriteLine(json);  // Odeslání JSON řetězce na server
                writer.Flush();          // Ujistíme se, že všechna data byla odeslána
                Console.WriteLine("Character sent to the server.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}
