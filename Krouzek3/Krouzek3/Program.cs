// Vygeneruje cislo
int cislo = new Random(464646).Next(1, 100);

Console.WriteLine("Myslim si cislo, hadej:");

int pocetPokusu = 0;
bool uhadnul = false; // indikuje, zda hrac uhadnul
while (!uhadnul)
{ // dokud hrac neuhadnul
    // nactu cislo...
    string text_a = Console.ReadLine(); // precti text
    int a;
    bool uspech = int.TryParse(text_a, out a); // preved ho na cislo

    if (!uspech)
    { // nenacetl jsem cislo
        continue; // necham uzivatele zadat cislo znovu
    }

    pocetPokusu = pocetPokusu + 1;
    // porovnam cislo s vygenerovanym

    if (a == cislo)
    {
        Console.WriteLine("Uhadl jsi! Pocet pokusu:");
        Console.WriteLine(pocetPokusu);
        uhadnul = true;
    }
    if (a < cislo)
    {
        Console.WriteLine("Je to vice.");
    }
    if (a > cislo)
    {
        Console.WriteLine("Je to mene.");
    }
}

