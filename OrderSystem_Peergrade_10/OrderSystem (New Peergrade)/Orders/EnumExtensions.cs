using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OrderSystem_Peergrade_10.OrderSystem__New_Peergrade_.Orders
{
    /// <summary>
    /// Расширение для перечислений.
    /// </summary>
    static class OrderStatusEnumExtensions
    {
        /// <summary>
        /// Возвращает флаги, содержащиеся в данном экземпляре перечисления.
        /// </summary>
        /// <param name="currentEnum"></param>
        /// <returns></returns>
        public static OrderStatus[] GetFlags(this OrderStatus currentEnum)
        {
            return Enum.GetValues(typeof(OrderStatus))
                .Cast<OrderStatus>()
                .Where(v => currentEnum.HasFlag(v))
                .ToArray();
        }
    }
}
