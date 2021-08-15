using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Runtime.Serialization;
using System.Linq;
using OrderSystem_Peergrade_10.OrderSystem__New_Peergrade_.Sessions;

// Этот файл отвечает за класс Directory. (Также отвечает за его сериализацию/десериализацию).

namespace OrderSystem_Peergrade_10.Storehouse__Old_Peergrade_
{
    /// <summary>
    /// Класс директории (сериализуемый).
    /// </summary>
    [Serializable]
    public class Directory : TreeNode, IProductStoring
    {
        /// <summary>
        /// Добавленный к treeNode список продуктов.
        /// </summary>
        public List<Product> Products { get; set; } = new List<Product>();

        //Простые конструкторы.
        public Directory() : base() { }
        public Directory(string text) : base(text) { }
        public Directory(TreeNode treeNode, List<Product> products) : base(treeNode.Text, treeNode.Nodes.Cast<TreeNode>().ToArray())
        {
            Products = products;
        }
        public Directory(string text, TreeNode[] children) : base(text, children) { }

        /// <summary>
        /// Этот конструктор автоматически вызывается при десериализации.
        /// </summary>
        /// <param name="si"></param>
        /// <param name="context"></param>
        protected Directory(SerializationInfo si, StreamingContext context) : base(si, context)
        {
            Products = (List<Product>)si.GetValue("Products", typeof(List<Product>));
        }

        /// <summary>
        /// Клонирует директорию (клонирует treeNode + список товаров).
        /// </summary>
        /// <returns></returns>
        public override object Clone()
        {
            return new Directory(base.Clone() as TreeNode, Products);
        }

        /// <summary>
        /// Автоматически вызывается при сериализации директории.
        /// </summary>
        /// <param name="si"></param>
        /// <param name="context"></param>
        protected override void Serialize(SerializationInfo si, StreamingContext context)
        {
            base.Serialize(si, context);
            si.AddValue("Products", Products);
        }
    }
}
