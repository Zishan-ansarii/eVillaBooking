using eVillaBooking.Application.Common.Interfaces;
using eVillaBooking.Domain.Entities;
using eVillaBooking.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace eVillaBooking.Infrastructure.Repository
{
    public class VillaRepository : Repository<Villa> ,IVillaRepository
    {
        private readonly ApplicationDbContext _db;
        public VillaRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Villa villa)
        {
            _db.Villas.Update(villa);
        }

        public void save()
        {
            _db.SaveChanges();
        }

    }
}
