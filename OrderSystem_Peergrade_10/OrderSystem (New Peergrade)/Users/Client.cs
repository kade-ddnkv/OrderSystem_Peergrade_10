using OrderSystem_Peergrade_10.OrderSystem__New_Peergrade_.Orders;
using OrderSystem_Peergrade_10.OrderSystem__New_Peergrade_.Sessions;
using OrderSystem_Peergrade_10.OrderSystem__New_Peergrade_.UserControlSystems;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

namespace OrderSystem_Peergrade_10.OrderSystem__New_Peergrade_.Users
{
    /// <summary>
    /// Класс пользователя. Подклассы - клиент и продавец.
    /// </summary>
    [Serializable]
    class Client : User, IOrderStoring
    {
        public Cart Cart { get; set; }
        public List<Order> Orders { get; set; }

        public delegate void AddOrderHandler();
        public static event AddOrderHandler OrdersChanged;

        public Client(string name, string surname, string patronymic
            , string adress, Phone phone, Email email, Password password)
            : base(name, surname, patronymic, adress, phone, email, password)
        {
            Name = name;
            Surname = surname;
            Patronymic = patronymic;
            Adress = adress;
            Phone = phone;
            Email = email;
            Password = password;

            Cart = new Cart(this);
            Orders = new List<Order>();
        }

        /// <summary>
        /// Добавляет заказ клиенту.
        /// </summary>
        /// <param name="order"></param>
        public void AddOrder(Order order)
        {
            Orders.Add(order);
            OrdersChanged?.Invoke();
        }

        /// <summary>
        /// Делает заказ из корзины.
        /// </summary>
        public void PlaceOrderFromCart()
        {
            AddOrder(new Order(this, Cart.Products));
            Cart.Products.Clear();
        }

        /// <summary>
        /// Клиент оплачивает обработанный заказ.
        /// </summary>
        /// <param name="order"></param>
        public void PayForOrder(Order order)
        {
            Order selectedOrder = Orders.Find((other) => other == order);
            selectedOrder.Status |= OrderStatus.Paid;
        }
    }
}
