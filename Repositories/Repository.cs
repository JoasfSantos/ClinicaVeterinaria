#nullable disable
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using ClinicaVet.Data;


namespace ClinicaVet.Repositories
{
    public class Repository<TEntity>(MyDbContext context) where TEntity : class
    {
        protected readonly MyDbContext Context = context;

        public async Task<TEntity> Get(int id)
        {
            return await Context.Set<TEntity>().FindAsync(id);
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await Context.Set<TEntity>().ToListAsync();
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().Where(predicate);
        }

        public async Task Add(TEntity entity)
        {
            await Context.Set<TEntity>().AddAsync(entity);
            await Context.SaveChangesAsync();
        }

        public void Update(TEntity entity)
        {
            Context.Set<TEntity>().Update(entity);
            Context.SaveChanges();
        }

        public void Remove(TEntity entity)
        {
            Context.Set<TEntity>().Remove(entity);
            Context.SaveChanges();
        }
    }
}