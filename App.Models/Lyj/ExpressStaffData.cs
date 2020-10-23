using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace App.Models
{
    [Table("ExpressStaffData")]
    public   class ExpressStaffData
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public int StoreId { get; set; }

        public int CompanyId { get; set; }


        public string Name { get; set; }

        public string PhoneNum { get; set; }

        public int DefaultType { get; set; }

        public string Remarks { get; set; }

    }
}
