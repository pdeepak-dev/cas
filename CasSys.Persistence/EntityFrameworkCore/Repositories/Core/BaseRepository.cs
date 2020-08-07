using Microsoft.EntityFrameworkCore;
using CasSys.Domain.EntityFrameworkCore.Repositories.Core;

namespace CasSys.Persistence.EntityFrameworkCore.Repositories.Core
{
    public abstract class BaseRepository : IRepositoryInjection
    {
        protected DbContext Context { get; private set; }

        public IRepositoryInjection SetContext(DbContext context)
        {
            this.Context = context;

            return this;
        }
    }
}
