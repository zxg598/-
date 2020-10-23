using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppApi.Models
{
    public class ApiResult<T> where T:class
    {
        public Boolean IsOK { get; set; } = true ;
        public string ErrorMessage { get; set; }

        public List<T> ApiData { get; set; } 

        public string FilePath { get; set; }
    }
}
