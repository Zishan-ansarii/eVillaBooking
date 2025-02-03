using eVillaBooking.Application.Common.Interfaces;
using eVillaBooking.Domain.Entities;
using eVillaBooking.Infrastructure.Data;

namespace eVillaBooking.Infrastructure.Repository
{
    public class AmenityRepository : Repository<Amenity>, IAmenityRepository
    {
        private readonly ApplicationDbContext _db;
        public AmenityRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Amenity amenity)
        {
            _db.Update(amenity);
        }
    }
}
