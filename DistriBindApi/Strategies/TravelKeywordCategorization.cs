using DistriBindApi.Enums;
using DistriBindApi.Interfaces;
using DistriBindApi.Models;

namespace DistriBindApi.Strategies;

public class TravelKeywordCategorization : IExpenseCategorizationStrategy
{
    public Category Categorize(Expense expense)
    {
        return expense.Description.ToLower().Contains("flight") || expense.Description.ToLower().Contains("hotel") 
            ? Category.Travel 
            : Category.Uncategorized;
    }
}