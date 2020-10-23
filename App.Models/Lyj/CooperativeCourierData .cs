using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace App.Models
{
    [Table("CooperativeCourierData ")]
    public   class CooperativeCourierData
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public int StoreId { get; set; }

        public int ExpressCompanyId { get; set; }

        public int CourierId { get; set; }

        public decimal UnitPrice { get; set; }
        public int CollectionNum { get; set; }
        public decimal LineOfCredit { get; set; }
        public int DefaultType { get; set; }

    }
}
