using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterProjectDTOModel.Jobs
{
    public  class GetJobsDetailsByListResponse_DTO
    {
        public int Id { get; set; }
        public string? Code { get; set; }
        public string? Title { get; set; }
        public string? Location { get; set; }
        public string? Department { get; set; }
        public DateTime? PostedDate { get; set; }

        public DateTime? ClosingDate { get; set; }
    }
  
}
