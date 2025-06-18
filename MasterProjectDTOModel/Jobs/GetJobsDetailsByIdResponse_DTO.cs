using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterProjectDTOModel.Jobs
{
   
        public class GetJobsDetailsByIdResponse_DTO
        {
            public int Id { get; set; }
            public string Code { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }

            public LocationDTO Location { get; set; }
            public DepartmentDTO Department { get; set; }

            public DateTime PostedDate { get; set; }
            public DateTime ClosingDate { get; set; }

            public class LocationDTO
            {
                public int Id { get; set; }
                public string Title { get; set; }
                public string City { get; set; }
                public string State { get; set; }
                public string Country { get; set; }
                public int Zip { get; set; }
            }

            public class DepartmentDTO
            {
                public int Id { get; set; }
                public string Title { get; set; }
            }
        }

    }
