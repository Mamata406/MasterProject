using System;
using System.Collections.Generic;
using System.Text;

namespace MasterProjectCommonUtility.Configuration
{
    public class ConnectToDb
    {
        private string _connectionString=string.Empty;

        public string ConnectionString
        {
            get { return _connectionString; }
            set { _connectionString = value; }
        }

        public int ConnectionTimeOutSec { get; set; } = 300;
        public int CommandTimeOutSec { get; set; } = 600;
    }
}
