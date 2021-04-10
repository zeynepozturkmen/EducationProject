using System;
using System.Threading.Tasks;

namespace EducationProject.Service.IUnitOfWorks
{
    public interface IUnitOfWork : IDisposable
    {
        Task<bool> CommitAsync();
    }
}
