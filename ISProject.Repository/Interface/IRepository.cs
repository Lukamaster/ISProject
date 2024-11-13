using ISProject.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISProject.Repository.Interface
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<List<T>> GetAll();
        Task<T> Get(Guid? id);
        Task<T> Insert(T entity);
        Task<T> Update(T entity);
        Task Delete(T entity);
    }
}
