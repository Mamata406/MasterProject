using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MasterProjectCommonUtility.Logger;
using MasterProjectCommonUtility.Paging;
using MasterProjectCommonUtility.Response;
using MasterProjectDAL.DataModel.DepartmentsRepo;
using MasterProjectDAL.DataModel.JobsRepo;
using MasterProjectDAL.DataModel.LocationsRepo;
using MasterProjectDAL.DataModel;
using MasterProjectDAL.EmailRepo;
using MasterProjectDTOModel.Departments;
using System.Text.Json;
using MasterProjectDTOModel.Locations;

namespace MasterProjectBAL.Locations
{
    public  class LocationService : ILocationService
    {
        private readonly ILoggerManager _loggerManager;
        private readonly IMapper _mapper;
        private readonly IEmailRepository _emailRepository;
        private readonly IMasterProjectContext _masterProjectContext;
        private readonly IJobsRepository _jobsRepository;
        private readonly IDepartmentsRepository _departmentsRepository;
        private readonly ILocationsRepository _locationsRepository;
        private readonly IPagingParameter _pagingParameter;
        public LocationService(ILoggerManager loggerManager, IDepartmentsRepository departmentsRepository, ILocationsRepository locationsRepository, IJobsRepository jobsRepository, IMapper mapper, IMasterProjectContext masterProjectContext, IPagingParameter pagingParameter, IEmailRepository emailRepository)
        {
            _loggerManager = loggerManager;
            _mapper = mapper;
            _emailRepository = emailRepository;
            _masterProjectContext = masterProjectContext;
            _pagingParameter = pagingParameter;
            _jobsRepository = jobsRepository;
            _departmentsRepository = departmentsRepository;
            _locationsRepository = locationsRepository;


        }
        public async Task<ResultWithDataDTO<int>> AddLocations(LocationsRequest_DTO request_DTO)
        {
            _loggerManager.LogInfo("Entry LocationService=> AddLocations");
            ResultWithDataDTO<int> ResultWithDataDTO = new ResultWithDataDTO<int>
            {
                IsSuccessful = false
            };
            try
            {
                var dataResult = await _locationsRepository.AddLocation(_mapper.Map<MasterProjectDAL.DataModel.Locations>(request_DTO));

                if (dataResult != null)
                {
                    ResultWithDataDTO.Data = dataResult.Id;
                    ResultWithDataDTO.IsSuccessful = true;
                    ResultWithDataDTO.Message = $"Department details added successfully.";
                    _loggerManager.LogInfo(ResultWithDataDTO.Message);
                }
                else
                {
                    ResultWithDataDTO.IsBusinessError = true;
                    ResultWithDataDTO.BusinessErrorMessage = $"Failed to add location-Error observed during registering ContactUs .\nKindly retry or contact System Administrator.";
                    _loggerManager.LogError(ResultWithDataDTO.BusinessErrorMessage);
                }

            }
            catch (Exception ex)
            {
                ResultWithDataDTO.IsBusinessError = true;
                ResultWithDataDTO.BusinessErrorMessage = $"Failed to add loactions-Error observed during registering ContactUs: \nKindly retry or contact System Administrator.";
                ResultWithDataDTO.SystemErrorMessage = $"LocationService=> AddLocations: Exception Message: {ex.Message}\n" +
                    $"Stack Trace: {ex.StackTrace}.\n Inner Exception Message:{ex.InnerException?.Message} with Request Object : {JsonSerializer.Serialize(request_DTO)}";
                _loggerManager.LogError(ResultWithDataDTO.BusinessErrorMessage);
                if (ResultWithDataDTO.SystemErrorMessage != null)
                {
                    _emailRepository.SendEmail(ResultWithDataDTO.SystemErrorMessage, "AddLocations");
                }
            }
            _loggerManager.LogInfo("Exit LocationService=> AddDepartments");
            return ResultWithDataDTO;
        }



        public async Task<ResultWithDataDTO<int>> UpdateLocations(int Id, UpdateLocationsRequest_DTO request_DTO)
        {
            _loggerManager.LogInfo("Entry LocationService=> UpdateLocations");
            ResultWithDataDTO<int> ResultWithDataDTO = new ResultWithDataDTO<int>
            {
                IsSuccessful = false
            };
            try
            {

                var preExistData = await _locationsRepository.GetLocationById(Id);
                if (preExistData != null)
                {
                    var dataResult = await _locationsRepository.UpdateLocation(_mapper.Map(request_DTO, preExistData));

                    if (dataResult != null)
                    {
                        ResultWithDataDTO.Data = _mapper.Map<int>(1);
                        ResultWithDataDTO.IsSuccessful = true;
                        ResultWithDataDTO.Message = $"Location for locationId : '{Id}', updated successfully.";
                        _loggerManager.LogInfo(ResultWithDataDTO.Message);
                    }
                    else
                    {
                        ResultWithDataDTO.IsBusinessError = true;
                        ResultWithDataDTO.BusinessErrorMessage = $"Failed to update Location-Error observed during updating department for department Id '{Id}'.\nKindly retry or contact System Administrator.";
                        _loggerManager.LogError(ResultWithDataDTO.BusinessErrorMessage);
                    }
                }
                else
                {
                    ResultWithDataDTO.IsBusinessError = true;
                    ResultWithDataDTO.BusinessErrorMessage = $"Cannot update the Location. Location for LocationId: '{Id}' do not exists.\nKindly verify.";
                    _loggerManager.LogError(ResultWithDataDTO.BusinessErrorMessage);
                }

            }
            catch (Exception ex)
            {
                ResultWithDataDTO.IsBusinessError = true;
                ResultWithDataDTO.BusinessErrorMessage = $"Failed to update Location-Error observed during updating Location: '{Id}'.\nKindly retry or contact System Administrator.";
                ResultWithDataDTO.SystemErrorMessage = $"LocationService=> UpdateLocations: Exception Message: {ex.Message}\n" +
                    $"Stack Trace: {ex.StackTrace}.\n Inner Exception Message:{ex.InnerException?.Message} with Request Object : {JsonSerializer.Serialize(request_DTO)}";
                _loggerManager.LogError($"Business Error:{ResultWithDataDTO.BusinessErrorMessage}\n\n System Error:{ResultWithDataDTO.SystemErrorMessage}");
                if (ResultWithDataDTO.SystemErrorMessage != null)
                {
                    _emailRepository.SendEmail(ResultWithDataDTO.SystemErrorMessage, "UpdateLocations");
                }
            }
            _loggerManager.LogInfo("Exit LocationService=> UpdateLocations");
            return ResultWithDataDTO;
        }

        public async Task<ResultWithDataDTO<GetLocationDetailsByIdResponse_DTO>> GetLocationstDetails(int Id)
        {
            _loggerManager.LogInfo("Entry LocationService=> GetLocationstDetails");
            ResultWithDataDTO<GetLocationDetailsByIdResponse_DTO> ResultWithDataDTO = new ResultWithDataDTO<GetLocationDetailsByIdResponse_DTO>
            {
                IsSuccessful = false
            };
            try
            {
                var Data = await _locationsRepository.GetLocationById(Id);
                if (Data != null)
                {


                    ResultWithDataDTO.Data = _mapper.Map<GetLocationDetailsByIdResponse_DTO>(Data);
                    ResultWithDataDTO.IsSuccessful = true;
                    ResultWithDataDTO.Message = $"Locations retrieved successfully.";
                    _loggerManager.LogInfo(ResultWithDataDTO.Message);
                }
                else
                {
                    ResultWithDataDTO.IsSuccessful = true;
                    ResultWithDataDTO.Message = $"location Data not found.";
                    _loggerManager.LogInfo(ResultWithDataDTO.Message);

                }
            }

            catch (Exception ex)
            {
                ResultWithDataDTO.IsBusinessError = true;
                ResultWithDataDTO.BusinessErrorMessage = $"Operation failed: Error observed during Data Retrieval.\nKindly retry or contact System Administrator.";
                ResultWithDataDTO.SystemErrorMessage = $"LocationService=> GetLocationstDetails: Exception Message: {ex.Message}\n" +
                    $"Stack Trace: {ex.StackTrace}.\n Inner Exception Message:{ex.InnerException?.Message} with Request Object : {Id}";
                _loggerManager.LogError(ResultWithDataDTO.BusinessErrorMessage);
                if (ResultWithDataDTO.SystemErrorMessage != null)
                {
                    _emailRepository.SendEmail(ResultWithDataDTO.SystemErrorMessage, "GetLocationstDetails");
                }
            }
            _loggerManager.LogInfo("Exit LocationService=> GetLocationstDetails");
            return ResultWithDataDTO;
        }
    }
}

