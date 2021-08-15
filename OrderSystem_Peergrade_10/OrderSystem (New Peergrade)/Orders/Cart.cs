using OrderSystem_Peergrade_10.OrderSystem__New_Peergrade_.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderSystem_Peergrade_10.OrderSystem__New_Peergrade_.Orders
{
    /// <summary>
    /// Подкласс заказа - корзина, в которую можно класть продукты и потом оформлять заказы.
    /// </summary>
    [Serializable]
    class Cart : Order
    {
        public Cart(Client client) : base(client)
        {
        }

        /// <summary>
        /// Переопределяет возвращение свойств класса Order.
        /// </summary>
        /// <returns></returns>
        public override List<string> GetAllProperties()
        {
            return new List<string>() { "Cart", "-", "-", "-" };
        }
    }
}
