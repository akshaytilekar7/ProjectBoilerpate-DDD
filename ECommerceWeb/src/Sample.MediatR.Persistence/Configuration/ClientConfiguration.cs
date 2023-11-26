using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Sample.MediatR.Persistence.Configuration
{
    public class ClientConfiguration : IEntityTypeConfiguration<Domain.Client>
    {
        public void Configure(EntityTypeBuilder<Domain.Client> builder)
        {
            builder.ToTable("Client");
        }
    }
}
