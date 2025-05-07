﻿using DistriBindApi.Data;
using DistriBindApi.DTOs;
using DistriBindApi.Enums;
using DistriBindApi.Interfaces;
using DistriBindApi.Models;
using DistriBindApi.Strategies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DistriBindApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ExpensesController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IEnumerable<IExpenseCategorizationStrategy> _strategies;

    public ExpensesController(AppDbContext context, IEnumerable<IExpenseCategorizationStrategy> strategies)
    {
        _context = context;
        _strategies = strategies;
    }

    // GET: api/expenses
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Expense>>> GetExpenses()
    {
        return await _context.Expenses.ToListAsync();
    }

    // GET: api/expenses/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Expense>> GetExpense(int id)
    {
        var expense = await _context.Expenses.FirstOrDefaultAsync(e => e.Id == id);

        if (expense == null)
        {
            return NotFound();
        }

        return expense;
    }

    // POST: api/expenses
    [HttpPost]
    public async Task<ActionResult<Expense>> CreateExpense(Expense expense)
    {
        
        foreach (var strategy in _strategies)
        {
            var category = strategy.Categorize(expense);
            if (category != Category.Uncategorized)
            {
                expense.Category = category;
                break;
            }
        }
        
        _context.Expenses.Add(expense);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetExpense), new { id = expense.Id }, expense);
    }

    // PUT: api/expenses/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateExpense(int id, Expense expense)
    {
        if (id != expense.Id)
        {
            return BadRequest();
        }

        _context.Entry(expense).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Expenses.Any(e => e.Id == id))
            {
                return NotFound();
            }
            throw;
        }

        return NoContent();
    }

    // DELETE: api/expenses/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteExpense(int id)
    {
        var expense = await _context.Expenses.FindAsync(id);
        if (expense == null)
        {
            return NotFound();
        }

        _context.Expenses.Remove(expense);
        await _context.SaveChangesAsync();

        return NoContent();
    }
    
    
    [HttpGet("monthly-expenses/{userId}")]
    public async Task<ActionResult<IEnumerable<MonthlyExpenseDto>>> GetMonthlyExpenses(int userId)
    {
        var result = await _context.Expenses
            .Where(e => e.UserId == userId)
            .GroupBy(e => new { e.CreatedOn.Year, e.CreatedOn.Month })
            .Select(g => new MonthlyExpenseDto
            {
                Year = g.Key.Year,
                Month = g.Key.Month,
                TotalExpense = g.Sum(e => e.Amount)
            })
            .OrderBy(e => e.Year)
            .ThenBy(e => e.Month)
            .ToListAsync();

        return Ok(result);
    }
    
    
}