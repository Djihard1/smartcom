using Model.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.HelperClass.MyOrder
{
  public  class OrderGetAll
    {
        public Guid ID { get; set; }

        //Дата когда сделан заказ. Не пустое
        
        public string ORDER_DATE { get; set; }

        //Дата доставки
        
        public string SHIPMENT_DATE { get; set; }

        //Номер заказа
        public int? ORDER_NUMBER { get; set; }

        //Состояние заказа
        public string STATUS { get; set; }

        public int total { get; set; }

        // [Required]
        public virtual Customer Customer { get; set; }

        public virtual ICollection<OrderItem> OrrderItems { get; set; }
    }
}
