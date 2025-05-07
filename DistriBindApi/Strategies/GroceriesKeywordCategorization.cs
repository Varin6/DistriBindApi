using DistriBindApi.Enums;
using DistriBindApi.Interfaces;
using DistriBindApi.Models;

namespace DistriBindApi.Strategies;

public class GroceriesKeywordCategorization : IExpenseCategorizationStrategy
{
    public Category Categorize(Expense expense)
    {
        return expense.Description.ToLower().Contains("grocery") || expense.Description.ToLower().Contains("supermarket") 
            ? Category.Groceries
            : Category.Uncategorized;
    }
}