List<Student> seznamStudentu = new List<Student>();

while (true)
{
    try
    {
        Student student = Student.Nacti();
        seznamStudentu.Add(student);
    }
    catch (Exception e)
    {
        // Console.WriteLine(e.ToString());
        break;
    }
}

seznamStudentu.Sort();

foreach (Student student in seznamStudentu)
{
    Console.WriteLine(student);
}

/*
1,Jan Novák,j.novak@email.com,Školní 123;Praha
2,Petra Svobodová,p.svobodova@email.com,Zahradní 456;Brno
3,Martin Dvořák,m.dvorak@email.com,Novákova 789;Ostrava
4,Lucie Kovářová,l.kovarova@email.com,Lesní 234;Plzeň
5,Tomas Černý,t.cerny@email.com,Krátká 567;Hradec Králové
6,Kateřina Máchová,k.machova@email.com,Stromořadí 890;Olomouc
7,Petr Jaroš,p.jaros@email.com,Jarní 345;Liberec
8,Alena Tomanová,a.tomanova@email.com,Sady 678;Pardubice
9,Marek Polák,m.polak@email.com,Centrum 234;Zlín
10,Veronika Holečková,v-holeckova@email.com,Sportovní 123;Ústí nad Labem
*/