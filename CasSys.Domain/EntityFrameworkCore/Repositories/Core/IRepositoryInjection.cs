using Microsoft.EntityFrameworkCore;

namespace CasSys.Domain.EntityFrameworkCore.Repositories.Core
{
    public interface IRepositoryInjection
    {
        IRepositoryInjection SetContext(DbContext context);
    }
}