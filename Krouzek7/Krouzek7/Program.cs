List<string> postavy = new List<string>{"Statečný rytíř", "Vychytralý zloděj", "Odvážná lovkyně",
    "Nepoučitelný diplomat", "Tichý zabiják", "Šikovná bylinkářka", "Neohrožená vůdkyně"};
List<string> mista = new List<string> { "v temném lese.", "na hradě Hukvaldy.", "v jeskyni Šipka",
    "v opuštěné zombie vesnici.", "v Karibiku.", "v podzemním bludišti.", "v ohnivém jezeře."};
List<string> predmety = new List<string> { "ostrý meč", "tajemný svitek", "kouzelný lektvar",
    "začarovaný amulet", "pevný štít", "zrcadlo pravdy", "gumový míč", "plášť neviditelnosti",
    "prsten moci", "tajemný krystal", "knihu moudrosti", "stará hůl", "hranolka z mekáče"};
List<string> slovesa = new List<string>
{
    "našel/la", "běžel/a", "objevil/a", "políbil/a", "zabil/a", "vyvolal/a", "odemkl/a",
    "uviděl/a", "zapálil/a", "ukradl/a", "rozluštil/a"
};

Random generator = new Random();

void VypisZeSeznamu(List<string> seznam)
{
    Console.Write(seznam[generator.Next(0, seznam.Count)] + " ");
}


while (true)
{
    VypisZeSeznamu(postavy);
    VypisZeSeznamu(slovesa);
    VypisZeSeznamu(predmety);
    VypisZeSeznamu(mista);
    Console.WriteLine();

    Console.ReadKey();
}
