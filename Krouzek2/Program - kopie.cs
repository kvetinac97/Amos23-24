using static Calculator;

Calc();

Console.WriteLine("Zadej stranu a:");
string text_a = Console.ReadLine();
double a;
bool uspech = double.TryParse(text_a, out a);

if (!uspech || a <= 0)
{
    Console.WriteLine("Toto neni strana obdelnika!");
    return;
}

Console.WriteLine("Zadej stranu b:");
string text_b = Console.ReadLine();
double b;
uspech = double.TryParse(text_b, out b);

if (!uspech || b <= 0)
{
    Console.WriteLine("Toto neni strana obdelnika!");
    return;
}

double soucin = a * b;

Console.Write("Obdelnik ma obsah: ");
Console.WriteLine(soucin);