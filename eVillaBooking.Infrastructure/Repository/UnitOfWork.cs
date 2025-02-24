using eVillaBooking.Application.Common.Interfaces;
using eVillaBooking.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eVillaBooking.Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;
        public IVillaRepository VillaRepositoryUOW { get; private set; }
        public IVillaNumberRepository VillaNumberRepositoryUOW { get; private set; }

        public IAmenityRepository AmenityRepositoryUOW { get; private set; }

        public IBookingRepository BookingRepositoryUOW { get; private set; }

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            this.VillaRepositoryUOW = new VillaRepository(_db);
            this.VillaNumberRepositoryUOW = new VillaNumberRepository(_db);
            this.AmenityRepositoryUOW = new AmenityRepository(_db);
            this.BookingRepositoryUOW = new BookingRepository(_db);
        }
        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
