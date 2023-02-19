using ePizza.Entities.Dtos.ProductTypes;
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
    public class ProductTypeManager : IProductTypeService
	{
		private readonly IProductTypeRepository _productTypeRepository;

		public ProductTypeManager(IProductTypeRepository productTypeRepository)
		{
			_productTypeRepository = productTypeRepository;
		}

		public async Task<IDataResult<ProductTypeListDto>> GetAllAsync()
		{
			var productTypes = await _productTypeRepository.GetAllAsync();
			if (productTypes!=null)
			{
				return new DataResult<ProductTypeListDto>(ResultStatus.Success, new ProductTypeListDto
				{
					ProductTypes = productTypes,
					ResutStatus = ResultStatus.Success,
				});
			}
			else
			{
				return new DataResult<ProductTypeListDto>(ResultStatus.Error,"Product Type Not Found",new ProductTypeListDto
				{
					ProductTypes = null,
					ResutStatus = ResultStatus.Error,
					Message = "Product Not Found"
				});
			}
		}
	}
}
