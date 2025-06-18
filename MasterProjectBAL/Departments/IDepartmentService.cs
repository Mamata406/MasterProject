using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MasterProjectCommonUtility.Response;
using MasterProjectDTOModel.Departments;

namespace MasterProjectBAL.Departments
{
    public  interface IDepartmentService
    {
        Task<ResultWithDataDTO<int>> AddDepartments(DepartmentsRequest_DTO request_DTO);
        Task<ResultWithDataDTO<int>> UpdateDepartments(int Id, UpdateDepartementsRequest_DTO request_DTO);
        Task<ResultWithDataDTO<GetDepartemntsDetailsByIdResponse_DTO>> GetDepartmentDetails(int Id);
    }
}
