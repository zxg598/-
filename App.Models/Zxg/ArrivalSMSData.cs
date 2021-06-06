using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace App.Models
{
    [Table("ArrivalSMSData")]
    public class ArrivalSMSData
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public int StoreId { get; set; }
        
        public DateTime? SMSTime { get; set; }
        
        public string Name { get; set; }

        public int SMSType { get; set; }
        public string Details { get; set; }
        public int SMSStatus { get; set; }

    }
}
