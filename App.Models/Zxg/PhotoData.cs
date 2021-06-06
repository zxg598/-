using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace App.Models
{
    [Table("PhotoData")]
    public   class PhotoData
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public int StoreId { get; set; }

        public string FilePath { get; set; }
        /// <summary>
        /// 照片类型
        /// </summary>
        public int Type { get; set; }

    }
}
