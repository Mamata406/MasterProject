using MasterProjectBAL.Jobs;
using MasterProjectCommonUtility.Logger;
using MasterProjectCommonUtility.Paging;
using MasterProjectCommonUtility.Response;
using MasterProjectDTOModel.Jobs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace MasterProjectWebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class JobsController : ControllerBase
    {
        private readonly ILoggerManager _loggerManager;
        private readonly IJobsService _jobsService;
        private readonly IPagingParameter _pagingParameter;
        public JobsController(ILoggerManager loggerManager, IPagingParameter pagingParameter,IJobsService jobService)
        {
            _loggerManager = loggerManager;
            _jobsService = jobService;
            _pagingParameter = pagingParameter;
        }
        [Authorize]
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> jobs([FromBody] JobsRequest_DTO request_DTO)
        {
            ResultWithDataDTO<int> resultWithDataDTO =
                new ResultWithDataDTO<int> { IsSuccessful = false };
            if (request_DTO == null)
            {
                resultWithDataDTO.IsBusinessError = true;
                resultWithDataDTO.BusinessErrorMessage = "Error,Product Information posted to the Server is empty. Kindly retry, or contact System Admin.";
                return BadRequest(resultWithDataDTO);
            }
            _loggerManager.LogInfo("Entry JobsController=> jobs");
            resultWithDataDTO = await _jobsService.AddJobs(request_DTO);
            _loggerManager.LogInfo("Exit JobsController=> jobs");
            if (resultWithDataDTO.IsSuccessful)
            { return Ok(resultWithDataDTO); }
            else { return BadRequest(resultWithDataDTO); }
        }

        [Authorize]

        [HttpPut]
        [Route("[action]/{Id}")]
        public async Task<IActionResult> jobs(int Id,[FromBody] UpdateJobsRequest_DTO request_DTO)
        {
            ResultWithDataDTO<int> resultWithDataDTO =
                new ResultWithDataDTO<int> { IsSuccessful = false };
            if (request_DTO == null)
            {
                resultWithDataDTO.IsBusinessError = true;
                resultWithDataDTO.BusinessErrorMessage = "Error,Product Information posted to the Server is empty. Kindly retry, or contact System Admin.";
                return BadRequest(resultWithDataDTO);
            }
            _loggerManager.LogInfo("Entry JobsController=> UpdateJobs");
            resultWithDataDTO = await _jobsService.UpdateJobs(Id,request_DTO);
            _loggerManager.LogInfo("Exit JobsController=> UpdateJobs");
            if (resultWithDataDTO.IsSuccessful)
            { return Ok(resultWithDataDTO); }
            else { return BadRequest(resultWithDataDTO); }
        }

        [Authorize]

        [HttpGet]

        [Route("[action]/{q}/{pageNo}/{pageSize}")]
        public async Task<IActionResult> List(string q,int? pageNo,int? pageSize,int locationId,int departmentId)
        {
            ResultWithDataDTO<List<GetJobsDetailsByListResponse_DTO>> resultWithDataDTO =
              new ResultWithDataDTO<List<GetJobsDetailsByListResponse_DTO>> { IsSuccessful = false };
          
            _pagingParameter.PageNumber = pageNo;
            _pagingParameter.PageSize = pageSize;
            _loggerManager.LogInfo("Entry JobsController=> List");
            resultWithDataDTO = await _jobsService.List(q, locationId, departmentId);
            _loggerManager.LogInfo("Exit JobsController=> List ");
            if (resultWithDataDTO.IsSuccessful)
            { return Ok(resultWithDataDTO); }
            else { return BadRequest(resultWithDataDTO); }
        }


        [Authorize]
        [HttpGet]

        [Route("[action]/{Id}")]
        public async Task<IActionResult> jobs(int Id)
        {
            ResultWithDataDTO<GetJobsDetailsByIdResponse_DTO> resultWithDataDTO =
              new ResultWithDataDTO<GetJobsDetailsByIdResponse_DTO> { IsSuccessful = false };
            if (Id == 0)
            {
                resultWithDataDTO.IsBusinessError = true;
                resultWithDataDTO.BusinessErrorMessage = "Error GetJobsDetails";
                return BadRequest(resultWithDataDTO);
            }
            _loggerManager.LogInfo("Entry JobsController=> GetJobsDetails");
            resultWithDataDTO = await _jobsService.GetJobsDetails(Id);
            _loggerManager.LogInfo("Exit JobsController=> GetJobsDetails ");
            if (resultWithDataDTO.IsSuccessful)
            { return Ok(resultWithDataDTO); }
            else { return BadRequest(resultWithDataDTO); }
        }

    }
}

