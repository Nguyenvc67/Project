using System;
using System.Collections.Generic;

namespace Model
{
    public static class OrderStatus
    {
        public const int CREATE_NEW_ORDER = 1;
        public const int ORDER_INPROGRESS = 2;
    }
    public class Order
    {
        public int OrderId { set; get; }
        public DateTime OrderDate { set; get; }
        public Customer OrderCustomer { set; get; }
        public int Status { set; get; }
        public List<Tabacco> TabaccosList { set; get; }

        public Tabacco? this[int index]
        {
            get
            {
                if (TabaccosList == null || TabaccosList.Count == 0 || index < 0 || TabaccosList.Count < index) return null;
                return TabaccosList[index];
            }
            set
            {
                if (TabaccosList == null) TabaccosList = new List<Tabacco>();
                if (value != null) TabaccosList.Add(value);
            }
        }

        public Order()
        {
            TabaccosList = new List<Tabacco>();
            OrderCustomer = default!;
        }
    }
}
