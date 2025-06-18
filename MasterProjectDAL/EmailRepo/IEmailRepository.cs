using System;
using System.Collections.Generic;
using System.Text;

namespace MasterProjectDAL.EmailRepo
{
    public interface IEmailRepository
    {
        void SendEmail(string emailbody, string Subject);
    }
}
