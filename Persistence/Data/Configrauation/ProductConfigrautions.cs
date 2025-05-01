using Domain.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Data.Configrauation
{
    internal class ProductConfigrautions : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasOne(P => P.productBrand).WithMany()
                .HasForeignKey(p => p.BrandId);

            builder.HasOne(P => P.productType).WithMany()
                .HasForeignKey(p => p.TypeId);


        }


    }
}
