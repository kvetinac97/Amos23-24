class Address
{
    string street;
    int cp;
    string city;

    public Address(string street, int cp, string city)
    {
        this.street = street;
        this.cp = cp;
        this.city = city;
    }

    public Address(string addressString)
    {
        string[] addressInformation = addressString.Split(";");
        string streetCp = addressInformation[0];
        this.city = addressInformation[1];

        string[] streetCpInformation = streetCp.Split(" ");
        this.street = streetCpInformation[0];
        string cpString = streetCpInformation[1];
        this.cp = int.Parse(cpString);
    }

    public override string ToString()
    {
        return "Address(street=" + street + ", cp=" + cp + ", city=" + city + ")";
    }
}
