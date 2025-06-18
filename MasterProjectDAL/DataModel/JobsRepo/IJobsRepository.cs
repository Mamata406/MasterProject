using MasterProjectDAL.ViewModel;
using MasterProjectDTOModel.Jobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterProjectDAL.DataModel.JobsRepo
{
    public interface IJobsRepository
    {
        Task<Jobs> AddJobs(Jobs job);
        Task<Jobs> UpdateJobs(Jobs updateJobs);
        Task<Jobs> GetJobsById(int jobsId);
        Task<List<GetJobsDetailsByListResponse_DTO>> GetList(string q, PageList pageList, int locationId, int departmentId);
    }
}
