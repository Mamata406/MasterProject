using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterProjectDAL.DataModel.LocationsRepo
{
    public  interface ILocationsRepository
    {
        Task<Locations> AddLocation(Locations loc);
        Task<Locations> UpdateLocation(Locations loc);
        Task<Locations> GetLocationById(int locId);
    }
}
