using MasterProjectCommonUtility.Logger;
using MasterProjectDAL.ViewModel;
using MasterProjectDTOModel.Jobs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterProjectDAL.DataModel.JobsRepo
{
    public class JobsRepository  : IJobsRepository
    {
        private IMasterProjectContext _context;
        private ILoggerManager _loggerManager;

        public JobsRepository(IMasterProjectContext context, ILoggerManager loggerManager)
        {
            _context = context;
            _loggerManager = loggerManager;
        }
        public async Task<Jobs> AddJobs(Jobs job)
        {
            _loggerManager.LogInfo("Entry JobsRepository=> AddWarehouseData");
           var data =  await _context.Jobs.AddAsync(job);
            await _context.SaveChangesAsync();
            _loggerManager.LogInfo("Exit DepartmentsRepository=> AddWarehouseData");
            return data.Entity;
        }
        public async Task<Jobs> UpdateJobs(Jobs Jobs)
        {
            _loggerManager.LogInfo("Entry JobsRepository=> UpdateJobs");
            if (Jobs != null)
            {

                _context.Jobs.Update(Jobs);
                await _context.SaveChangesAsync();
            }
            _loggerManager.LogInfo("Exit JobsRepository=> UpdateJobs");
            return Jobs;
        }
        public async Task<Jobs> GetJobsById(int jobsId)
        {
           return await _context.Jobs.FirstOrDefaultAsync(x => x.Id == jobsId);
           
        }

        public async Task<List<GetJobsDetailsByListResponse_DTO>> GetList(string q,PageList pageList,int locationId,int departmentId)
        {
            _loggerManager.LogInfo("Entry JobsRepository=> GetList");
            var dataResult = await (
                from j in _context.Jobs
                join d in _context.Departments on j.DepartmentId equals d.Id into depdata
                from d in depdata.DefaultIfEmpty()
                join l in _context.Locations on j.LocationId equals l.Id into locdata
                from l in locdata.DefaultIfEmpty()
                where (locationId == 0 ? true : locationId == l.Id) && (departmentId == 0 ? true : departmentId == d.Id)

        && (string.IsNullOrWhiteSpace(q) ||
    (j.Code ?? "").ToLower().Contains(q.ToLower()) ||
    (j.Title ?? "").ToLower().Contains(q.ToLower()) ||
    (l.Country ?? "").ToLower().Contains(q.ToLower()) ||
    (d.Title ?? "").ToLower().Contains(q.ToLower()))

                orderby j.PostedDate descending
                select new GetJobsDetailsByListResponse_DTO
                {
                    Id = l.Id,
                    Code = j.Code,
                    Title = j.Title,
                    Location = l.Country,
                    Department = d.Title,
                    PostedDate = j.PostedDate,
                    ClosingDate = j.ClosingDate
                }).OrderByDescending(x =>x.Id).Skip((pageList.PageNumber - 1) * pageList.PageSize)
                    .Take(pageList.PageSize).ToListAsync();

            return dataResult;

        }
    }
}
