using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace OrderSystem_Peergrade_10.OrderSystem__New_Peergrade_.Users
{
    [Serializable]
    class ListViewItemUser : ListViewItem
    {
        public User User { get; set; }

        public ListViewItemUser(User user) : base(user.GetAllProperties().ToArray())
        {
            User = user;
        }
    }
}
