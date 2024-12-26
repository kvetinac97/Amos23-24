/*string bludisteText = "XXXXX\r\n" +
                  "X X@X\r\n" +
                  "X   X\r\n" +
                  "X.  X\r\n" +
                  "XXXXX\r\n";*/

/*List<List<char>> bludiste = new List<List<char>>()
{
    new List<char>() { 'X', 'X', 'X', 'X', 'X' },
    new List<char>() { 'X', ' ', 'X', '@', 'X' },
    new List<char>() { 'X', ' ', ' ', ' ', 'X' },
    new List<char>() { 'X', '.', ' ', ' ', 'X' },
    new List<char>() { 'X', 'X', 'X', 'X', 'X' },
};*/

using System.Timers;

string BludisteNaText(List<List<char>> bludiste)
{
    string text = "";
    for (int i = 0; i < bludiste.Count; i++)
    {
        for (int j = 0; j < bludiste[i].Count; j++)
        {
            text += bludiste[i][j];
        }
        text += "\n";
    }
    return text;
}

List<List<char>> TextNaBludiste(string text)
{
    List<List<char>> bludiste = new List<List<char>>();
    string[] lines = text.Split("\r\n");
    for (int i = 0; i < lines.Length; i++)
    {
        List<char> line = new List<char>();
        for (int j = 0; j < lines[i].Length; j++) {
            line.Add(lines[i][j]);
        }
        if (line.Count <= 0)
        {
            continue;
        }
        bludiste.Add(line);
    }
    return bludiste;
}

bool KontrolaBludiste(List<List<char>> bludiste)
{
    List<char> prvniRadek = bludiste[0];
    for (int i = 0; i < prvniRadek.Count; ++i)
        if (prvniRadek[i] != 'X')
            return false;

    List<char> posledniRadek = bludiste[bludiste.Count - 1];
    for (int i = 0; i < posledniRadek.Count; ++i)
        if (posledniRadek[i] != 'X')
            return false;

    for (int i = 0; i < bludiste.Count; ++i)
        if (bludiste[i][0] != 'X' || bludiste[i].Count != prvniRadek.Count ||
            bludiste[i][prvniRadek.Count - 1] != 'X')
            return false;

    int pocetZacatku = 0, pocetCilu = 0;

    for (int i = 0; i < bludiste.Count; ++i)
        for (int j = 0; j < bludiste[i].Count; ++j)
        {
            if (bludiste[i][j] != 'X' && bludiste[i][j] != ' '
                && bludiste[i][j] != ' ' &&
                bludiste[i][j] != '@' && bludiste[i][j] != '.')
                return false;

            if (bludiste[i][j] == '.')
                pocetZacatku++;
            if (bludiste[i][j] == '@')
                pocetCilu++;
        }

    if (pocetZacatku != 1 || pocetCilu != 1)
        return false;

    return true;
}

Position ZacatekBludiste(List<List<char>> bludiste)
{
    Position position = new Position();
    for (int i = 0; i < bludiste.Count; i++)
        for (int j = 0; j < bludiste[i].Count; j++)
        {
            if (bludiste[i][j] == '.')
            {
                position.x = j;
                position.y = i;
                return position;
            }
        }
    return position;
}

string textSouboru = File.ReadAllText("bludiste.txt");

List<List<char>> bludisteP = TextNaBludiste(textSouboru);
if (!KontrolaBludiste(bludisteP))
{
    Console.WriteLine("Neplatne bludiste!");
    return;
}

void PrekresliObrazovku(object? source, ElapsedEventArgs? e)
{
    Console.SetCursorPosition(0, 0);
    Console.CursorVisible = false;
    for (int y = 0; y < Console.WindowHeight; y++)
        Console.Write(new String(' ', Console.WindowWidth));
    Console.SetCursorPosition(0, 0);
    Console.CursorVisible = true;

    Console.WriteLine("Vyres bludiste. Pohybujes se sipkami:");
    Console.Write(BludisteNaText(bludisteP));
}

Position poloha = ZacatekBludiste(bludisteP);

System.Timers.Timer aTimer = new System.Timers.Timer();
aTimer.Elapsed += new ElapsedEventHandler(PrekresliObrazovku);
aTimer.Interval = 20;
aTimer.AutoReset = true;
aTimer.Enabled = true;

Queue<Position> chciNavstivit = new Queue<Position>();
poloha.previous = null;
chciNavstivit.Enqueue(poloha);

HashSet<Tuple<int, int>> been = new HashSet<Tuple<int, int>>();

while (chciNavstivit.Count > 0)
{
    Position position = chciNavstivit.Dequeue();
    if (been.Contains(new Tuple<int, int>(position.x, position.y))) continue;

    if (!aTimer.Enabled)
        break;

    if (bludisteP[position.y][position.x] == '@')
    {
        Position polohax = position;
        int length = 0;
        while (polohax.previous != null)
        {
            Position dalsi = polohax.previous;
            char znak;
            if (polohax.x == dalsi.x)
            {
                if (polohax.y < dalsi.y)
                    znak = '^';
                else
                    znak = 'v';
            }
            else
            {
                if (polohax.x < dalsi.x)
                    znak = '<';
                else
                    znak = '>';
            }
            bludisteP[dalsi.y][dalsi.x] = znak;

            polohax = dalsi;
            length++;
        }

        aTimer.Enabled = false;
        PrekresliObrazovku(null, null);
        Console.WriteLine($"Vyhra! Delka cesty: {length}");

        return;
    }
    been.Add(new Tuple<int, int>(position.x, position.y));

    Position vlevo = new Position();
    vlevo.x = position.x - 1;
    vlevo.y = position.y;
    vlevo.previous = position;

    if (bludisteP[vlevo.y][vlevo.x] != 'X')
        chciNavstivit.Enqueue(vlevo);

    Position vpravo = new Position();
    vpravo.x = position.x + 1;
    vpravo.y = position.y;
    vpravo.previous = position;

    if (bludisteP[vpravo.y][vpravo.x] != 'X')
        chciNavstivit.Enqueue(vpravo);

    Position nahoru = new Position();
    nahoru.x = position.x;
    nahoru.y = position.y - 1;
    nahoru.previous = position;

    if (bludisteP[nahoru.y][nahoru.x] != 'X')
        chciNavstivit.Enqueue(nahoru);

    Position dolu = new Position();
    dolu.x = position.x;
    dolu.y = position.y + 1;
    dolu.previous = position;

    if (bludisteP[dolu.y][dolu.x] != 'X')
        chciNavstivit.Enqueue(dolu);
}

Console.ReadKey();

/*
while (true)
{
    ConsoleKeyInfo stisknutaKlavesa = Console.ReadKey();
    int posunI = 0, posunJ = 0;
    switch (stisknutaKlavesa.Key)
    {
        case ConsoleKey.DownArrow:
            posunI = 1;
            break;
        case ConsoleKey.UpArrow:
            posunI = -1;
            break;
        case ConsoleKey.RightArrow:
            posunJ = 1;
            break;
        case ConsoleKey.LeftArrow:
            posunJ = -1;
            break;
        case ConsoleKey.Escape:
            Console.WriteLine("Vzdal jsi to? L.");
            return;
        default:
            Console.WriteLine("Spatna klavesa, stiskni znovu!");
            continue;
    }
    Position novaPoloha = new Position();
    novaPoloha.x = poloha.x + posunJ;
    novaPoloha.y = poloha.y + posunI;

    char znak = bludisteP[novaPoloha.y][novaPoloha.x];
    if (znak == 'X')
    {
        continue;
    }
    if (znak == '@')
    {
        Console.WriteLine("Gratuluji! Vyhral jsi.");
        aTimer.Enabled = false;
        return;
    }

    bludisteP[novaPoloha.y][novaPoloha.x] = '.';
    bludisteP[poloha.y][poloha.x] = ' ';
    poloha = novaPoloha;
}
*/