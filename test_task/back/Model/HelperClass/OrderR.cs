using System;
using System.Collections.Generic;
using System.Text;

namespace Model.HelperClass
{
   public class OrderR
    {
        public Guid ID { get; set; }
        public Guid customerId { get; set; }
        public string CODE { get; set; }
        public string NAME { get; set; }
        public int PRICE { get; set; }
        public string CATEGORY { get; set; }

        //public int quality;


    }
}
