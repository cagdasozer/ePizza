using ePizza.Entities.Dtos.Categories;
using ePizza.Shared.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePizza.Services.Interfaces
{
    public interface ICategoryService
	{
		Task<IDataResult<CategoryListDto>> GetAllAsync();

		Task<IDataResult<CategoryDto>> GetAsync(int categoryId);

		Task<IDataResult<CategoryDto>> AddAsync(CategoryAddDto categoryAddDto);

		Task<IDataResult<CategoryDto>> UpdateAsync(CategoryUpdateDto categoryUpdateDto);

		Task<IDataResult<CategoryDto>> DeleteAsync(int categoryId);
	}
}
