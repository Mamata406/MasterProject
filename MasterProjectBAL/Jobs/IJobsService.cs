using MasterProjectCommonUtility.Response;
using MasterProjectDTOModel.Jobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterProjectBAL.Jobs
{
    public interface IJobsService
    {
        Task<ResultWithDataDTO<int>> AddJobs(JobsRequest_DTO request_DTO);
        Task<ResultWithDataDTO<int>> UpdateJobs(int Id, UpdateJobsRequest_DTO request_DTO);
        Task<ResultWithDataDTO<GetJobsDetailsByIdResponse_DTO>> GetJobsDetails(int Id);
        Task<ResultWithDataDTO<List<GetJobsDetailsByListResponse_DTO>>> List(string q, int locationId, int departmentId);
    }
}
