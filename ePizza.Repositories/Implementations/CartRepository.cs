using ePizza.Data.Concrete.EntitityFramework.Contexts;
using ePizza.Entities.Concrete;
using ePizza.Repositories.Interfaces;
using ePizza.Repositories.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePizza.Repositories.Implementations
{
	public class CartRepository : Repository<Cart>, ICartRepository
	{
		public ePizzaContext ePizzaContext
		{
			//context nesnesini burqada kapsule(encapsulation) ederek almış olduk...
			get
			{
				return _context as ePizzaContext;
			}
		}

		public CartRepository(DbContext context) : base(context)
		{

		}

		public Cart GetCart(Guid cartId)
		{
			return ePizzaContext.Carts.Include("Product").FirstOrDefault(x => x.ID == cartId && x.IsACtive == true);
		}

		public CartModel GetCartDetails(Guid cartId)
		{
			var model = (from cart in ePizzaContext.Carts
						 where cart.ID == cartId && cart.IsACtive == true
						 select new CartModel
						 {
							 Id = cart.ID,
							 UserId = cart.UserId,
							 CreatedDate = cart.CreatedDate,
							 Products = (from cartItem in ePizzaContext.CartItems
										 join item in ePizzaContext.Products
										 on cartItem.ProductId equals item.ID
										 where cartItem.CartId == cartId
										 select new ProductModel()
										 {
											 Id = cartItem.ID,
											 Name = item.Name,
											 Description = item.Description,
											 ImageUrl = item.ImageUrl,
											 Quantity = cartItem.Quantity,
											 ProductId = item.ID,
											 UnitPrice = cartItem.UnitPrice
										 }).ToList()

						 }).FirstOrDefault();
			return model;
		}

		public int DeleteItem(Guid cartId, int productId)
		{
			var item = ePizzaContext.CartItems.FirstOrDefault(c => c.CartId == cartId && c.ID == productId);
			if (item != null)
			{
				ePizzaContext.CartItems.Remove(item);
				return ePizzaContext.SaveChanges();
			}
			else
			{
				return 0;
			}
		}

		public int UpdateQuantity(Guid cartId, int productId, int quantity)
		{
			bool flag = false;
			var cart = GetCart(cartId); //seçilen ürünü getir.
			if (cart != null) // ürün var ise
			{
				for (int i = 0; i < cart.CartItems.Count; i++) //sepetin içindeki nesneleri dön 
				{
					if (cart.CartItems[i].ID == productId) //sepet içindeki ürün gelen ürünün id eşit ise
					{
						flag = true; // bayrağı true yap
						if (quantity < 0 && cart.CartItems[i].Quantity > 1) //miktar küçükse sıfırdan ve mevcut sepet büyükse 1 den 
							cart.CartItems[i].Quantity += (quantity); // mevcut sepete gelen miktari ekle
						else if (quantity > 0) // mevcut sepet büyük ise 0 dan
							cart.CartItems[i].Quantity += quantity; // gelen değeri mevcut sepete ekle
						break; // işi bitir.
					}
				}
				if (flag) // bayrak false sql'e kayıt et 
				{
					return ePizzaContext.SaveChanges();
				}
			}
			return 0; //return 0 döndür eğer ürün ya da sepet yoksa
		}

		public int UpdateToCart(Guid cartId, int userId)
		{
			Cart cart = GetCart(cartId);
			cart.UserId = userId;
			return ePizzaContext.SaveChanges();
		}
	}
}
