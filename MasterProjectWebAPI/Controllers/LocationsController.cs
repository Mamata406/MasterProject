using MasterProjectBAL.Departments;
using MasterProjectBAL.Jobs;
using MasterProjectBAL.Locations;
using MasterProjectCommonUtility.Logger;
using MasterProjectCommonUtility.Paging;
using MasterProjectCommonUtility.Response;
using MasterProjectDTOModel.Departments;
using MasterProjectDTOModel.Locations;
using Microsoft.AspNetCore.Mvc;

namespace MasterProjectWebAPI.Controllers
{
    public class LocationsController : ControllerBase
    {
        private readonly ILoggerManager _loggerManager;
        private readonly ILocationService _locationService;
        private readonly IPagingParameter _pagingParameter;
        public LocationsController(ILoggerManager loggerManager, ILocationService locationService, IPagingParameter pagingParameter, IJobsService jobService)
        {
            _loggerManager = loggerManager;
            _locationService = locationService;
               _pagingParameter = pagingParameter;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Locations([FromBody] LocationsRequest_DTO request_DTO)
        {
            ResultWithDataDTO<int> resultWithDataDTO =
                new ResultWithDataDTO<int> { IsSuccessful = false };
            if (request_DTO == null)
            {
                resultWithDataDTO.IsBusinessError = true;
                resultWithDataDTO.BusinessErrorMessage = "Error,Departments Information posted to the Server is empty. Kindly retry, or contact System Admin.";
                return BadRequest(resultWithDataDTO);
            }
            _loggerManager.LogInfo("Entry LocationsController=> Locations");
            resultWithDataDTO = await _locationService.AddLocations(request_DTO);
            _loggerManager.LogInfo("Exit LocationsController=> Locations");
            if (resultWithDataDTO.IsSuccessful)
            { return Ok(resultWithDataDTO); }
            else { return BadRequest(resultWithDataDTO); }
        }

        [HttpPut]
        [Route("[action]/{Id}")]
        public async Task<IActionResult> Locations(int Id, [FromBody] UpdateLocationsRequest_DTO request_DTO)
        {
            ResultWithDataDTO<int> resultWithDataDTO =
                new ResultWithDataDTO<int> { IsSuccessful = false };
            if (request_DTO == null)
            {
                resultWithDataDTO.IsBusinessError = true;
                resultWithDataDTO.BusinessErrorMessage = "Error,Locations Information posted to the Server is empty. Kindly retry, or contact System Admin.";
                return BadRequest(resultWithDataDTO);
            }
            _loggerManager.LogInfo("Entry LocationsController=> Locations");
            resultWithDataDTO = await _locationService.UpdateLocations(Id, request_DTO);
            _loggerManager.LogInfo("Exit LocationsController=> Locations");
            if (resultWithDataDTO.IsSuccessful)
            { return Ok(resultWithDataDTO); }
            else { return BadRequest(resultWithDataDTO); }
        }



        [HttpGet]

        [Route("[action]/{Id}")]
        public async Task<IActionResult> Locations(int Id)
        {
            ResultWithDataDTO<GetLocationDetailsByIdResponse_DTO> resultWithDataDTO =
              new ResultWithDataDTO<GetLocationDetailsByIdResponse_DTO> { IsSuccessful = false };
            if (Id == 0)
            {
                resultWithDataDTO.IsBusinessError = true;
                resultWithDataDTO.BusinessErrorMessage = "Error Locations";
                return BadRequest(resultWithDataDTO);
            }
            _loggerManager.LogInfo("Entry LocationsController=> Locations");
            resultWithDataDTO = await _locationService.GetLocationstDetails(Id);
            _loggerManager.LogInfo("Exit LocationsController=> Locations ");
            if (resultWithDataDTO.IsSuccessful)
            { return Ok(resultWithDataDTO); }
            else { return BadRequest(resultWithDataDTO); }
        }
    }
}
