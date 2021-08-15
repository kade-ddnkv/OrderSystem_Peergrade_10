using OrderSystem_Peergrade_10.Storehouse__Old_Peergrade_;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OrderSystem_Peergrade_10.OrderSystem__New_Peergrade_.Sessions
{
    /// <summary>
    /// Сессия для запоминания ссылки на текущий список товара.
    /// Важный класс, так как на одной панели показываются товары и из директорий, и из заказов.
    /// </summary>
    class ProductSession
    {
        ListView.ListViewItemCollection ListViewProducts;
        public IProductStoring Store { get; set; }
        List<Product> Products
        {
            get
            {
                return Store.Products;
            }
        }

        public ProductSession(ListView.ListViewItemCollection listViewItemCollection)
        {
            ListViewProducts = listViewItemCollection;
        }

        /// <summary>
        /// Сессия "запоминает" ссылку на список продуктов.
        /// </summary>
        /// <param name="store"></param>
        public void SetProductsStore(IProductStoring store)
        {
            Store = store;
        }

        /// <summary>
        /// Записывает теущий список продуктов из ListView в список, запомненный из конструктора.
        /// </summary>
        public void WriteSessionProductsToOrigin()
        {
            var stringProducts = ListViewProducts.Cast<ListViewItem>()
                    .Select(item => item.SubItems.Cast<ListViewItem.ListViewSubItem>()
                    .Select(subitem => subitem.Text).ToList()).ToList();
            var products = stringProducts.Select(x => new Product(x)).ToList();

            Products.Clear();
            Products.AddRange(products);
        }
    }
}
