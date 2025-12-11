using System;
using System.Threading.Tasks;

namespace OutletRopa.Domain.Interfaces 
{
    public interface IUnitOfWork : IDisposable
    {
       
        IGenericRepository<T> Repository<T>() where T : class;

        
        Task<int> CompleteAsync();
    }
}