using AutoMapper;
using ePizza.Entities.Concrete;
using ePizza.Entities.Dtos.Products;
using ePizza.Repositories.Interfaces;
using ePizza.Services.Interfaces;
using ePizza.Shared.Utilities.Results.Abstract;
using ePizza.Shared.Utilities.Results.ComplexType;
using ePizza.Shared.Utilities.Results.Concrete;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ePizza.Services.Implemantations
{
    public class ProductManager : IProductService
	{
		private readonly IProductRepository _productRepository;
		public readonly IMapper _mapper;

		public ProductManager(IProductRepository productRepository, IMapper mapper)
		{
			_productRepository = productRepository;
			_mapper = mapper;
		}

		public async Task<IDataResult<ProductDto>> AddAsync(ProductAddDto productAddDto)
		{
			var product = _mapper.Map<Product>(productAddDto);//öncellikle gelen datayı dto olarak maplememiz gerekmektedir. Aksi halde halde ananymous type olarak giden data hata çözülmeyecektir.
			var productAdded = await _productRepository.AddAsync(product);
			return new DataResult<ProductDto>(ResultStatus.Success, "Product Added", new ProductDto
			{
				Product = productAdded,
				Message = "Product Added",
				ResutStatus = ResultStatus.Success,
			});
		}

		public async Task<IDataResult<ProductDto>> DeleteAsync(int productId)
		{
			var product = await _productRepository.GetAsync(x => x.ID == productId);
			if (product != null)
			{
				var deletedProduct = await _productRepository.DeleteAsync(product);
				await _productRepository.SaveAsync();
				return new DataResult<ProductDto>(ResultStatus.Success, "Product is Deleted", new ProductDto
				{
					Product = deletedProduct,
					Message = "Product is Deleted",
					ResutStatus = ResultStatus.Success
				});
			}
			else
			{
				return new DataResult<ProductDto>(ResultStatus.Error, "Product Not Found", new ProductDto
				{
					Product = null,
					Message = "Product Not Found",
					ResutStatus = ResultStatus.Error
				});
			}
		}

		public async Task<IDataResult<ProductListDto>> GetAllAsync()
		{
			var product = await _productRepository.GetAllAsync();
			if (product != null)
			{
				return new DataResult<ProductListDto>(ResultStatus.Success, new ProductListDto
				{
					Products = product
				});
			}
			else
			{
				return new DataResult<ProductListDto>(ResultStatus.Error, "Product Not Found", new ProductListDto
				{
					Products = null,
					Message = "Product Not Found",
					ResutStatus = ResultStatus.Error
				});
			}
		}

		public async Task<IDataResult<ProductDto>> GetAsync(int productId)
		{
			var product = await _productRepository.GetAsync(x => x.ID == productId);
			if (product != null)
			{
				return new DataResult<ProductDto>(ResultStatus.Success, new ProductDto
				{
					Product = product,
					ResutStatus = ResultStatus.Success
				});
			}
			else
			{
				return new DataResult<ProductDto>(ResultStatus.Error, "Product Not Found", new ProductDto
				{
					Product = null,
					Message = "Product Not Found", //Error Messageler dinamik olmalıdır.
					ResutStatus = ResultStatus.Error
				});
			}
		}

		public async Task<IDataResult<ProductDto>> UpdateAsync(ProductUpdateDto productUpdateDto)
		{
			var oldProduct = await _productRepository.GetAsync(x => x.ID == productUpdateDto.Id);
			var product = _mapper.Map<ProductUpdateDto, Product>(productUpdateDto, oldProduct);
			var updatedProduct = await _productRepository.UpdateAsync(product);
			await _productRepository.SaveAsync();

			return new DataResult<ProductDto>(ResultStatus.Success, new ProductDto
			{
				Product = updatedProduct,
				Message = "Product Update", // Error Messageler dinamik olmalıdır.
				ResutStatus = ResultStatus.Success
			});
		}
	}
}
