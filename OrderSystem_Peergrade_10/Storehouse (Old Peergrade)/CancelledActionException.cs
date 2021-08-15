using System;
using System.Collections.Generic;
using System.Text;

namespace OrderSystem_Peergrade_10.Storehouse__Old_Peergrade_
{
    /// <summary>
    /// Исключение, сообщающее, что действие было отменено.
    /// </summary>
    [Serializable]
    public class CancelledActionException : Exception
    {
        // Стандартные методы исключений.
        public CancelledActionException() { }
        public CancelledActionException(string message) : base(message) { }
        public CancelledActionException(string message, Exception inner) : base(message, inner) { }
        protected CancelledActionException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
