using DistriBindApi.Enums;
using DistriBindApi.Interfaces;
using DistriBindApi.Models;

namespace DistriBindApi.Strategies;

public class RentKeywordCategorization : IExpenseCategorizationStrategy
{
    public Category Categorize(Expense expense)
    {
        return expense.Description.ToLower().Contains("rent") || expense.Description.ToLower().Contains("lease") 
            ? Category.Rent
            : Category.Uncategorized;
    }
}