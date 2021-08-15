using OrderSystem_Peergrade_10.Storehouse__Old_Peergrade_;
using OrderSystem_Peergrade_10.OrderSystem__New_Peergrade_.Users;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using OrderSystem_Peergrade_10.OrderSystem__New_Peergrade_.Sessions;

namespace OrderSystem_Peergrade_10.OrderSystem__New_Peergrade_.Orders
{
    /// <summary>
    /// Order - заказ.
    /// </summary>
    [Serializable]
    class Order : IProductStoring
    {
        public List<Product> Products { get; set; }
        Client Client { get; set; }
        uint Id { get; set; }
        DateTime CreationDate { get; set; }
        public OrderStatus Status { get; set; }

        // Так как заказы не удаляются, можно добавлять их в общую копилку при создании.
        static public List<Order> AllOrders { get; set; } = new List<Order>();

        /// <summary>
        /// Выдает свободный айдишник для заказа (может выдать и меньше текущего максимального).
        /// </summary>
        static uint FreeNumber
        {
            get
            {
                // Каждый раз для поиска свободного айдишника я прохожу по всем уже существующим.
                uint expectedNumber = 1;
                bool freeNumberFound = false;
                List<uint> numbersList = AllOrders.Select(order => order.Id).ToList();
                while(!freeNumberFound)
                {
                    if (numbersList.Contains(expectedNumber))
                    {
                        expectedNumber++;
                    }
                    else
                    {
                        freeNumberFound = true;
                    }
                }
                return expectedNumber;
            }
        }

        /// <summary>
        /// Простой конструктор.
        /// </summary>
        /// <param name="client"></param>
        public Order(Client client)
        {
            Products = new List<Product>();
            Client = client;
            CreationDate = DateTime.Now;
            Status = 0;
            Id = FreeNumber;

            if (!(this is Cart))
            {
                AllOrders.Add(this);
            }
        }

        public Order(Client client, List<Product> products) : this(client)
        {
            Products.AddRange(products);
        }

        public string IdString
            => $"#{Id}";

        public string CreationDateString
            => $"{CreationDate}";

        public string StatusString
        {
            get
            {
                var flags = Status.GetFlags().Select(flag => Enum.GetName(typeof(OrderStatus), flag));
                return string.Join(" + ", flags);
            }
        }

        /// <summary>
        /// Метод, выдающий названия всех свойств в классе (нужен для столбцов на панели заказов).
        /// </summary>
        /// <returns></returns>
        public static List<string> GetAllPropertiesNames()
        {
            return new List<string>() { "Name", "Creation Date", "Status", "Client Email"};
        }

        /// <summary>
        /// Метод, выдающий значения всех свойств экземпляра класса.
        /// </summary>
        /// <returns></returns>
        public virtual List<string> GetAllProperties()
        {
            return new List<string>() { IdString, CreationDateString, StatusString, Client.Email.ToString() };
        }

        /// <summary>
        /// Добавляет флаг к статусу заказа.
        /// </summary>
        /// <param name="orderStatus"></param>
        public void AddStatusFlag(OrderStatus orderStatus)
        {
            Status |= orderStatus;
        }

        /// <summary>
        /// Отвечает, готов ли этот заказ сейчас получить данный флаг (подразумаевается, следующий).
        /// </summary>
        /// <param name="orderStatus"></param>
        /// <returns></returns>
        public bool ReadyForStatusFlag(OrderStatus orderStatus)
        {
            return Math.Pow(2, Status.GetFlags().Length) == (int)orderStatus;
        }
    }

    /// <summary>
    /// Перечисление флагов статусов для заказов.
    /// </summary>
    enum OrderStatus
    {
        Processed = 1,
        Paid = 2,
        Shipped = 4,
        Executed = 8
    }
}
