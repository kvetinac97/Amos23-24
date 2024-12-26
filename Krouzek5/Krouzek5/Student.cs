class Student : IComparable<Student>
{
    public int id;
    public string name;
    public string email;
    public Address address;

    public Student(int id, string name, string email, Address address)
    {
        this.id = id;
        this.name = name;
        this.email = email;
        this.address = address;
    }

    public static Student Nacti()
    {
        string line = Console.ReadLine();
        string[] information = line.Split(",");
        string idString = information[0];
        int id = int.Parse(idString);
        string name = information[1];
        string email = information[2];

        string addressString = information[3];
        return new Student(id, name, email, new Address(addressString));
    }

    public int CompareTo(Student student)
    {
        return email.CompareTo(student.email);
    }

    override public string ToString()
    {
        return "Student(id=" + id + ", name=" + name +
            ", email=" + email + ", address=" + address + ")";
    }
}
