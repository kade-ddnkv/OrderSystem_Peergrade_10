using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace OrderSystem_Peergrade_10.OrderSystem__New_Peergrade_.Orders
{/// <summary>
/// Класс, наследуемый от ListViewItem, нужен для встраивания Order в ListView.
/// </summary>
    class ListViewItemOrder : ListViewItem
    {
        public Order Order { get; set; }

        public ListViewItemOrder(Order order) : base(order.GetAllProperties().ToArray())
        {
            Order = order;
        }
    }
}
