using MyBooks.Data.Entities;
using System.Collections.Generic;
using System.Linq;

namespace MyBooks.Data.Services
{
    public class LogsService
    {
        private MyDbContext _context;
        public LogsService(MyDbContext context)
        {
            _context = context;
        }

        public List<Log> GetAllLogsFromDB() => _context.Logs.ToList();
    }
}
