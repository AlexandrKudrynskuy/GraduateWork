using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class OperationDetails
    {
        public string Message { get; set; }
        public bool IsError { get; set; }
        public Exception Exception { get; set; }
    }
}
