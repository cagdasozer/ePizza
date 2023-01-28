﻿using ePizza.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePizza.Data.Concrete.EntitityFramework.Mappings
{
	public class OrderItemMap : IEntityTypeConfiguration<OrderItem>
	{
		public void Configure(EntityTypeBuilder<OrderItem> builder)
		{
			builder.HasKey(o => o.ID);
			builder.Property(o => o.ID).ValueGeneratedOnAdd();
			builder.Property(o => o.ProductId);
			builder.Property(o => o.UnitPrice).IsRequired();
			builder.Property(o => o.Total).IsRequired();
			builder.ToTable("OrderItems");
		}
	}
}