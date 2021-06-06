using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace App.Models
{
    [Table("StaffManagementData")]
    public   class StaffManagementData
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public int StoreId { get; set; }

        public string  PhoneNum { get; set; }

        public string  Password { get; set; }
        public string Nickname { get; set; }
    }
}
