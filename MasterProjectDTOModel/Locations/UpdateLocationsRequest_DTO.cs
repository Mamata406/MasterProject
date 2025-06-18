using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterProjectDTOModel.Locations
{
    public class UpdateLocationsRequest_DTO
    {
        public string? Title { get; set; }

        public string? City { get; set; }

        public string? State { get; set; }

        public string? Country { get; set; }

        public int? Zip { get; set; }
    }
}
