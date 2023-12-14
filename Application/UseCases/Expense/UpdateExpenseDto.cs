namespace Application.UseCases.Expense
{
    public class UpdateExpenseDto
    {
        public string Description { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public DateTimeOffset Date { get; set; }
    }
}
