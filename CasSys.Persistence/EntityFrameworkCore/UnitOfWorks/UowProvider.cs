using System;
using Microsoft.EntityFrameworkCore;
using CasSys.Domain.EntityFrameworkCore.UnitOfWorks;

namespace CasSys.Persistence.EntityFrameworkCore.UnitOfWorks
{
    public class UowProvider : IUowProvider
    {
        private readonly IServiceProvider _serviceProvider;

        public UowProvider(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IUnitOfWork CreateUnitOfWork()
        {
            var uow = (IUnitOfWork)_serviceProvider.GetService(typeof(IUnitOfWork));
            return uow;
        }

        public IUnitOfWork CreateUnitOfWork<TEntityContext>() where TEntityContext : DbContext
        {
            var uow = (IUnitOfWork)_serviceProvider.GetService(typeof(IUnitOfWork<TEntityContext>));
            return uow;
        }
    }
}