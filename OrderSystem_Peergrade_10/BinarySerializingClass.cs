using OrderSystem_Peergrade_10.OrderSystem__New_Peergrade_.Users;
using OrderSystem_Peergrade_10.Storehouse__Old_Peergrade_;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderSystem_Peergrade_10
{
    /// <summary>
    /// Это сериализуемый класс, нужен для одновременного сохранения в одном файле всей информациию
    /// </summary>
    [Serializable]
    class BinarySerializingClass
    {
        // Директории с товарами.
        public Directory WholeTreeDir { get; set; }
        // Клиенты с товарами.
        public List<User> Users { get; set; }
        public User CurrentUser { get; set; }

        public BinarySerializingClass(Directory wholeTreeDir, List<User> users, User currentUser)
        {
            WholeTreeDir = wholeTreeDir;
            Users = users;
            CurrentUser = currentUser;
        }
    }
}
