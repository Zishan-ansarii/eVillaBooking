using eVillaBooking.Application.Common.Interfaces;
using eVillaBooking.Domain.Entities;
using eVillaBooking.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace eVillaBooking.Infrastructure.Repository
{
    public class VillaRepository : IVillaRepository
    {
        private readonly ApplicationDbContext _db;
        public VillaRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public void Add(Villa villa)
        {
            _db.Villas.Add(villa);
        }

        public void Update(Villa villa)
        {
            _db.Villas.Update(villa);
        }

        public void Remove(Villa villa)
        {
            _db.Villas.Remove(villa);
        }

        public void save()
        {
            _db.SaveChanges();
        }

        public Villa Get(Expression<Func<Villa, bool>> filter, string? includeProperties = null)
        {
            IQueryable<Villa> query = _db.Villas;

            query = query.Where(filter);

            if (includeProperties is not null)
            {
                foreach (var IncludeProperty in includeProperties.Split(new char[] { ',', ' ', '-', '_' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(IncludeProperty);
                }
            }
            return query.FirstOrDefault();
        }

        public IEnumerable<Villa> GetAll(Expression<Func<Villa, bool>>? filter = null, string? includeProperties = null)
        {
            IQueryable<Villa> query = _db.Villas;
            if (filter is not null)
            {
                query = query.Where(filter);
            }

            if (includeProperties is not null)
            {
                foreach (var IncludeProperty in includeProperties.Split(new char[] {',',' ','-','_' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(IncludeProperty);
                }
            }
            return query.ToList();
        }


    }
}
