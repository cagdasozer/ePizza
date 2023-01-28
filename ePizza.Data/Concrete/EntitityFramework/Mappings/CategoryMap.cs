using ePizza.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePizza.Data.Concrete.EntitityFramework.Mappings
{
	public class CategoryMap : IEntityTypeConfiguration<Category>
	{
		public void Configure(EntityTypeBuilder<Category> builder)
		{
			builder.HasKey(c => c.ID);
			builder.Property(c => c.ID).ValueGeneratedOnAdd();
			builder.Property(c => c.Name).IsRequired();
			builder.Property(c => c.Name).HasMaxLength(70);
			builder.Property(c => c.Description).HasMaxLength(500);
			builder.ToTable("Categories");

			builder.HasData(//varsayılan default değerler atadık bu değerler db oluştururken kategoride oluşacaktır. Bunu HasDate ile yapmaktayız.
				new Category { ID = 1, Name = "Pizza", Description = "Pizzalar" },
				new Category { ID = 2, Name = "Tatlı", Description = "Tatlılar" },
				new Category { ID = 3, Name = "İçecek", Description = "İçecekler" }
				);
		}
	}
}
