namespace CrudTest.Core.Context.Model;

public class Entity
{
    public long Id { get; set; }

    public DateTime Created { get; set; } = DateTime.UtcNow;
}