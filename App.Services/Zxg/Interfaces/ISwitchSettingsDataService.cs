using App.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Services
{
    public interface ISwitchSettingsDataService
    {
        public IList<SwitchSettingsData> getSwitchSettingsDatas(SwitchSettingsData data,out string Message);

        public Boolean CreateSwitchSettingsDatas(IList<SwitchSettingsData> data,out string Message);

        public Boolean UpdateSwitchSettingsData(SwitchSettingsData data, out string Message);

        public Boolean DeleteSwitchSettingsDatas(IList<SwitchSettingsData> data, out string Message);
    }
}
