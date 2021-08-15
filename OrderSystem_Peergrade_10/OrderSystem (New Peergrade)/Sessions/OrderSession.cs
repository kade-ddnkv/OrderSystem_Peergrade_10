using OrderSystem_Peergrade_10.OrderSystem__New_Peergrade_.Orders;
using OrderSystem_Peergrade_10.Storehouse__Old_Peergrade_;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OrderSystem_Peergrade_10.OrderSystem__New_Peergrade_.Sessions
{
    /// <summary>
    /// Сессия, нужная для сохранения заказа в "оперативной" памяти.
    /// </summary>
    class OrderSession
    {
        ListView.ListViewItemCollection ListViewOrders;
        public IOrderStoring Store { get; set; }
        List<Order> Orders
        {
            get
            {
                return Store.Orders;
            }
        }

        public OrderSession(ListView.ListViewItemCollection listViewItemCollection)
        {
            ListViewOrders = listViewItemCollection;
        }

        /// <summary>
        /// Сессия "запоминает" ссылку на склад заказов.
        /// </summary>
        /// <param name="store"></param>
        public void SetOrdersStore(IOrderStoring store)
        {
            Store = store;
        }
    }
}
