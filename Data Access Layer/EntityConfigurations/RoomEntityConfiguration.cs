using Entity_Layer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.EntityConfigurations
{
    public class RoomEntityConfiguration : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> entity)
        {
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Id).IsRequired().HasMaxLength(10);
            entity.HasData(
                new Room("A-101"), new Room("A-102"), new Room("A-103"), new Room("A-104"),
                new Room("B-101"), new Room("B-102"), new Room("B-103"), new Room("B-104"),
                new Room("C-101"), new Room("C-102"), new Room("C-103"), new Room("C-104"),
                new Room("D-101"), new Room("D-102"), new Room("D-103"), new Room("D-104")
            );
        }
    }
}
