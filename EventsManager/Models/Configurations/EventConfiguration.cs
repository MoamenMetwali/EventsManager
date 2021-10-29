using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsManager.Models.Configurations
{
    public class EventConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.ToTable("Events");

            builder.Property(a => a.Id)
               .IsRequired();

            builder.Property(a => a.Title)
                .IsRequired();

            builder.Property(a => a.Description)
                            .IsRequired();

            builder.Property(a => a.UsersNo);

            builder.Property(a => a.Date)
                .HasColumnType("DateTime");

            builder.Property(a => a.Active).ValueGeneratedNever()
               .HasDefaultValue(true);

            builder.Property(a => a.Deleted)
                .HasDefaultValue(false);




        }
    }
}
