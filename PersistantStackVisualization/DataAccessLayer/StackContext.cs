using System.Data.Entity;

namespace DataAccessLayer
{
    public class StackContext : DbContext
    {
        public StackContext() : base("DefaultConnection")
        {
        }

        public IDbSet<LogRecord> Records { get; set; }
    }
}