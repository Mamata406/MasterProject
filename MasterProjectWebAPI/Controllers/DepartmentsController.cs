using MasterProjectBAL.Departments;
using MasterProjectBAL.Jobs;
using MasterProjectCommonUtility.Logger;
using MasterProjectCommonUtility.Paging;
using MasterProjectCommonUtility.Response;
using MasterProjectDTOModel.Departments;
using MasterProjectDTOModel.Jobs;
using Microsoft.AspNetCore.Mvc;

namespace MasterProjectWebAPI.Controllers
{
    
    [Route("[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly ILoggerManager _loggerManager;
        private readonly IDepartmentService _departmentService;
        private readonly IPagingParameter _pagingParameter;
        public DepartmentsController(ILoggerManager loggerManager, IDepartmentService  departmentService, IPagingParameter pagingParameter, IJobsService jobService)
        {
            _loggerManager = loggerManager;
            _departmentService = departmentService;
               _pagingParameter = pagingParameter;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Departments([FromBody] DepartmentsRequest_DTO request_DTO)
        {
            ResultWithDataDTO<int> resultWithDataDTO =
                new ResultWithDataDTO<int> { IsSuccessful = false };
            if (request_DTO == null)
            {
                resultWithDataDTO.IsBusinessError = true;
                resultWithDataDTO.BusinessErrorMessage = "Error,Departments Information posted to the Server is empty. Kindly retry, or contact System Admin.";
                return BadRequest(resultWithDataDTO);
            }
            _loggerManager.LogInfo("Entry DepartmentsController=> Departments");
            resultWithDataDTO = await _departmentService.AddDepartments(request_DTO);
            _loggerManager.LogInfo("Exit DepartmentsController=> Departments");
            if (resultWithDataDTO.IsSuccessful)
            { return Ok(resultWithDataDTO); }
            else { return BadRequest(resultWithDataDTO); }
        }

        [HttpPut]
        [Route("[action]/{Id}")]
        public async Task<IActionResult> Departments(int Id, [FromBody] UpdateDepartementsRequest_DTO request_DTO)
        {
            ResultWithDataDTO<int> resultWithDataDTO =
                new ResultWithDataDTO<int> { IsSuccessful = false };
            if (request_DTO == null)
            {
                resultWithDataDTO.IsBusinessError = true;
                resultWithDataDTO.BusinessErrorMessage = "Error,Product Information posted to the Server is empty. Kindly retry, or contact System Admin.";
                return BadRequest(resultWithDataDTO);
            }
            _loggerManager.LogInfo("Entry DepartmentsController=> Departments");
            resultWithDataDTO = await _departmentService.UpdateDepartments(Id, request_DTO);
            _loggerManager.LogInfo("Exit DepartmentsController=> Departments");
            if (resultWithDataDTO.IsSuccessful)
            { return Ok(resultWithDataDTO); }
            else { return BadRequest(resultWithDataDTO); }
        }



        [HttpGet]

        [Route("[action]/{Id}")]
        public async Task<IActionResult> Departments(int Id)
        {
            ResultWithDataDTO<GetDepartemntsDetailsByIdResponse_DTO> resultWithDataDTO =
              new ResultWithDataDTO<GetDepartemntsDetailsByIdResponse_DTO> { IsSuccessful = false };
            if (Id == 0)
            {
                resultWithDataDTO.IsBusinessError = true;
                resultWithDataDTO.BusinessErrorMessage = "Error Departments";
                return BadRequest(resultWithDataDTO);
            }
            _loggerManager.LogInfo("Entry DepartmentsController=> Departments");
            resultWithDataDTO = await _departmentService.GetDepartmentDetails(Id);
            _loggerManager.LogInfo("Exit DepartmentsController=> Departments ");
            if (resultWithDataDTO.IsSuccessful)
            { return Ok(resultWithDataDTO); }
            else { return BadRequest(resultWithDataDTO); }
        }

    }
}
