using MasterProjectCommonUtility.Logger;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterProjectDAL.DataModel.DepartmentsRepo
{
    public class DepartmentsRepository : IDepartmentsRepository
    {
        private IMasterProjectContext _context;
        private ILoggerManager _loggerManager;

        public DepartmentsRepository(IMasterProjectContext context, ILoggerManager loggerManager)
        {
            _context = context;
            _loggerManager = loggerManager;
        }
        public async Task<Departments> AddDepartment(Departments dep)
        {
            _loggerManager.LogInfo("Entry DepartmentsRepository=> AddWarehouseData");
            var data = await _context.Departments.AddAsync(dep);
            await _context.SaveChangesAsync();
            _loggerManager.LogInfo("Exit DepartmentsRepository=> AddWarehouseData");
            return data.Entity;
        }
       
        public async Task<Departments> UpdateDepartment(Departments dep)
        {
            _loggerManager.LogInfo("Entry DepartmentsRepository=> UpdateDepartment");
            if (dep != null)
            {

                _context.Departments.Update(dep);
                await _context.SaveChangesAsync();
            }
            _loggerManager.LogInfo("Exit DepartmentsRepository=> UpdateDepartment");
            return dep;
        }
        public async Task<Departments> GetDepartmentById(int depId)
        {
            return await _context.Departments.FirstOrDefaultAsync(x => x.Id == depId);

        }
    }
}
