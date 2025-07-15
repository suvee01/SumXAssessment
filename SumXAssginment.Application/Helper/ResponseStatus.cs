using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SumXAssginment.Application.Helper
{
    public class ResponseStatus<t> where t : class
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public t Data { get; set; }
    }
}
