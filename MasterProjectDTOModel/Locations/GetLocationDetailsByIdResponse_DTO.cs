using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterProjectDTOModel.Locations
{
    public  class GetLocationDetailsByIdResponse_DTO
    {
        public int Id { get; set; }

        public string? Title { get; set; }

        public string? City { get; set; }

        public string? State { get; set; }

        public string? Country { get; set; }

        public int? Zip { get; set; }
    }
}
