using Data_Access_Layer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Repository_Layer.Child_Repositories;
using Repository_Layer.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository_Layer.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ApplicationDbContext _dbContext;
        public IDepartmentRepository Departments { get; private set; }
        
        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            Departments = new DepartmentRepository(dbContext);
        }

        public async Task CompleteAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
