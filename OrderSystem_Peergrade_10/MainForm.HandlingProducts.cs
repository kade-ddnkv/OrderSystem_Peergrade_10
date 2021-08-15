using OrderSystem_Peergrade_10.OrderSystem__New_Peergrade_.Orders;
using OrderSystem_Peergrade_10.OrderSystem__New_Peergrade_.Sessions;
using OrderSystem_Peergrade_10.OrderSystem__New_Peergrade_.UserControlSystems;
using OrderSystem_Peergrade_10.OrderSystem__New_Peergrade_.Users;
using OrderSystem_Peergrade_10.Storehouse__Old_Peergrade_;
using OrderSystem_Peergrade_10.Storehouse__Old_Peergrade_.ProductForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

// Этот файл отвечает за работу с товарами (с listViewProducts).

namespace OrderSystem_Peergrade_10
{
    public partial class MainForm
    {
        /// <summary>
        /// Настраивает listView1.
        /// </summary>
        public void InitializeProductsPanel()
        {
            foreach (string name in Product.GetAllPropertiesNames())
            {
                listViewProducts.Columns.Add(name, -2, HorizontalAlignment.Left);
            }
        }

        // В конце каждого метода по типу add, edit, delete вызывается WriteProductsToDir(),...
        // ...который записывает текущие товары в выделенную директорию.
        // Я не подключил WritesProductsToDir к ивентам потому, что в add, edit и delete может выскочить ошибка.

        /// <summary>
        /// Добавляет товар к тем, что в выделенной директории и к listView1. (вызывает new CreateProductForm).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddProdToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Product newProduct = new CreateProductForm().GetProduct();
                if (CheckProductArticleNonuniqueness(newProduct.Article))
                {
                    ShowProductArticleNonuniqueness();
                    return;
                }
                listViewProducts.Items.Add(ProductToListViewItem(newProduct));
            }
            // NullRefException на всякий случай обрабатываю, вылетать не должно.
            catch (NullReferenceException) { }
            catch (CancelledActionException) { }
        }

        /// <summary>
        /// Редактирует товар в текущей директории и в listView1. (вызывает new CreateProductForm).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditProdToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                // Получаю текущие значения товара из таблицы и передаю их в форму для создания товара.
                var selectedItem = listViewProducts.SelectedItems[0];
                List<string> subItems = selectedItem.SubItems.Cast<ListViewItem.ListViewSubItem>().Select(subitem => subitem.Text).ToList();
                Product newProduct = new CreateProductForm(subItems[0], subItems[1], subItems[2], subItems[3], subItems[4]).GetProduct();
                // Проверяю уникальность артикула.
                if (CheckProductArticleNonuniqueness(newProduct.Article, ListViewItemToProduct(selectedItem)))
                {
                    ShowProductArticleNonuniqueness();
                    return;
                }
                // Заменяю прошлый товар измененным.
                listViewProducts.Items[listViewProducts.Items.IndexOf(selectedItem)] = ProductToListViewItem(newProduct);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                ShowExceptionMessage(ex);
            }
            catch (CancelledActionException) { }
        }

        /// <summary>
        /// Удаляет товар из текущей директории и из listView1.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteProdToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Если нажат Delete, значит, точно выделен один объект.
            try
            {
                listViewProducts.Items.Remove(listViewProducts.SelectedItems[0]);
            }
            // Но try-catch не помешает.
            catch (ArgumentOutOfRangeException ex)
            {
                ShowExceptionMessage(ex);
            }
        }

        /// <summary>
        /// Вызывает контекстное меню для работы с товарами.
        /// Включенные кнопки зависят от места нажатия.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListView1_MouseUp(object sender, MouseEventArgs e)
        {
            if (UserControlSystem.CurrentUser is Client)
            {
                CallContextMenuOfProductsForClient(e);
            }
            else if (UserControlSystem.CurrentUser is Seller)
            {
                CallContextMenuOfProductsForSeller(e);
            }
        }

        public void CallContextMenuOfProductsForClient(MouseEventArgs e)
        {
            // Чтобы как-то менять товары, обязательно должна быть выделена директория или заказ.
            if (e.Button == MouseButtons.Right && (treeViewDir.SelectedNode != null || GetSelectedOrder() != null))
            {
                contextMenuStripProducts.Show(Cursor.Position);
                // Беру товар на месте курсора.
                var selectedItem = listViewProducts.GetItemAt(e.X, e.Y);
                if (selectedItem != null)
                {
                    listViewProducts.SelectedItems.Clear();
                    selectedItem.Selected = true;

                    // Клиент не может менять товары в директории.
                    if (currentProductSession.Store is Directory)
                    {
                        // Нельзя добавлять товар с нулевым количеством.
                        EnableButtonsOfProdContextMenu(false, false, false, ListViewItemToProduct(selectedItem).Count > 0);
                    }
                    // Иначе он редактирует свои! заказы.
                    else
                    {
                        // Можно удалять товары только до обработки заказа, что логично.
                        EnableButtonsOfProdContextMenu(false, false, (int)GetSelectedOrder().Status <= 1, false);
                    }
                }
                else
                {
                    EnableButtonsOfProdContextMenu(false, false, false, false);
                }
            }
        }

        public void CallContextMenuOfProductsForSeller(MouseEventArgs e)
        {
            // Чтобы как-то менять товары продавцу, обязательно должна быть выделена директория.
            if (e.Button == MouseButtons.Right && treeViewDir.SelectedNode != null)
            {
                contextMenuStripProducts.Show(Cursor.Position);
                // Беру товар на месте курсора.
                var selectedItem = listViewProducts.GetItemAt(e.X, e.Y);
                if (selectedItem != null)
                {
                    listViewProducts.SelectedItems.Clear();
                    selectedItem.Selected = true;
                    EnableButtonsOfProdContextMenu(true, true, true, false);
                }
                else
                {
                    EnableButtonsOfProdContextMenu(true, false, false, false);
                }
            }
        }

        /// <summary>
        /// Блокирует/разблокирует кнопки на контекстном меню для работы с товарами.
        /// </summary>
        /// <param name="addProd"></param>
        /// <param name="editProd"></param>
        /// <param name="deleteProd"></param>
        private void EnableButtonsOfProdContextMenu(bool addProd, bool editProd, bool deleteProd, bool addToCart)
        {
            addProdToolStripMenuItem.Enabled = addProd;
            editProdToolStripMenuItem.Enabled = editProd;
            deleteProdToolStripMenuItem.Enabled = deleteProd;
            addToCartToolStripMenuItem.Enabled = addToCart;
        }

        private void ResetProductsIfNoSelectEventHandler(object sender, EventArgs e)
        {
            ResetProductsIfNoSelect();
        }

        /// <summary>
        /// Очищает окно продуктов, если нет выделенного подключенного хранилища.
        /// </summary>
        public void ResetProductsIfNoSelect()
        {
            // Если ранее выделенное хранилище продуктов теперь не selected.
            if ((currentProductSession.Store is Directory && treeViewDir.SelectedNode == null)
                || (currentProductSession.Store is Order && listViewOrders.SelectedItems.Count == 0))
            {
                listViewProducts.Items.Clear();
            }
        }

        /// <summary>
        /// Преобразует Product в ListViewItem.
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public ListViewItem ProductToListViewItem(Product product)
        {
            return new ListViewItem(product.GetAllProperties().ToArray());
        }

        /// <summary>
        /// Преобразует ListViewItem в Product.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public Product ListViewItemToProduct(ListViewItem item)
        {
            return new Product(item.SubItems.Cast<ListViewItem.ListViewSubItem>().Select(subitem => subitem.Text).ToList());
        }

        /// <summary>
        /// Показывает в окне товары из переданного хранилища товаров.
        /// </summary>
        /// <param name="store"></param>
        private void ShowProductsOfStore(IProductStoring store)
        {
            ShowListOfProducts(store.Products);
            // Если показываются товары, значит, сессия связывается с переданным списком товаров.
            currentProductSession.SetProductsStore(store);
        }

        public void ShowListOfProducts(List<Product> products)
        {
            listViewProducts.Items.Clear();
            var items = products.Select(x => ProductToListViewItem(x));
            listViewProducts.Items.AddRange(items.ToArray());
        }

        
        /// <summary>
        /// Обновляет ширину столбцов товаров. (подключается к ивентам любых совершаемых с товарами/заказами/клиентами действий).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RefreshWidthOfProductsEventHandler(object sender, EventArgs e)
        {
            RefreshWidthOfProducts(listViewProducts);
            RefreshWidthOfProducts(listViewOrders);
            RefreshWidthOfProducts(listViewUsers);
            // На время отключу, пока не разберусь, почему такая задержка при прорисовке.
            //RefreshWidthOfProducts();
        }

        private void RefreshWidthOfProducts(ListView listView)
        {
            if (listView.Items.Count == 0)
            {
                listView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            }
            else
            {
                listView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            }
        }

        // На время отключу, пока не разберусь, почему такая задержка при прорисовке.
        /*
        /// <summary>
        /// Обновляет ширину столбцов в таблице товаров.
        /// </summary>
        private void RefreshWidthOfProducts()
        {
            foreach (var column in listViewProducts.Columns.Cast<ColumnHeader>())
            {
                // Выбирается нибольшее из длины элементов и заголовков.
                column.Width = -2;
                int widthByHeader = column.Width;
                column.Width = -1;
                int widthByItems = column.Width;
                column.Width = widthByHeader > widthByItems ? -2 : -1;
            }
        }
        */
        
        // Временный список товаров (для рекурсивной функции сбора всех товаров).
        private HashSet<Product> allProductsTmp = new HashSet<Product>();

        /// <summary>
        /// Рекурсивная функция получения всех товаров из коллекции нодов.
        /// </summary>
        /// <param name="nodeCollection"></param>
        /// <param name="exeptProduct"></param>
        private void GetAllProductsByRecursion(TreeNodeCollection nodeCollection, Product exeptProduct)
        {
            foreach (Directory dir in nodeCollection.Cast<Directory>())
            {
                foreach (Product product in dir.Products.Where(prod => prod != exeptProduct))
                {
                    product.SetDirectoryPath(dir.FullPath);
                    allProductsTmp.Add(product);
                }
                GetAllProductsByRecursion(dir.Nodes, exeptProduct);
            }
        }

        /// <summary>
        /// Получает все продукты. (Также обновляет во всех продуктах свойство DirectoryPath).
        /// </summary>
        /// <param name="exeptProduct"></param>
        /// <returns></returns>
        public HashSet<Product> GetAllProducts(Product exeptProduct = null)
        {
            allProductsTmp.Clear();
            GetAllProductsByRecursion(treeViewDir.Nodes, exeptProduct);
            return allProductsTmp;
        }

        /// <summary>
        /// Проверяет все продукты на наличие переданного артикула. true - если такой артикул уже есть.
        /// </summary>
        /// <param name="article"></param>
        /// <param name="exeptProduct"></param>
        /// <returns></returns>
        private bool CheckProductArticleNonuniqueness(uint article, Product exeptProduct = null)
        {
            var allProducts = GetAllProducts(exeptProduct);
            return allProducts.Any(product => product.Article == article);
        }

        /// <summary>
        /// Сообщает пользователю, что не может быть двух товаров с одним артикулом.
        /// </summary>
        private void ShowProductArticleNonuniqueness()
        {
            MessageBox.Show("There can not be two products with one article.");
        }
    }
}
