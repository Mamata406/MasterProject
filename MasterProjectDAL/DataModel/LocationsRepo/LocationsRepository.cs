using MasterProjectCommonUtility.Logger;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterProjectDAL.DataModel.LocationsRepo
{
    public  class LocationsRepository : ILocationsRepository
    {

        private IMasterProjectContext _context;
        private ILoggerManager _loggerManager;

        public LocationsRepository(IMasterProjectContext context, ILoggerManager loggerManager)
        {
            _context = context;
            _loggerManager = loggerManager;
        }
        public async Task<Locations> AddLocation(Locations loc)
        {
            _loggerManager.LogInfo("Entry LocationsRepository=> AddLocation");
            await _context.Locations.AddAsync(loc);
            await _context.SaveChangesAsync();
            _loggerManager.LogInfo("Exit LocationsRepository=> AddLocation");
            return loc;
        }
        public async Task<Locations> UpdateLocation(Locations loc)
        {
            _loggerManager.LogInfo("Entry LocationsRepository=> UpdateLocation");
            if (loc != null)
            {

                _context.Locations.Update(loc);
                await _context.SaveChangesAsync();
            }
            _loggerManager.LogInfo("Exit LocationsRepository=> UpdateLocation");
            return loc;
        }
        public async Task<Locations> GetLocationById(int locId)
        {
            return await _context.Locations.FirstOrDefaultAsync(x => x.Id == locId);

        }
    }
}
