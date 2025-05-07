using DistriBindApi.Enums;
using DistriBindApi.Models;

namespace DistriBindApi.Interfaces;

public interface IExpenseCategorizationStrategy
{
    Category Categorize(Expense expense);
}