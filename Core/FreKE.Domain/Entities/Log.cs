using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreKE.Domain.Entities
{
    public class Log
    {
        public Guid Id { get; set; }
        public DateTime ProcessTime { get; }
        public string LogLevel { get; set; }
        public string Endpoint { get; set; }
        public Guid? UserId { get; set; } = null;
        public string Request { get; set; }
        public string ActionMethod { get; set; }
        public Log()
        {
            ProcessTime = DateTime.Now;
        }
    }
}
