using System;
using System.Collections.Generic;
using System.Text;

namespace Model.HelperClass
{
  public   class ProductR
    {
        public Guid ID { get; set; }
        public Guid orrderItems { get; set; }
        public string CODE { get; set; }
        public string NAME { get; set; }
        public int PRICE { get; set; }
        public string CATEGORY { get; set; }


    }
}
