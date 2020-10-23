using App.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Services
{
    public interface ICooperativeCourierDataService
    {
        public IList<CooperativeCourierData> getCooperativeCourierDatas(CooperativeCourierData data,out string Message);

        public Boolean CreateCooperativeCourierDatas(IList<CooperativeCourierData> data,out string Message);

        public Boolean UpdateCooperativeCourierData(CooperativeCourierData data, out string Message);

        public Boolean DeleteCooperativeCourierDatas(IList<CooperativeCourierData> data, out string Message);
    }
}
