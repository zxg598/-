using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace App.Models
{
    [Table("StoreData ")]
    public   class StoreData
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
         
        public string  StoreName { get; set; }

        public string  PickUpAddress { get; set; }

        public DateTime?  OpeningTime { get; set; }
        public DateTime? ClosingTime { get; set; }
        public string  PhoneNum { get; set; }
        public string  Area { get; set; }
        public string Address { get; set; }
    }
}
