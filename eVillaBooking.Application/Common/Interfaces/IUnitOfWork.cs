using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eVillaBooking.Application.Common.Interfaces
{
    public interface IUnitOfWork
    {
        public IVillaRepository VillaRepositoryUOW { get; }
        public IVillaNumberRepository VillaNumberRepositoryUOW { get; }
        public IAmenityRepository AmenityRepositoryUOW { get; }
        public IBookingRepository BookingRepositoryUOW { get; }

        void Save();

    }
}
