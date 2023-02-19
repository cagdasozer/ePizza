using ePizza.Data.Concrete.EntitityFramework.Contexts;
using ePizza.Entities.Concrete;
using ePizza.Repositories.Implementations;
using ePizza.Repositories.Interfaces;
using ePizza.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePizza.Services.Configuration
{
	public class ConfigureRepositories
	{
		public static void AddService(IServiceCollection services, IConfiguration configuration)
		{
			services.AddDbContext<ePizzaContext>((option) =>
			{
				option.UseSqlServer(configuration.GetConnectionString("DbConnection"));
			});

			services.AddIdentity<User, Role>(option =>
			{
				option.Password.RequiredLength = 6;
				option.Password.RequireDigit = false; //sayısal değer
				option.Password.RequiredUniqueChars = 0; //birbirinden farklı
				option.Password.RequireNonAlphanumeric = false; // ya alfabe ya özel karakter olucak 
				option.Password.RequireLowercase = false; //küçük harf
				option.Password.RequireUppercase = false; //büyük harf

				//User Username and email options
				option.User.AllowedUserNameCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789._-@+";
				option.User.RequireUniqueEmail = true;
			}).AddEntityFrameworkStores<ePizzaContext>();

			services.AddScoped<DbContext, ePizzaContext>();
			services.AddTransient<IOrderRepository, OrderRepository>();
			services.AddTransient<ICartRepository, CartRepository>();
			services.AddTransient<IProductRepository, ProductRepository>();
			services.AddTransient<IProductTypeRepository, ProductTypeRepository>();
			services.AddTransient<ICategoryRepository, CategoryRepository>();


			services.AddTransient<IRepository<Product>,Repository<Product>>();
			services.AddTransient<IRepository<Category>, Repository<Category>>();
			services.AddTransient<IRepository<ProductType>, Repository<ProductType>>();
			services.AddTransient<IRepository<CartItem>, Repository<CartItem>>();
			services.AddTransient<IRepository<OrderItem>, Repository<OrderItem>>();
			services.AddTransient<IRepository<PaymentDetails>, Repository<PaymentDetails>>();
		}
		//AddTransient : Yukarıdaki nesneleri her kullanımında tekrar çağırma işlemini yapar tekrar tekrar her istekte nesne oluşur kısacası.
	}
}
