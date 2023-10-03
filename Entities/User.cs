namespace Contabilizacao.Entities;

public class User
{
    public User(string idn, string name)
    {
        Idn = idn;
        Name = name;
    }
    public string Idn { get; set; }
    public string Name { get; set; }
    public List<Event>? Events { get; set; }
}