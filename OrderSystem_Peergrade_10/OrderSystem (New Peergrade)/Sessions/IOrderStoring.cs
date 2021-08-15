using OrderSystem_Peergrade_10.OrderSystem__New_Peergrade_.Orders;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderSystem_Peergrade_10.OrderSystem__New_Peergrade_.Sessions
{
    /// <summary>
    /// Интерфейс показывает, что в классе есть список заказов.
    /// </summary>
    interface IOrderStoring
    {
        public List<Order> Orders { get; set; }
    }
}
