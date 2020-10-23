using App.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Services
{
    public interface IRetentionTimeDataService
    {
        public IList<RetentionTimeData> getRetentionTimeDatas(RetentionTimeData data,out string Message);

        public Boolean CreateRetentionTimeDatas(IList<RetentionTimeData> data,out string Message);

        public Boolean UpdateRetentionTimeData(RetentionTimeData data, out string Message);

        public Boolean DeleteRetentionTimeDatas(IList<RetentionTimeData> data, out string Message);
    }
}
