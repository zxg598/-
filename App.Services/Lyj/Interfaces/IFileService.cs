using App.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace App.Services
{
    public interface IFileService
    {
        public string ExportExcel(string templatePath, DataTable dataSource, string outFileName, DataTable para = null);
    }
}
