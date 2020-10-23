using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace App.Models
{
    [Table("RetentionTimeData")]
    public   class RetentionTimeData
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public int StoreId { get; set; }

        public int ExpressCompanyId { get; set; }

        public int RemainingPartsDays { get; set; }

    }
}
