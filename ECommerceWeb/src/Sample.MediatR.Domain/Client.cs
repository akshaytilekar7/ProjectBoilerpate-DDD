namespace Sample.MediatR.Domain;

public class Client : BaseEntity
{
    public string Name { get; set; }
    public string Email { get; set; }
    public DateTime Date { get; set; }
}
