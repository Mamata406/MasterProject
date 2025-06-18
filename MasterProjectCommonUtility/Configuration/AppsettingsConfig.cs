using System;
using System.Collections.Generic;
using System.Text;

namespace MasterProjectCommonUtility.Configuration
{
    public class AppsettingsConfig
    {
        public MasterProjectData MasterProjectData { get; set; } = new MasterProjectData();
        public EmailSetting EmailSetting { get; set; } = new EmailSetting();
        public bool RequestResponseLoggingEnabled { get; set; }
    }
}