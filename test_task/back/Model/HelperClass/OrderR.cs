using System;
using System.Collections.Generic;
using System.Text;

namespace Model.HelperClass
{
   public class OrderR
    {
      public  Guid ProductId { get; set; }
      public Guid CustomerId { get; set; }
      public DateTime Shipment_date { get; set; }
      public int Item_count { get; set; }

    }
}
