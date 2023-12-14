using Application.Extensions.UserContext;
using Application.Services.ExpenseService;
using Application.UseCases.Expense;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseController : ControllerBase
    {
        private readonly IExpenseService _expenseService;
        private readonly IUserContextService _userContextService;

        public ExpenseController(IExpenseService expenseService,
            IUserContextService userContextService)
        {
            _expenseService = expenseService;
            _userContextService = userContextService;
        }

        [HttpGet("expenses")]
        [Authorize]
        public async Task<ActionResult<List<ExpenseDto>>> GetExpensesByUserId()
        {
            var expenses = await _expenseService.GetExpensesByUserId(_userContextService.GetCurrentUserId());

            if (expenses == null)
                return NotFound();

            return Ok(expenses);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ExpenseDto>> GetExpense(int id)
        {
            var expense = await _expenseService.GetExpense(id);

            if (expense == null)
                return NotFound();

            return Ok(expense);
        }

        [HttpGet("{userId}/expense-by-user-id")]
        [Authorize]
        public async Task<ActionResult<List<ExpenseDto>>> GetExpenseByUserId(int id)
        {
            var expense = await _expenseService.GetExpenseByUserId(id, _userContextService.GetCurrentUserId());

            if (expense == null)
                return NotFound();

            return Ok(expense);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<ExpenseDto>> AddExpense(AddExpenseDto addExpenseDto)
        {
            var result = await _expenseService.AddExpense(addExpenseDto, _userContextService.GetCurrentUserId());

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<ExpenseDto>> UpdateExpense(UpdateExpenseDto updateExpenseDto, int id)
        {
            var result = await _expenseService.UpdateExpense(updateExpenseDto, id, _userContextService.GetCurrentUserId());

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<bool>> DeleteExpense(int id)
        {
            var result = await _expenseService.DeleteExpense(id, _userContextService.GetCurrentUserId());

            if (result != true)
                return NotFound();

            return Ok(result);
        }
    }
}
