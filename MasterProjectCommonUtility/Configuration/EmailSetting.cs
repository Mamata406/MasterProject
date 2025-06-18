using System;
using System.Collections.Generic;
using System.Text;

namespace MasterProjectCommonUtility.Configuration
{
    public class EmailSetting
    {
        public string Secret { get; set; }
        public string SmtpHost { get; set; }
        public int SmtpPort { get; set; }
        public string SmtpUser { get; set; }
        public string SmtpPass { get; set; }
        public string Sender { get; set; }

    }
}
