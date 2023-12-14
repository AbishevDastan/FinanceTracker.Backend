using Domain.Abstractions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ExpenseRepository : IExpenseRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ExpenseRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Expense> GetExpenseByUserId(int expenseId, int userId)
        {
            return await _dbContext.Expenses
                .FirstOrDefaultAsync(e => e.Id == expenseId && e.UserId == userId);
        }

        public async Task<Expense> GetExpense(int expenseId)
        {
            return await _dbContext.Expenses
                .FirstOrDefaultAsync(e => e.Id == expenseId);
        }

        public async Task<List<Expense>> GetExpensesByUserId(int userId)
        {
            return await _dbContext.Expenses
                .Where(p => p.UserId == userId)
                .ToListAsync();
        }

        public async Task<Expense> AddExpense(Expense expense, int userId)
        {
            var newExpense = new Expense
            {
                Description = expense.Description,
                Amount = expense.Amount,
                Date = DateTimeOffset.UtcNow,
                UserId = userId
            };

            await _dbContext.Expenses.AddAsync(newExpense);
            await _dbContext.SaveChangesAsync();

            return newExpense;
        }

        public async Task<bool> DeleteExpense(int id)
        {
            _dbContext.Expenses.Remove(await GetExpense(id));
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<Expense> UpdateExpense(Expense expense, int id)
        {
            var dbExpense = await GetExpense(id);

            if (dbExpense != null)
            {
                dbExpense.Description = expense.Description;
                dbExpense.Date = expense.Date;
                dbExpense.Amount = expense.Amount;

                await _dbContext.SaveChangesAsync();

                return dbExpense;
            }
            throw new ArgumentNullException(nameof(expense));
        }
    }
}

