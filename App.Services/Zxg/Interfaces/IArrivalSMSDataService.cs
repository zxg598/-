using App.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Services
{
    public interface IArrivalSMSDataService
    {
        public IList<ArrivalSMSData> getArrivalSMSDatas(ArrivalSMSData  data,out string Message);

        public Boolean CreateArrivalSMSDatas(IList<ArrivalSMSData> data,out string Message);

        public Boolean UpdateArrivalSMSDatas(ArrivalSMSData data, out string Message);

        public Boolean DeleteArrivalSMSDatas(IList<ArrivalSMSData> data, out string Message);
    }
}
