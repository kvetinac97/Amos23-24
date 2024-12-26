Console.WriteLine("Zadej 1. cislo:");
string text_a = Console.ReadLine();
double a;
bool uspech = double.TryParse(text_a, out a);

if (!uspech)
{
    Console.WriteLine("Toto neni cislo!");
    return;
}

Console.WriteLine("Zadej 2. cislo:");
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

double soucet = a + b;
double rozdil = a - b;
double soucin = a * b;
double podil = a / b;
double modulo = a % b;

Console.WriteLine("Kalkulacka: ");

if (operace == "+")
{
    Console.WriteLine(soucet);
}
if (operace == "-")
{
    Console.WriteLine(rozdil);
}
if (operace == "*")
{
    Console.WriteLine(soucin);
}
if (operace == "/" && b != 0)
{
    Console.WriteLine(podil);
}
if (operace == "%" && b != 0)
{
    Console.WriteLine(modulo);
}