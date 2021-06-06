using App.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Services
{
    public interface IStaffManagementDataService
    {
        public IList<StaffManagementData> getStaffManagementDatas(StaffManagementData data,out string Message);

        public Boolean CreateStaffManagementDatas(IList<StaffManagementData> data,out string Message);

        public Boolean UpdateStaffManagementData(StaffManagementData data, out string Message);

        public Boolean DeleteStaffManagementDatas(IList<StaffManagementData> data, out string Message);
    }
}
