using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Model.Model
{
    public class OrderItem
    {

        //Guid	Первичный ключ определяющий запись в таблице. Не пустое
        
        public Guid ID { get; set; }

        //Количество заказанного товара. Не пустое.
        [Required]
        public int ITEMS_COUNT { get; set; }

        //Цена  за единицу. Не пустое.
        [Required]
        public int ITEM_PRICE { get; set; }

        //ИД заказа. Не пустое
        [Required]
        public virtual Order Order { get; set; }
        //Количество заказанного товара. Не пустое.
        [Required]
        public virtual Product Product { get; set; }

    }
}
