using eVillaBooking.Domain.Entities;

namespace eVillaBooking.Application.Common.Interfaces
{
    public interface IVillaNumberRepository :IRepository<VillaNumber>
    {
        void Save();
        void Update(VillaNumber villaNumber);

    }
}
