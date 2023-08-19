using ITicketUZ.Data.Contexts;
using ITicketUZ.Data.IRepositories;
using ITicketUZ.Domain.Commons;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ITicketUZ.Data.Repositories;
public class Repository<T> : IRepository<T> where T : Auditable
{
    protected readonly AppDbContexts dbContext;
    protected readonly DbSet<T> dbSet;

    public Repository(AppDbContexts dbContext)
    {
        this.dbContext = dbContext;
        this.dbSet = dbContext.Set<T>();
    }

    public async ValueTask<T> InsertAsync(T entity)
    => (await this.dbSet.AddAsync(entity)).Entity;

    public async ValueTask SaveAsync()
        => await dbContext.SaveChangesAsync();

    public IQueryable<T> SelectAll(Expression<Func<T, bool>> expression = null, string[] includes = null)
    {
        IQueryable<T> query = expression is null ? this.dbSet : this.dbSet.Where(expression);

        if (includes is not null)
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
        }
        return query;
    }

    public async ValueTask<T> SelectAsync(Expression<Func<T, bool>> expression, string[] includes = null)
        => await this.SelectAll(expression, includes).FirstOrDefaultAsync(t => !t.IsDeleted);

    public async ValueTask<bool> DeleteAsync(Expression<Func<T, bool>> expression)
    {
        var entity = await this.SelectAsync(expression);

        if (entity is not null)
        {
            entity.IsDeleted = true;
            return true;
        }
        return false;
    }
}
