﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePizza.Entities.Concrete
{
	public class Category
	{
		public int ID { get; set; }

		public string Name { get; set; }

		public string Description { get; set; }

		public List<Product> Product { get; set; } // bir kategorinin birden fazla ürünü olur...
	}
}
