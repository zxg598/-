using App.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Services
{
    public interface IStoreDataService
    {
        public IList<StoreData> getStoreDatas(StoreData data,out string Message);

        public Boolean CreateStoreDatas(IList<StoreData> data,out string Message);

        public Boolean UpdateStoreData(StoreData data, out string Message);

        public Boolean DeleteStoreDatas(IList<StoreData> data, out string Message);
    }
}
