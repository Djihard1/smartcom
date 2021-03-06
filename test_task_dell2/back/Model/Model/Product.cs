using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Model.Model
{
    public class Product
    {

        //Первичный ключ определяющий запись в таблице. Не пустое
        public Guid ID { get; set; }

        //Код товара, формат «XX-XXXX-YYXX» где Х – число , Y- заглавная буква английского алфавита. Не пустое.
        [Required]
        public string CODE { get; set; }

        //Наименование товара
        public string NAME { get; set; }

        //Цена за единицу
        public int PRICE { get; set; }

        //Категория товара.
        [MaxLength(30)]
        public string CATEGORY { get; set; }
        //Img
        
        public object ImageFile { get; set; }

        public virtual ICollection<OrderItem> OrrderItems { get; set; }

    }
}
