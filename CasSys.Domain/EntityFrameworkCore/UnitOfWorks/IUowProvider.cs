using Microsoft.EntityFrameworkCore;

namespace CasSys.Domain.EntityFrameworkCore.UnitOfWorks
{
    public interface IUowProvider
    {
        IUnitOfWork CreateUnitOfWork();
        IUnitOfWork CreateUnitOfWork<TEntityContext>() where TEntityContext : DbContext;
    }
}