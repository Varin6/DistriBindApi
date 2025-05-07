using DistriBindApi.Enums;
using DistriBindApi.Interfaces;

namespace DistriBindApi.Models;

public class Expense : Entity
{
    public override int Id { get; set; }
    public decimal Amount { get; set; }
    public Category Category { get; set; }
    public int UserId { get; set; }
    
    public string Description { get; set; }
    
}