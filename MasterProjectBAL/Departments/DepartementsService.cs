using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MasterProjectBAL.Jobs;
using MasterProjectCommonUtility.Logger;
using MasterProjectCommonUtility.Paging;
using MasterProjectCommonUtility.Response;
using MasterProjectDAL.DataModel.DepartmentsRepo;
using MasterProjectDAL.DataModel.JobsRepo;
using MasterProjectDAL.DataModel.LocationsRepo;
using MasterProjectDAL.DataModel;
using MasterProjectDAL.EmailRepo;
using MasterProjectDAL.ViewModel;
using MasterProjectDTOModel.Jobs;
using MasterProjectDTOModel.Departments;
using System.Text.Json;

namespace MasterProjectBAL.Departments
{
    public class DepartementsService : IDepartmentService
    {

        private readonly ILoggerManager _loggerManager;
        private readonly IMapper _mapper;
        private readonly IEmailRepository _emailRepository;
        private readonly IMasterProjectContext _masterProjectContext;
        private readonly IJobsRepository _jobsRepository;
        private readonly IDepartmentsRepository _departmentsRepository;
        private readonly ILocationsRepository _locationsRepository;
        private readonly IPagingParameter _pagingParameter;
        public DepartementsService(ILoggerManager loggerManager, IDepartmentsRepository departmentsRepository, ILocationsRepository locationsRepository, IJobsRepository jobsRepository, IMapper mapper, IMasterProjectContext masterProjectContext, IPagingParameter pagingParameter, IEmailRepository emailRepository)
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
        public async Task<ResultWithDataDTO<int>> AddDepartments(DepartmentsRequest_DTO request_DTO)
        {
            _loggerManager.LogInfo("Entry DepartementsService=> AddDepartments");
            ResultWithDataDTO<int> ResultWithDataDTO = new ResultWithDataDTO<int>
            {
                IsSuccessful = false
            };
            try
            {
                var data = _mapper.Map<MasterProjectDAL.DataModel.Departments>(request_DTO);
                var dataResult = await _departmentsRepository.AddDepartment(data);

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
                    ResultWithDataDTO.BusinessErrorMessage = $"Failed to add Department-Error observed during registering ContactUs .\nKindly retry or contact System Administrator.";
                    _loggerManager.LogError(ResultWithDataDTO.BusinessErrorMessage);
                }

            }
            catch (Exception ex)
            {
                ResultWithDataDTO.IsBusinessError = true;
                ResultWithDataDTO.BusinessErrorMessage = $"Failed to add Jobs-Error observed during registering ContactUs: \nKindly retry or contact System Administrator.";
                ResultWithDataDTO.SystemErrorMessage = $"DepartementsService=> AddDepartments: Exception Message: {ex.Message}\n" +
                    $"Stack Trace: {ex.StackTrace}.\n Inner Exception Message:{ex.InnerException?.Message} with Request Object : {JsonSerializer.Serialize(request_DTO)}";
                _loggerManager.LogError(ResultWithDataDTO.BusinessErrorMessage);
                if (ResultWithDataDTO.SystemErrorMessage != null)
                {
                    _emailRepository.SendEmail(ResultWithDataDTO.SystemErrorMessage, "AddDepartments");
                }
            }
            _loggerManager.LogInfo("Exit DepartementsService=> AddDepartments");
            return ResultWithDataDTO;
        }



        public async Task<ResultWithDataDTO<int>> UpdateDepartments(int Id, UpdateDepartementsRequest_DTO request_DTO)
        {
            _loggerManager.LogInfo("Entry DepartementsService=> UpdateDepartments");
            ResultWithDataDTO<int> ResultWithDataDTO = new ResultWithDataDTO<int>
            {
                IsSuccessful = false
            };
            try
            {

                var preExistData = await _departmentsRepository.GetDepartmentById(Id);
                if (preExistData != null)
                {
                    var dataResult = await _departmentsRepository.UpdateDepartment(_mapper.Map(request_DTO, preExistData));

                    if (dataResult != null)
                    {
                        ResultWithDataDTO.Data = _mapper.Map<int>(1);
                        ResultWithDataDTO.IsSuccessful = true;
                        ResultWithDataDTO.Message = $"department for departmentId : '{Id}', updated successfully.";
                        _loggerManager.LogInfo(ResultWithDataDTO.Message);
                    }
                    else
                    {
                        ResultWithDataDTO.IsBusinessError = true;
                        ResultWithDataDTO.BusinessErrorMessage = $"Failed to update department-Error observed during updating department for department Id '{Id}'.\nKindly retry or contact System Administrator.";
                        _loggerManager.LogError(ResultWithDataDTO.BusinessErrorMessage);
                    }
                }
                else
                {
                    ResultWithDataDTO.IsBusinessError = true;
                    ResultWithDataDTO.BusinessErrorMessage = $"Cannot update the department. department for departmentId: '{Id}' do not exists.\nKindly verify.";
                    _loggerManager.LogError(ResultWithDataDTO.BusinessErrorMessage);
                }

            }
            catch (Exception ex)
            {
                ResultWithDataDTO.IsBusinessError = true;
                ResultWithDataDTO.BusinessErrorMessage = $"Failed to update department-Error observed during updating department: '{Id}'.\nKindly retry or contact System Administrator.";
                ResultWithDataDTO.SystemErrorMessage = $"DepartementsService=> UpdateDepartments: Exception Message: {ex.Message}\n" +
                    $"Stack Trace: {ex.StackTrace}.\n Inner Exception Message:{ex.InnerException?.Message} with Request Object : {JsonSerializer.Serialize(request_DTO)}";
                _loggerManager.LogError($"Business Error:{ResultWithDataDTO.BusinessErrorMessage}\n\n System Error:{ResultWithDataDTO.SystemErrorMessage}");
                if (ResultWithDataDTO.SystemErrorMessage != null)
                {
                    _emailRepository.SendEmail(ResultWithDataDTO.SystemErrorMessage, "UpdateDepartments");
                }
            }
            _loggerManager.LogInfo("Exit DepartementsService=> UpdateDepartments");
            return ResultWithDataDTO;
        }

        public async Task<ResultWithDataDTO<GetDepartemntsDetailsByIdResponse_DTO>> GetDepartmentDetails(int Id)
        {
            _loggerManager.LogInfo("Entry DepartementsService=> GetDepartmentDetails");
            ResultWithDataDTO<GetDepartemntsDetailsByIdResponse_DTO> ResultWithDataDTO = new ResultWithDataDTO<GetDepartemntsDetailsByIdResponse_DTO>
            {
                IsSuccessful = false
            };
            try
            {
                var Data = await _departmentsRepository.GetDepartmentById(Id);
                if (Data != null)
                {


                    ResultWithDataDTO.Data = _mapper.Map<GetDepartemntsDetailsByIdResponse_DTO>(Data);
                    ResultWithDataDTO.IsSuccessful = true;
                    ResultWithDataDTO.Message = $"Departments retrieved successfully.";
                    _loggerManager.LogInfo(ResultWithDataDTO.Message);
                }
                else
                {
                    ResultWithDataDTO.IsSuccessful = true;
                    ResultWithDataDTO.Message = $"Department Data not found.";
                    _loggerManager.LogInfo(ResultWithDataDTO.Message);

                }
            }

            catch (Exception ex)
            {
                ResultWithDataDTO.IsBusinessError = true;
                ResultWithDataDTO.BusinessErrorMessage = $"Operation failed: Error observed during Data Retrieval.\nKindly retry or contact System Administrator.";
                ResultWithDataDTO.SystemErrorMessage = $"DepartementsService=> GetDepartmentDetails: Exception Message: {ex.Message}\n" +
                    $"Stack Trace: {ex.StackTrace}.\n Inner Exception Message:{ex.InnerException?.Message} with Request Object : {Id}";
                _loggerManager.LogError(ResultWithDataDTO.BusinessErrorMessage);
                if (ResultWithDataDTO.SystemErrorMessage != null)
                {
                    _emailRepository.SendEmail(ResultWithDataDTO.SystemErrorMessage, "GetDepartmentDetails");
                }
            }
            _loggerManager.LogInfo("Exit DepartementsService=> GetDepartmentDetails");
            return ResultWithDataDTO;
        }
    }
       
}
