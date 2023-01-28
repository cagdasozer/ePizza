using ePizza.Entities.Concrete;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePizza.Data.Concrete.EntitityFramework.Mappings
{
	public class CartMap : IEntityTypeConfiguration<Cart>
	{
		public void Configure(EntityTypeBuilder<Cart> builder)
		{
			builder.HasKey(c => c.ID);
			builder.Property(c=>c.ID).ValueGeneratedOnAdd();
			builder.Property(c => c.UserId);
			builder.Property(c => c.CreatedDate).IsRequired();
			builder.Property(c => c.IsACtive);
			builder.ToTable("Carts");
		}
	}
}
