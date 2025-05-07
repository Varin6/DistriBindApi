namespace DistriBindApi.Models;

public class Category : Entity
{
    public override int Id { get; set; }
    public string Name { get; set; }
    public ICollection<Expense> Expenses { get; set; }
}