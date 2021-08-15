using OrderSystem_Peergrade_10.OrderSystem__New_Peergrade_.UserControlSystems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OrderSystem_Peergrade_10.OrderSystem__New_Peergrade_.Users
{
    /// <summary>
    /// Класс клиента.ы
    /// </summary>
    [Serializable]
    abstract class User
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public string Adress { get; set; }
        public Phone Phone { get; set; }
        public Email Email { get; set; }
        public Password Password { get; set; }

        public User(string name, string surname, string patronymic
            , string adress, Phone phone, Email email, Password password)
        {
            Name = name;
            Surname = surname;
            Patronymic = patronymic;
            Adress = adress;
            Phone = phone;
            Email = email;
            Password = password;
        }

        /// <summary>
        /// Проверяет по почте, существует ли такой же клиент.
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        static public bool UserAlreadyExists(Client client)
        {
            return UserControlSystem.Users.Contains(client, new UserComparerByEmail());
        }

        /// <summary>
        /// Получает значения всех свойств. Нужно для панели с клиентами.
        /// </summary>
        /// <returns></returns>
        public List<string> GetAllProperties()
        {
            return new List<string> { Name, Surname, Email.ToString(), Phone.ToString() };
        }

        /// <summary>
        /// Получает все имена свойств в классе. Нужно для панели с клиентами.
        /// </summary>
        /// <returns></returns>
        static public List<string> GetAllPropertiesNames()
        {
            return new List<string>() { "Name", "Surname", "Email", "Phone"};
        }
    }
}
