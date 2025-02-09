using System.Linq.Expressions;
using ResumeManagementAPI.DTOs;

namespace ResumeManagementAPI.Interface
{
    public interface IRepository <T> where T : class
    {
        Task<Response> CreateAsync(T entity);
        Task<Response> UpdateAsync(T entity);

        Task<Response> DeleteAsync(T entity);
        Task<T?> GetByAsync(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> FindByIdAsync(int id);

    }
}
