using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace OrderSystem_Peergrade_10.OrderSystem__New_Peergrade_.Users
{
    [Serializable]
    class Password
    {
        string pwdString;

        public Password(string pwdString)
        {
            this.pwdString = pwdString;
        }

        static public bool IsValid(string pwdString)
        {
            if ((pwdString.Trim().Length != pwdString.Length) || (pwdString.Length < 3))
            {
                return false;
            }
            return true;
        }

        public override bool Equals(object obj)
        {
            return pwdString == (obj as Password).pwdString;
        }
    }
}
