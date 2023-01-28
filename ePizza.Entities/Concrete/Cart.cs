using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePizza.Entities.Concrete
{
	public class Cart
	{

		/// <summary>
		/// Sepet işlemleri olduğu entity...Burada constructor içerisinde CartItem o anın tarihi ve sepetin aktif olduğu nu belirtmek için default değerler verdik.
		/// </summary>
		public Cart() 
		{
			CartItems= new List<CartItem>();
			CreatedDate= DateTime.Now;
			IsACtive= true;
		}


		public Guid ID { get; set; }

		public int UserId { get; set; }

		public DateTime CreatedDate { get; set; }

		public virtual List<CartItem> CartItems { get; set; }

		public bool IsACtive { get; set; }
	}
}
