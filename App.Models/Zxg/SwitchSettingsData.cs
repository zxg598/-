using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace App.Models
{
    [Table("SwitchSettingsData")]
    public   class SwitchSettingsData
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public int StoreId { get; set; }

        public int? InputSendMessageType { get; set; }


        public int? NotificationType { get; set; }

        public int? SMSpickupType { get; set; }

        public int? TakeRemindType { get; set; }

        public int? SMSSendingFailedType { get; set; }


        public int? NoShelfType { get; set; }

        public int? NumberDigitsSixType { get; set; }

        public int? SendNotificationType { get; set; }

        public int? InTakePhotosType { get; set; }

        public int? ArrivalDeliveryType { get; set; }

        public int? ArrivalSignInType { get; set; }

        public int? CashOnDeliveryType { get; set; }
         

    }
}
