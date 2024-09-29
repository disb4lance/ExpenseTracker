using Entities.Models;
using Shared.DataTransferObject;
using AutoMapper;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ExpenseTracker
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Category, CategoryDto>();

            CreateMap<Expense, ExpenseDto>();

            CreateMap<CategoryForCreationDto, Category>();

            CreateMap<ExpenseForCreationDto, Expense>();

            CreateMap<ExpenseForUpdateDto, Expense>().ReverseMap();

            CreateMap<CategoryForUpdateDto, Category>();

        }

    }
}
