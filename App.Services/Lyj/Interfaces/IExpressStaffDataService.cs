using App.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Services
{
    public interface IExpressStaffDataService
    {
        public IList<ExpressStaffData> getExpressStaffDatas(ExpressStaffData data,out string Message);

        public Boolean CreateExpressStaffDatas(IList<ExpressStaffData> data,out string Message);

        public Boolean UpdateExpressStaffData(ExpressStaffData data, out string Message);

        public Boolean DeleteExpressStaffDatas(IList<ExpressStaffData> data, out string Message);
    }
}
