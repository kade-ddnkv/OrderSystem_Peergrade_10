using OrderSystem_Peergrade_10.OrderSystem__New_Peergrade_.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderSystem_Peergrade_10.OrderSystem__New_Peergrade_.UserControlSystems
{
    /// <summary>
    /// Важный класс, отвечает за управление системой клиентов и админа в программе.
    /// </summary>
    [Serializable]
    class UserControlSystem
    {
        static public User admin = new Seller("admin", "admin", "admin", "admin"
            , new Phone("+88888888888"), new Email("ad@min.com"), new Password("1234"));
        static public List<User> Users { get; set; } = new List<User>() { admin };
        static public User CurrentUser { get; set; }

        /// <summary>
        /// Находит пользователя в списке по почте. Не исключает почту админа.
        /// </summary>
        /// <param name="emailString"></param>
        /// <returns></returns>
        static public User FindUserByEmail(string emailString)
        {
            return Users.Find((user) => user.Email.Equals(new Email(emailString)));
        }

        /// <summary>
        /// Проверяет валидность восстановленных из файла данных.
        /// </summary>
        static public void ValidateRestoredData()
        {
            if (CurrentUser == null)
            {
                SetCurrentUser();
                Users.Clear();
                Users.Add(admin);
            }
        }

        /// <summary>
        /// Заставлет пользователя залогиниться. (Не закрывается, пока не.)
        /// </summary>
        static public void SetCurrentUser()
        {
            var regForm = new UserControlSystemForm();
            regForm.ShowDialog();
        }
    }
}
