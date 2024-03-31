namespace SampleProjectDatabase.model;

public class Client(string name) : Entity<long>
{
    public string Name { set; get; } = name;

    public override string ToString()
    {
        return "Id: " + Id + " Nume: " + Name;
    }
}