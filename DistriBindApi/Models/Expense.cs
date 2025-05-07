using DistriBindApi.Interfaces;

namespace DistriBindApi.Models;

public class Expense : Entity
{
    public override int Id { get; set; }
    public decimal Amount { get; set; }
    public DateTime Timestamp { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; }
    public int UserId { get; set; }
    
}