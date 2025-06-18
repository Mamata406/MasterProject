using AutoMapper;
using MasterProjectCommonUtility.Paging;
using MasterProjectDAL.DataModel;
using MasterProjectDAL.ViewModel;
using MasterProjectDTOModel.Departments;
using MasterProjectDTOModel.Jobs;
using MasterProjectDTOModel.Locations;


namespace MasterProjectWebAPI.AutoMapperProfile
{
    public class MapperProfileDeclaration : Profile
    {
        public MapperProfileDeclaration() 
        {
          

            #region Jobs
            CreateMap<JobsRequest_DTO, Jobs>();
            CreateMap<UpdateJobsRequest_DTO, Jobs>();
            CreateMap<Jobs, GetJobsDetailsByIdResponse_DTO>();   
            CreateMap<PagingParameter, PageList>();
            #endregion

            #region Departments
            CreateMap<DepartmentsRequest_DTO, Departments>();
            CreateMap<UpdateDepartementsRequest_DTO, Departments>();
            CreateMap<Departments, GetDepartemntsDetailsByIdResponse_DTO>();
            CreateMap<PagingParameter, PageList>();
            #endregion

            #region  Locations
            CreateMap<LocationsRequest_DTO, Locations>();
            CreateMap<UpdateLocationsRequest_DTO, Locations>();
            CreateMap<Locations, GetLocationDetailsByIdResponse_DTO>();
            CreateMap<PagingParameter, PageList>();
            #endregion

        }
    }
}
