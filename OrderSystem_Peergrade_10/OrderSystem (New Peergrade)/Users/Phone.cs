using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace OrderSystem_Peergrade_10.OrderSystem__New_Peergrade_.Users
{
    [Serializable]
    class Phone
    {
        string phoneString;

        public Phone (string phoneString)
        {
            this.phoneString = phoneString;
        }

        static string phonePattern = @"^((8|\+7)[\- ]?)?(\(?\d{3}\)?[\- ]?)?[\d\- ]{7,10}$";

        static public bool IsValid(string phoneString)
        {
            return new Regex(phonePattern).IsMatch(phoneString);
        }

        public override string ToString()
        {
            return phoneString;
        }
    }
}
