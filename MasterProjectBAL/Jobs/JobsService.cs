using AutoMapper;
using MasterProjectCommonUtility.Logger;
using MasterProjectCommonUtility.Paging;
using MasterProjectCommonUtility.Response;
using MasterProjectDAL.DataModel;
using MasterProjectDAL.DataModel.DepartmentsRepo;
using MasterProjectDAL.DataModel.JobsRepo;
using MasterProjectDAL.DataModel.LocationsRepo;
using MasterProjectDAL.EmailRepo;
using MasterProjectDAL.ViewModel;
using MasterProjectDTOModel.Jobs;
using System.Text.Json;

namespace MasterProjectBAL.Jobs
{
    public class JobsService : IJobsService
    {
        private readonly ILoggerManager _loggerManager;
        private readonly IMapper _mapper;
        private readonly IEmailRepository _emailRepository;
        private readonly IMasterProjectContext _masterProjectContext;
        private readonly IJobsRepository _jobsRepository;
        private readonly IDepartmentsRepository _departmentsRepository;
        private readonly ILocationsRepository _locationsRepository;
        private readonly IPagingParameter _pagingParameter;
        public JobsService(ILoggerManager loggerManager, IDepartmentsRepository departmentsRepository, ILocationsRepository locationsRepository, IJobsRepository jobsRepository, IMapper mapper, IMasterProjectContext masterProjectContext, IPagingParameter pagingParameter, IEmailRepository emailRepository)
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
    public async Task<ResultWithDataDTO<int>> AddJobs(JobsRequest_DTO request_DTO)
        {
            _loggerManager.LogInfo("Entry JobsService=> AddJobs");
            ResultWithDataDTO<int> ResultWithDataDTO = new ResultWithDataDTO<int>
            {
                IsSuccessful = false
            };
            try
            {
                    var dataResult = await _jobsRepository.AddJobs(_mapper.Map<MasterProjectDAL.DataModel.Jobs>(request_DTO));

                    if (dataResult != null)
                    {
                        ResultWithDataDTO.Data = dataResult.Id;
                        ResultWithDataDTO.IsSuccessful = true;
                        ResultWithDataDTO.Message = $"Job details added successfully.";
                        _loggerManager.LogInfo(ResultWithDataDTO.Message);
                    }
                    else
                    {
                        ResultWithDataDTO.IsBusinessError = true;
                        ResultWithDataDTO.BusinessErrorMessage = $"Failed to add Jobs-Error observed during registering ContactUs .\nKindly retry or contact System Administrator.";
                        _loggerManager.LogError(ResultWithDataDTO.BusinessErrorMessage);
                    }
               
            }
            catch (Exception ex)
            {
                ResultWithDataDTO.IsBusinessError = true;
                ResultWithDataDTO.BusinessErrorMessage = $"Failed to add Jobs-Error observed during registering ContactUs: \nKindly retry or contact System Administrator.";
                ResultWithDataDTO.SystemErrorMessage = $"JobsService=> AddJobs: Exception Message: {ex.Message}\n" +
                    $"Stack Trace: {ex.StackTrace}.\n Inner Exception Message:{ex.InnerException?.Message} with Request Object : {JsonSerializer.Serialize(request_DTO)}";
                _loggerManager.LogError(ResultWithDataDTO.BusinessErrorMessage);
                if (ResultWithDataDTO.SystemErrorMessage != null)
                {
                    _emailRepository.SendEmail(ResultWithDataDTO.SystemErrorMessage, "AddJobs");
                }
            }
            _loggerManager.LogInfo("Exit JobsService=> AddContactUs");
            return ResultWithDataDTO;
        }



        public async Task<ResultWithDataDTO<int>> UpdateJobs(int Id,UpdateJobsRequest_DTO request_DTO)
        {
            _loggerManager.LogInfo("Entry JobsService=> UpdateJobs");
            ResultWithDataDTO<int> ResultWithDataDTO = new ResultWithDataDTO<int>
            {
                IsSuccessful = false
            };
            try
            {

                    var preExistData = await _jobsRepository.GetJobsById(Id);
                    if (preExistData != null)
                    {
                    var dataResult = await _jobsRepository.UpdateJobs(_mapper.Map(request_DTO, preExistData));

                        if (dataResult != null)
                        {
                            ResultWithDataDTO.Data = _mapper.Map<int>(1);
                            ResultWithDataDTO.IsSuccessful = true;
                            ResultWithDataDTO.Message = $"Jobs for JobId : '{Id}', updated successfully.";
                            _loggerManager.LogInfo(ResultWithDataDTO.Message);
                        }
                        else
                        {
                            ResultWithDataDTO.IsBusinessError = true;
                            ResultWithDataDTO.BusinessErrorMessage = $"Failed to update Jobs-Error observed during updating jobs for Job Id '{Id}'.\nKindly retry or contact System Administrator.";
                            _loggerManager.LogError(ResultWithDataDTO.BusinessErrorMessage);
                        }
                    }
                    else
                    {
                        ResultWithDataDTO.IsBusinessError = true;
                        ResultWithDataDTO.BusinessErrorMessage = $"Cannot update the job. job for jobId: '{Id}' do not exists.\nKindly verify.";
                        _loggerManager.LogError(ResultWithDataDTO.BusinessErrorMessage);
                    }
               
            }
            catch (Exception ex)
            {
                ResultWithDataDTO.IsBusinessError = true;
                ResultWithDataDTO.BusinessErrorMessage = $"Failed to update MainBanner-Error observed during updating MainBanner: '{Id}'.\nKindly retry or contact System Administrator.";
                ResultWithDataDTO.SystemErrorMessage = $"JobsService=> UpdateJobs: Exception Message: {ex.Message}\n" +
                    $"Stack Trace: {ex.StackTrace}.\n Inner Exception Message:{ex.InnerException?.Message} with Request Object : {JsonSerializer.Serialize(request_DTO)}";
                _loggerManager.LogError($"Business Error:{ResultWithDataDTO.BusinessErrorMessage}\n\n System Error:{ResultWithDataDTO.SystemErrorMessage}");
                if (ResultWithDataDTO.SystemErrorMessage != null)
                {
                    _emailRepository.SendEmail(ResultWithDataDTO.SystemErrorMessage, "UpdateJobs");
                }
            }
            _loggerManager.LogInfo("Exit JobsService=> UpdateJobs");
            return ResultWithDataDTO;
        }
        public async Task<ResultWithDataDTO<List<GetJobsDetailsByListResponse_DTO>>> List(string q,int locationId,int departmentId)
        {
            _loggerManager.LogInfo("Entry JobsService=> List");
            ResultWithDataDTO<List<GetJobsDetailsByListResponse_DTO>> ResultWithDataDTO = new ResultWithDataDTO<List<GetJobsDetailsByListResponse_DTO>>
            {
                IsSuccessful = false
            };
            try
            {
                var ListData = await _jobsRepository.GetList(q, _mapper.Map<PageList>(_pagingParameter) ,locationId, departmentId);
                if (ListData != null)
                {

                    ResultWithDataDTO.Data = ListData;
                    ResultWithDataDTO.TotalCount = ListData.Count;
                    ResultWithDataDTO.CurrentPage = (int)_pagingParameter.PageNumber;
                    ResultWithDataDTO.PageSize = (int)_pagingParameter.PageSize;
                    ResultWithDataDTO.TotalPages = (int)Math.Ceiling((ResultWithDataDTO.TotalCount / (decimal)_pagingParameter.PageSize));
                    ResultWithDataDTO.previousPage = ResultWithDataDTO.CurrentPage > 1 ? "Yes" : "No";
                    ResultWithDataDTO.nextPage = ResultWithDataDTO.CurrentPage < ResultWithDataDTO.TotalPages ? "Yes" : "No";
                    ResultWithDataDTO.IsSuccessful = true;
                    ResultWithDataDTO.Message = $"List data retrieved successfully.";
                    _loggerManager.LogInfo(ResultWithDataDTO.Message);

                }
                else
                {
                    ResultWithDataDTO.IsSuccessful = true;
                    ResultWithDataDTO.Message = $"List Data not found.";
                    _loggerManager.LogInfo(ResultWithDataDTO.Message);

                }

            }
            catch (Exception ex)
            {
                ResultWithDataDTO.IsBusinessError = true;
                ResultWithDataDTO.BusinessErrorMessage = $"Operation failed: Error observed during Data Retrieval.\nKindly retry or contact System Administrator.";
                ResultWithDataDTO.SystemErrorMessage = $"JobsService=> List: Exception Message: {ex.Message}\n" +
                    $"Stack Trace: {ex.StackTrace}.\n Inner Exception Message:{ex.InnerException?.Message} with Request Object : {q},{locationId},{departmentId}";
                _loggerManager.LogError(ResultWithDataDTO.BusinessErrorMessage);
                if (ResultWithDataDTO.SystemErrorMessage != null)
                {
                    _emailRepository.SendEmail(ResultWithDataDTO.SystemErrorMessage, "List");
                }
            }
            _loggerManager.LogInfo("Exit JobsService=> List");
            return ResultWithDataDTO;
        }
        public async Task<ResultWithDataDTO<GetJobsDetailsByIdResponse_DTO>> GetJobsDetails(int Id)
        {
            _loggerManager.LogInfo("Entry JobsService=> GetJobsDetails");
            ResultWithDataDTO<GetJobsDetailsByIdResponse_DTO> ResultWithDataDTO = new ResultWithDataDTO<GetJobsDetailsByIdResponse_DTO
                >
            {
                IsSuccessful = false
            };
            try
            {
                var JobData = await _jobsRepository.GetJobsById(Id);
                if (JobData != null)
                {
                    var response = _mapper.Map<GetJobsDetailsByIdResponse_DTO>(JobData);
                    var departmentData = await _departmentsRepository.GetDepartmentById(JobData.DepartmentId ?? 0);
                    if (departmentData != null)
                    {
                        var locationData = await _locationsRepository.GetLocationById(JobData.LocationId ?? 0);
                     
                        if (departmentData != null)
                        {
                            response.Department = new GetJobsDetailsByIdResponse_DTO.DepartmentDTO
                            {
                                Id = departmentData.Id,
                                Title = departmentData.Title ?? ""
                            };
                        }

                        if (locationData != null)
                        {
                            response.Location = new GetJobsDetailsByIdResponse_DTO.LocationDTO
                            {
                                Id = locationData.Id,
                                Title = locationData.Title ?? "",
                                City = locationData.City ?? "",
                                State = locationData.State ??  "",
                                Country = locationData.Country ?? "",
                                Zip = locationData.Zip ?? 0
                            };
                        }
                        else
                        {
                            ResultWithDataDTO.IsSuccessful = true;
                            ResultWithDataDTO.Message = $"Location Data not found.";
                            _loggerManager.LogInfo(ResultWithDataDTO.Message);
                        }

                        ResultWithDataDTO.Data = response;
                        ResultWithDataDTO.IsSuccessful = true;
                        ResultWithDataDTO.Message = $"Jobs retrieved successfully.";
                        _loggerManager.LogInfo(ResultWithDataDTO.Message);
                    }
                    else
                    {
                        ResultWithDataDTO.IsSuccessful = true;
                        ResultWithDataDTO.Message = $"Department Data not found.";
                        _loggerManager.LogInfo(ResultWithDataDTO.Message);

                    }
                    
                }
                else
                {
                    ResultWithDataDTO.IsSuccessful = true;
                    ResultWithDataDTO.Message = $"Jobs Data not found.";
                    _loggerManager.LogInfo(ResultWithDataDTO.Message);


                }

            }
            catch (Exception ex)
            {
                ResultWithDataDTO.IsBusinessError = true;
                ResultWithDataDTO.BusinessErrorMessage = $"Operation failed: Error observed during Data Retrieval.\nKindly retry or contact System Administrator.";
                ResultWithDataDTO.SystemErrorMessage = $"JobsService=> GetJobsDetails: Exception Message: {ex.Message}\n" +
                    $"Stack Trace: {ex.StackTrace}.\n Inner Exception Message:{ex.InnerException?.Message} with Request Object : {Id}";
                _loggerManager.LogError(ResultWithDataDTO.BusinessErrorMessage);
                if (ResultWithDataDTO.SystemErrorMessage != null)
                {
                    _emailRepository.SendEmail(ResultWithDataDTO.SystemErrorMessage, "GetJobsDetails");
                }
            }
            _loggerManager.LogInfo("Exit JobsService=> GetJobsDetails");
            return ResultWithDataDTO;
        }
    }



    



}
