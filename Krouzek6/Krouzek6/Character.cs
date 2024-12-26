using System.ComponentModel;

class Character
{
    public string Name { get; set; }
    public int Health { get; set; }
    public int Strength { get; set; }

    public double DoubleDamageChance { get; set; }

    public int Heal { get; set; }
    public double HealChance { get; set; }

    public Character() { }

    public Character(string name, int health, int strength, double doubleDamageChance, int heal, double healChance)
    {
        Name = name;
        Health = health;
        Strength = strength;
        DoubleDamageChance = doubleDamageChance;
        Heal = heal;
        HealChance = healChance;
    }

    // Metoda pro útok na jinou postavu
    public void Attack(long arenaId, Character opponent)
    {
        if (new Random().NextDouble() <= DoubleDamageChance / 100.0)
        {
            opponent.Health -= 2 * this.Strength;
            Console.WriteLine($"\n[ARENA {arenaId}] {Name} -> {opponent.Name}: DOUBLE DAMAGE -{Strength*2}");
        }
        else
        {
            opponent.Health -= this.Strength;
            Console.WriteLine($"\n[ARENA {arenaId}] {Name} -> {opponent.Name}: -{Strength}");
        }
    }

    public void Healer(long arenaId)
    {
        if (new Random().NextDouble() <= Heal/100.0)
        {
            Console.WriteLine($"\n[ARENA {arenaId}] {Name} healed {Heal} HP");
            Health += Heal;
        }
    }

    // Určuje, zda je postava mrtvá
    public bool IsDead()
    {
        return Health <= 0;
    }
}