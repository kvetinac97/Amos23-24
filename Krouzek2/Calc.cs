internal class Calculator
{
    static public void Calc()
    {
        Console.WriteLine("Zadej prvni cislo:");
        string text_a = Console.ReadLine();
        double a;
        bool uspech = double.TryParse(text_a, out a);

        if (!uspech)
        {
            Console.WriteLine("Toto neni cislo!");
            return;
        }

        Console.WriteLine("Zadej druhe cislo:");
        string text_b = Console.ReadLine();
        double b;
        uspech = double.TryParse(text_b, out b);

        if (!uspech)
        {
            Console.WriteLine("Toto neni cislo!");
            return;
        }

        Console.WriteLine("Zadej operaci:");
        string operace = Console.ReadLine();

        double soucet = a + b,
               rozdil = a - b,
               soucin = a * b,
               podil  = a / b;

        Console.WriteLine("Kalkulacka: ");

        if (operace == "+")
        {
            Console.Write(a);
            Console.Write(" + ");
            Console.Write(b);
            Console.Write(" = ");
            Console.WriteLine(a + b);
        }

        if (operace == "-")
        {
            Console.Write(a);
            Console.Write(" - ");
            Console.Write(b);
            Console.Write(" = ");
            Console.WriteLine(a - b);
        }

        if (operace == "*")
        {
            Console.Write(a);
            Console.Write(" * ");
            Console.Write(b);
            Console.Write(" = ");
            Console.WriteLine(a * b);
        }

        if (b != 0 && operace == "/")
        {
            Console.Write(a);
            Console.Write(" / ");
            Console.Write(b);
            Console.Write(" = ");
            Console.WriteLine(a / b);
        }
    }
}
