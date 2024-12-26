using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.Json;

class TeacherServer
{
    private static ConcurrentQueue<Character> characters = new();
    private static long startTime = DateTime.Now.ToFileTime();

    static void Main(string[] args)
    {
        TcpListener listener = new TcpListener(IPAddress.Any, 12345);
        listener.Start();

        Console.WriteLine("Server ceka na dalsi hrace...");

        // Server bude čekat, dokud nedostane dostatek postav
        while (true)
        {
            TcpClient client = listener.AcceptTcpClient();
            NetworkStream stream = client.GetStream();
            string clientAddress = client.Client.RemoteEndPoint.ToString();

            // Příjem postavy od klienta (student)
            using (var reader = new StreamReader(stream))
            {
                try
                {
                    string json = reader.ReadLine(); // Přečteme JSON řetězec
                    Character character = JsonSerializer.Deserialize<Character>(json); // Deserializace JSON na objekt

                    if (character == null || character.Name == null || character.Name.Length == 0 || character.Health > 1000 || character.Strength > 100 || character.DoubleDamageChance < 0 ||
                        character.DoubleDamageChance > 100 ||
                        character.HealChance < 0 || character.HealChance > 100 || character.Heal > 100)
                    {
                        Console.WriteLine($"[LOBBY] Prazdne jmeno, HP > 1000 nebo sila > 100!");
                        throw new Exception();
                    }

                    Console.WriteLine($"[LOBBY] + {character.Name} ({character.Health} HP, {character.Strength} Attack)");
                    characters.Enqueue(character);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[LOBBY] Chybny charakter! Z IP adresy {clientAddress}");
                }
            }

            client.Close();

            if (characters.Count > 1)
                Task.Run(() => StartTournamentAsync());
        }
    }

    // Metoda pro simulaci turnaje mezi postavami
    static async Task StartTournamentAsync()
    {
        long arenaId = DateTime.Now.ToFileTime() - startTime;
        List<Character> tournamentCharacters;

        // Copy characters list for the tournament
        tournamentCharacters = new List<Character>();
        Character character1, character2;
        if (!characters.TryDequeue(out character1) || !characters.TryDequeue(out character2))
        {
            return;
        }

        tournamentCharacters.Add(character1);
        tournamentCharacters.Add(character2);

        Console.WriteLine($"\n[ARENA {arenaId}] Start of fight: {tournamentCharacters[0].Name} ({tournamentCharacters[0].Health} HP)" +
    $" vs {tournamentCharacters[1].Name} ({tournamentCharacters[1].Health} HP)");

        // Simulace zápasů (postavy budou útočit na sebe v pořadí)
        while (tournamentCharacters.Count > 1)
        {
            int first = new Random().Next(0, 1);
            Character player1 = tournamentCharacters[first];
            Character player2 = tournamentCharacters[1-first];

            // Simulace útoku
            while (!player1.IsDead() && !player2.IsDead())
            {
                player1.Attack(arenaId, player2);
                if (!player2.IsDead())
                {
                    player2.Attack(arenaId, player1);
                }

                Console.WriteLine($"\n[ARENA {arenaId}] {tournamentCharacters[0].Name} ({tournamentCharacters[0].Health} HP)" +
    $" vs {tournamentCharacters[1].Name} ({tournamentCharacters[1].Health} HP)");
            }

            // Určíme vítěze
            if (player1.IsDead())
            {
                Console.WriteLine($"[ARENA {arenaId}] {player2.Name} Wins and remains in arena with {player2.Health} HPs!");
                tournamentCharacters.RemoveAt(0); // Odstraníme prohranou postavu
            }
            else
            {
                Console.WriteLine($"[ARENA {arenaId}] {player1.Name} Wins and remains in arena with {player1.Health} HPs!");
                tournamentCharacters.RemoveAt(1); // Odstraníme prohranou postavu
            }
        }

        // Konečný vítěz
        characters.Enqueue(tournamentCharacters[0]);
    }
}