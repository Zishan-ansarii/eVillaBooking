namespace eVillaBooking.Application.Common.Interfaces
{
    public interface IUnitOfWork
    {
        public IVillaRepository VillaRepositoryUOW { get; }
        public IVillaNumberRepository VillaNumberRepositoryUOW { get; }
        public IAmenityRepository AmenityRepositoryUOW { get; }
        public IBookingRepository BookingRepositoryUOW { get; }
        public IApplicationUserRepository ApplicationUserRepositoryUOW { get; }

        void Save();

    }
}
