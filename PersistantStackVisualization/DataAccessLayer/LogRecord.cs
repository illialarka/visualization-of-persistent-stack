using System;

namespace DataAccessLayer
{
    public class LogRecord
    {
        public int Id { get; set; }

        public LogRecord()
        {
            DateTime = DateTime.UtcNow;
        }

        public string Action { get; set; }

        public DateTime DateTime { get; set; }

        public string Message { get; set; }
    }
}