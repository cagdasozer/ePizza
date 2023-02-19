using AutoMapper;
using ePizza.Entities.Concrete;
using ePizza.Entities.Dtos.Categories;
using ePizza.Repositories.Implementations;
using ePizza.Repositories.Interfaces;
using ePizza.Services.Interfaces;
using ePizza.Shared.Utilities.Results.Abstract;
using ePizza.Shared.Utilities.Results.ComplexType;
using ePizza.Shared.Utilities.Results.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePizza.Services.Implemantations
{
    public class CategoryManager : ICategoryService
	{
		private readonly ICategoryRepository _categoryRepository;
		public readonly IMapper _mapper;

		public CategoryManager(ICategoryRepository categoryRepository, IMapper mapper)
		{
			_categoryRepository = categoryRepository;
			_mapper = mapper;
		}

		public async Task<IDataResult<CategoryDto>> AddAsync(CategoryAddDto categoryAddDto)
		{
			var category = _mapper.Map<Category>(categoryAddDto);
			var categoryAdded = await _categoryRepository.AddAsync(category);
			return new DataResult<CategoryDto>(ResultStatus.Success, "Category Added", new CategoryDto
			{
				Category = categoryAdded,
				Message = "Category Added",
				ResutStatus = ResultStatus.Success,
			});
		}

		public  async Task<IDataResult<CategoryDto>> DeleteAsync(int categoryId)
		{
			var category = await _categoryRepository.GetAsync(x=>x.ID== categoryId);
			if (category!= null)
			{
				var deletedCategory = await _categoryRepository.DeleteAsync(category);
				await _categoryRepository.SaveAsync();
				return new DataResult<CategoryDto>(ResultStatus.Success, "Category is Deleted", new CategoryDto
				{
					Category = deletedCategory,
					Message = "Category is Deleted",
					ResutStatus = ResultStatus.Success
				});
			}
			else
			{
				return new DataResult<CategoryDto>(ResultStatus.Error, "Category Not Found", new CategoryDto
				{
					Category = null,
					Message = "Category Not Found",
					ResutStatus = ResultStatus.Error
				});
			}
		}

		public async Task<IDataResult<CategoryListDto>> GetAllAsync()
		{
			var category = await _categoryRepository.GetAllAsync();
			if (category != null)
			{
				return new DataResult<CategoryListDto>(ResultStatus.Success, new CategoryListDto
				{
					Categories = category
				});
			}
			else
			{
				return new DataResult<CategoryListDto>(ResultStatus.Error, "Category Not Found", new CategoryListDto
				{
					Categories = null,
					Message = "Category Not Found",
					ResutStatus = ResultStatus.Error
				});
			}
		}

		public async Task<IDataResult<CategoryDto>> GetAsync(int categoryId)
		{
			var category = await _categoryRepository.GetAsync(x => x.ID == categoryId);
			if (category != null)
			{
				return new DataResult<CategoryDto>(ResultStatus.Success, new CategoryDto
				{
					Category = category,
					ResutStatus = ResultStatus.Success
				});
			}
			else
			{
				return new DataResult<CategoryDto>(ResultStatus.Error, "Category Not Found", new CategoryDto
				{
					Category = null,
					Message = "Category Not Found", //Error Messageler dinamik olmalıdır.
					ResutStatus = ResultStatus.Error
				});
			}
		}

		public async Task<IDataResult<CategoryDto>> UpdateAsync(CategoryUpdateDto categoryUpdateDto)
		{
			var oldCategory = await _categoryRepository.GetAsync(x => x.ID == categoryUpdateDto.Id);
			var category = _mapper.Map<CategoryUpdateDto,Category>(categoryUpdateDto, oldCategory);
			var updatedCategory = await _categoryRepository.UpdateAsync(category);
			await _categoryRepository.SaveAsync();

			return new DataResult<CategoryDto>(ResultStatus.Success, new CategoryDto
			{
				Category = updatedCategory,
				Message = "Category Update", // Error Messageler dinamik olmalıdır.
				ResutStatus = ResultStatus.Success
			});
		}
	}
}
