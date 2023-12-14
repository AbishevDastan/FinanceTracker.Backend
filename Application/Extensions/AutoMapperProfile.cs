using Application.UseCases.Category;
using Application.UseCases.Expense;
using Application.UseCases.User;
using AutoMapper;
using Domain.Entities;

namespace Application.Extensions
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, AuthenticateUserDto>();
            CreateMap<AuthenticateUserDto, User>();
            CreateMap<User, RegisterUserDto>();
            CreateMap<RegisterUserDto, User>();
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();

            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryDto, Category>();
            CreateMap<Category, AddCategoryDto>();
            CreateMap<AddCategoryDto, Category>();
            CreateMap<Category, UpdateCategoryDto>();
            CreateMap<UpdateCategoryDto, Category>();

            CreateMap<Expense, ExpenseDto>();
            CreateMap<ExpenseDto, Expense>();
            CreateMap<Expense, AddExpenseDto>();
            CreateMap<AddExpenseDto, Expense>();
            CreateMap<Expense, UpdateExpenseDto>();
            CreateMap<UpdateExpenseDto, Expense>();
        }
    }
}
