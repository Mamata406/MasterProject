using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MasterProjectCommonUtility.Response;
using MasterProjectDTOModel.Locations;

namespace MasterProjectBAL.Locations
{
    public  interface ILocationService
    {
        Task<ResultWithDataDTO<int>> AddLocations(LocationsRequest_DTO request_DTO);
        Task<ResultWithDataDTO<int>> UpdateLocations(int Id, UpdateLocationsRequest_DTO request_DTO);
        Task<ResultWithDataDTO<GetLocationDetailsByIdResponse_DTO>> GetLocationstDetails(int Id);
    }
}
