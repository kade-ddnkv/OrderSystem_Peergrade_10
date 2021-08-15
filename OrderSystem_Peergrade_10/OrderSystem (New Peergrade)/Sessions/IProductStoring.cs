using OrderSystem_Peergrade_10.Storehouse__Old_Peergrade_;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderSystem_Peergrade_10.OrderSystem__New_Peergrade_.Sessions
{
    /// <summary>
    /// Интерфейс показывает, что в классе есть список продуктов.
    /// </summary>
    interface IProductStoring
    {
        public List<Product> Products { get; set; }
    }
}
