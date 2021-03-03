using Model.HelperClass.MyOrder;
using Model.Model;

using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Model.HelperClass
{
  public  class MyOrders
    {
        public string OrderDate;
        public string ShipmentDate;
        public string Status;
        public int? OrderNumber;
        public int Total;
        public Guid Id;
        public List<Orders> order { get; set; }
    }
}
