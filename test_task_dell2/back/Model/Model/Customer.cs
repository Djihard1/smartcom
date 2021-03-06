using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Model.Model
{
    public class Customer
    {

        //Первичный ключ определяющий запись в таблице. Не пустое
       
        public Guid ID { get; set; }

        //Наименование заказчика. Не пустое.
        [Required]
        public string NAME { get; set; }

        // Код заказчика. Содержит данные формата «ХХХХ-ГГГГ» где Х – число, ГГГГ – год в котором зарегистрирован заказчик. Не пустое.
        [Required]
        public string CODE { get; set; }

        //Адрес заказчика
        public string ADDRESS { get; set; }

        //% скидки для заказчика. 0 или null – означает что скидка не распространяется.
        public int? DISCOUNT { get; set; }

        public string Login { get; set; }


        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }

        [Required]
        public string Role { get; set; }


        public virtual ICollection<Order> Orders { get; set; }
      


    }
}
