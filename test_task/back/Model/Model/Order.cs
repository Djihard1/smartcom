using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Model.Model
{
    public class Order
    {
        //Первичный ключ определяющий запись в таблице. Не пустое
        
        public Guid ID { get; set; }

        //Дата когда сделан заказ. Не пустое
        [Required]
        public DateTime ORDER_DATE { get; set; }

        //Дата доставки
        public DateTime? SHIPMENT_DATE { get; set; }

        //Номер заказа
        public int? ORDER_NUMBER { get; set; }

        //Состояние заказа
        public string STATUS { get; set; }

       // [Required]
        public virtual Customer Customer { get; set; }

        public virtual ICollection<OrderItem> OrrderItems { get; set; }



    }
}
