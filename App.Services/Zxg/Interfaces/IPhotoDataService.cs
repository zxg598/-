using App.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Services
{
    public interface IPhotoDataService
    {
        public IList<PhotoData> getPhotoDatas(PhotoData  data,out string Message);

        public Boolean CreatePhotoDatas(IList<PhotoData> data,out string Message);

        public Boolean UpdatePhotoDatas(PhotoData data, out string Message);

        public Boolean DeletePhotoDatas(IList<PhotoData> data, out string Message);
    }
}
