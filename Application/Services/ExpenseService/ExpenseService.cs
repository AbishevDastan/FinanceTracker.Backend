using Application.UseCases.Expense;
using AutoMapper;
using Domain.Abstractions;
using Domain.Entities;

namespace Application.Services.ExpenseService
{
    public class ExpenseService : IExpenseService
    {
        private readonly IExpenseRepository _expenseRepository;
        private readonly IMapper _mapper;

        public ExpenseService(IExpenseRepository expenseRepository,
            IMapper mapper)
        {
            _expenseRepository = expenseRepository;
            _mapper = mapper;
        }

        public async Task<ExpenseDto> GetExpense(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid expense ID.", nameof(id));

            var expense = await _expenseRepository.GetExpense(id);

            if (expense == null)
                throw new Exception($"Expense with ID {id} not found");

            return _mapper.Map<ExpenseDto>(expense);
        }

        public async Task<ExpenseDto> GetExpenseByUserId(int id, int userId)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid expense ID.", nameof(id));

            var expense = await _expenseRepository.GetExpenseByUserId(id, userId);

            if (expense == null)
                throw new Exception($"Expense with ID {id} not found");

            return _mapper.Map<ExpenseDto>(expense);
        }

        public async Task<List<ExpenseDto>> GetExpensesByUserId(int userId)
        {
            var expenses = await _expenseRepository.GetExpensesByUserId(userId);

            if (expenses == null || expenses.Count <= 0)
                throw new Exception("Expenses not found");

            return _mapper.Map<List<ExpenseDto>>(expenses);
        }

        public async Task<ExpenseDto> AddExpense(AddExpenseDto addExpenseDto, int userId)
        {
            if (addExpenseDto == null)
                throw new ArgumentNullException(nameof(addExpenseDto), "Expense cannot be null.");

            var addedExpense = await _expenseRepository.AddExpense(_mapper.Map<Expense>(addExpenseDto), userId);

            return _mapper.Map<ExpenseDto>(addedExpense);
        }

        public async Task<bool> DeleteExpense(int id, int userId)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid expense ID.", nameof(id));

            var expense = await GetExpense(id);

            if (expense == null)
                throw new Exception("Expense not found");

            if (expense.UserId != userId)
                throw new Exception("Access denied. You are not the expense author.");

            await _expenseRepository.DeleteExpense(id);
            return true;
        }

        public async Task<ExpenseDto> UpdateExpense(UpdateExpenseDto updateExpenseDto, int id, int userId)
        {
            var expense = await GetExpense(id);

            if (expense.UserId == userId)
            {
                if (updateExpenseDto == null)
                    throw new ArgumentNullException(nameof(updateExpenseDto), "Expense cannot be null.");

                var updatedExpense = await _expenseRepository.UpdateExpense(_mapper.Map<Expense>(updateExpenseDto), id);

                return _mapper.Map<ExpenseDto>(updatedExpense);
            }
            throw new Exception("Access denied. You must be the author to edit an expense.");
        }
    }
}
