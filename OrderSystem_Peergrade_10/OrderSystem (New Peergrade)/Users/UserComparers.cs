using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace OrderSystem_Peergrade_10.OrderSystem__New_Peergrade_.Users
{
    class UserComparerByEmail : IEqualityComparer<User>
    {
        public bool Equals([AllowNull] User x, [AllowNull] User y)
        {
            return x.Email.Equals(y.Email);
        }

        public int GetHashCode([DisallowNull] User obj)
        {
            return obj.Name.GetHashCode();
        }
    }
}
