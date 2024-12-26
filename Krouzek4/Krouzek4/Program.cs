int NactiStranu()
{
    Console.WriteLine("Zadej stranu obdelniku:");
    string text_a = Console.ReadLine();
    int a;
    bool uspech = int.TryParse(text_a, out a);

    if (uspech)
    {
        return a;
    }
    else
    {
        Console.WriteLine("Toto neni cislo!");
        Environment.Exit(0);
        return 0;
    }
}

void VypisRadek(int j, int sirka)
{
    for (int i = 0; i < sirka; i += 1)
    {
        if (j % 2 == 0)
        {
            Console.Write("#");
        }
        else
        {
            Console.Write(".");
        }
    }
    Console.WriteLine();
}

int a = NactiStranu();
int b = NactiStranu();

// Vykreslit obdélník
for (int j = 0; j < b; j += 1)
{
    VypisRadek(j, a);
}

