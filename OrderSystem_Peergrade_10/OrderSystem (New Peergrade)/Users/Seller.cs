using System;
using System.Collections.Generic;
using System.Text;

namespace OrderSystem_Peergrade_10.OrderSystem__New_Peergrade_.Users
{
    [Serializable]
    class Seller : User
    {
        public Seller(string name, string surname, string patronymic
            , string adress, Phone phone, Email email, Password password)
            : base(name, surname, patronymic, adress, phone, email, password)
        {
        }
    }
}
