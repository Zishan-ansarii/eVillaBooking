using eVillaBooking.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace eVillaBooking.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Villa> Villas { get; set; }
        public DbSet<VillaNumber> VillaNumbers { get; set; }
        public DbSet<Amenity> Amenities { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var seedData = new List<Villa>()
                               {
                                  new Villa(){
                                              Id = 1,
                                              Name = "Royal Villa",
                                              Description = "Fusce 11 tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.",
                                              ImageUrl = "https://placehold.co/600x400",
                                              Occupancy = 4,
                                              Price = 200,
                                              Sqft = 550,
                                          },
                                        new Villa()
                                        {
                                            Id = 2,
                                            Name = "Premium Pool Villa",
                                            Description = "Fusce 11 tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.",
                                            ImageUrl = "https://placehold.co/600x401",
                                            Occupancy = 4,
                                            Price = 300,
                                            Sqft = 550,
                                        },
                                        new Villa()
                                        {
                                            Id = 3,
                                            Name = "Luxury Pool Villa",
                                            Description = "Fusce 11 tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.",
                                            ImageUrl = "https://placehold.co/600x402",
                                            Occupancy = 4,
                                            Price = 400,
                                            Sqft = 750,
                                        }
                                  };
            modelBuilder.Entity<Villa>().HasData(seedData);

            modelBuilder.Entity<VillaNumber>().HasData(
                new VillaNumber()
                {
                    Villa_Id = 1,
                    Villa_Number = 101,
                },
                new VillaNumber()
                {
                    Villa_Id = 2,
                    Villa_Number = 201,
                },
                new VillaNumber()
                {
                    Villa_Id = 3,
                    Villa_Number = 301,
                });

            modelBuilder.Entity<Amenity>().HasData(
                new Amenity { Id = 1, Name = "Swimming Pool", VillaId = 1 },
                new Amenity { Id = 2, Name = "Gym", VillaId = 1 },
                new Amenity { Id = 3, Name = "Spa", VillaId = 1 },
                new Amenity { Id = 4, Name = "Wi-Fi", VillaId = 1 },

                new Amenity { Id = 5, Name = "Private Beach", VillaId = 2 },
                new Amenity { Id = 6, Name = "Yoga Room", VillaId = 2 },
                new Amenity { Id = 7, Name = "BBQ Area", VillaId = 2 },
                new Amenity { Id = 8, Name = "Sauna", VillaId = 2 },

                new Amenity { Id = 9, Name = "Hiking Trails", VillaId = 3 },
                new Amenity { Id = 10, Name = "Fireplace", VillaId = 3 },
                new Amenity { Id = 11, Name = "Game Room", VillaId = 3 },
                new Amenity { Id = 12, Name = "Hot Tub", VillaId = 3 }
);


            
        }
    }
}
