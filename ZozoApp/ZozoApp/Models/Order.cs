using System;
using System.Collections.Generic;
using System.Text;

namespace ZozoApp.Models
{
    public class Order
    {
        public int id { get; set; }
        public string FullName { get; set; }
        public string address { get; set; }
        public string phone { get; set; }
        public double orderTotal { get; set; }
        public DateTime OrderPlaced { get; set; }
        public bool isOrderCompleted { get; set; }
        public int UserId { get; set; }
        public List<OrderDetail> orderDetails { get; set; }
    }
}
