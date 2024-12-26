
public class Node
{
    public string otazka { get; set; }
    public Node otazkaAno { get; set; }
    public Node otazkaNe { get; set; }

    public Node(string otazka)
    {
        this.otazka = otazka;
    }
}