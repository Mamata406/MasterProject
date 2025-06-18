using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterProjectDAL.DataModel.DepartmentsRepo
{
    public  interface IDepartmentsRepository
    {
        Task<Departments> AddDepartment(Departments dep);
        Task<Departments> UpdateDepartment(Departments dep);
        Task<Departments> GetDepartmentById(int depId);
    }
}
