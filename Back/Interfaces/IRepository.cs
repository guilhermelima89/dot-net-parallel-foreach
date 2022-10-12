using System.Linq.Expressions;
using Api.Models;

namespace Api.Interfaces;

public interface IRepository<T> : IDisposable where T : Entity
{
    T Where(Expression<Func<T, bool>> predicate);

    void Add(T entity);
    void AddRange(List<T> entities);
    void Update(T entity);
    void UpdateRange(List<T> entities);
    void Remove(int id);
    void RemoveRange(IEnumerable<T> entities);
}
