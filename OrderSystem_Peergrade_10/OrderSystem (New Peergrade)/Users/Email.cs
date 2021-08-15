using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Text.RegularExpressions;

namespace OrderSystem_Peergrade_10.OrderSystem__New_Peergrade_.Users
{
    [Serializable]
    class Email
    {
        string emailString;

        public Email(string emailString)
        {
            this.emailString = emailString;
        }

        static string emailPattern = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";
        static Regex rg = new Regex(emailPattern);

        static public bool IsValid(string emailString)
        {
            return rg.IsMatch(emailString);
        }

        public override string ToString()
        {
            return emailString;
        }

        public override bool Equals(object obj)
        {
            return emailString == (obj as Email).emailString;
        }
    }
}
