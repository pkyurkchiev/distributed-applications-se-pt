using GustoHub.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace GustoHub.Data.Configurations
{
    public class EmployeeUserEntityConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                .HasOne(u => u.Employee)
                .WithOne(e => e.User)
                .HasForeignKey<User>(u => u.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
