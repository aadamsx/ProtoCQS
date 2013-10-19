using System;

namespace DataModel
{
    public class ConnectionConfiguration
    {
        public string MachineName { get; set; }
        public string Setting { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
        public string Provider { get; set; }
        public Byte[] RowVersion { get; set; }
    }
}
