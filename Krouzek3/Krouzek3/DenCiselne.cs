/*
Console.WriteLine("Zadej den v tydnu (ciselne):");
string text_den = Console.ReadLine();
int den;
bool uspech = int.TryParse(text_den, out den);

if (!uspech || den < 1 || den > 7)
{
    Console.WriteLine("Toto neni cislo pro den v tydnu!");
    return;
}

Console.WriteLine("Den slovne: ");

switch(den) {
    case 1:
        Console.WriteLine("Pondeli");
        break;
    case 2:
        Console.WriteLine("Utery");
        break;
    case 3:
        Console.WriteLine("Streda");
        break;
    case 4:
        Console.WriteLine("Ctvrtek");
        break;
    case 5:
        Console.WriteLine("Patek");
        break;
    case 6:
        Console.WriteLine("Sobota");
        break;
    case 7:
        Console.WriteLine("Nedele");
        break;
}
*/