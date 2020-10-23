using App.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Services
{
    public interface IExpresscompanyDataService
    {
        public IList<ExpresscompanyData> getExpresscompanyDatas(ExpresscompanyData  data,out string Message);

        public Boolean CreateExpresscompanyDatas(IList<ExpresscompanyData> data,out string Message);

        public Boolean UpdateExpresscompanyDatas(ExpresscompanyData data, out string Message);

        public Boolean DeleteExpresscompanyDatas(IList<ExpresscompanyData> data, out string Message);
    }
}
