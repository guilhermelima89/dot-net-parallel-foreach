using System.Linq.Expressions;
using Api.Data;
using Api.Interfaces;
using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Repositories;

public abstract class Repository<T> : IRepository<T> where T : Entity, new()
{
    protected readonly ApplicationDbContext Context;
    protected readonly DbSet<T> DbSet;

    protected Repository(ApplicationDbContext context)
    {
        Context = context;
        DbSet = context.Set<T>();
    }

    public T Where(Expression<Func<T, bool>> predicate)
    {
        return DbSet
            .AsNoTrackingWithIdentityResolution()
            .FirstOrDefault(predicate);
    }

    public void Add(T entity)
    {
        DbSet.Add(entity);
        Commit();
    }

    public void AddRange(List<T> entities)
    {
        DbSet.AddRange(entities);
        Commit();
    }

    public void Update(T entity)
    {
        DbSet.Update(entity);
        Commit();
    }

    public void UpdateRange(List<T> entities)
    {
        DbSet.UpdateRange(entities);
        Commit();
    }

    public void Remove(int id)
    {
        DbSet.Remove(new T { Id = id });
        Commit();
    }

    public void RemoveRange(IEnumerable<T> entities)
    {
        DbSet.RemoveRange(entities);
        Commit();
    }

    private void Commit()
    {
        Context.SaveChanges();
    }

    public void Dispose()
    {
        Context?.Dispose();
    }
}
