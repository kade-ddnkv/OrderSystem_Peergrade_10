using OrderSystem_Peergrade_10.OrderSystem__New_Peergrade_.Users;
using OrderSystem_Peergrade_10.OrderSystem__New_Peergrade_.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OrderSystem_Peergrade_10.OrderSystem__New_Peergrade_.UserControlSystems;
using System.Drawing;
using OrderSystem_Peergrade_10.OrderSystem__New_Peergrade_.Sessions;
using System.Globalization;

// Этот файл отвечает за заказы, панель с заказами, и все связанное.

namespace OrderSystem_Peergrade_10
{
    public partial class MainForm
    {
        public delegate void StatusChangedHandler();
        static public event StatusChangedHandler UpdateOrdersStatusChanged;

        /// <summary>
        /// Инициализация панели с заказами.
        /// </summary>
        public void InitializeOrdersPanel()
        {
            Client.OrdersChanged += UpdateOrdersPanel;
            comboBoxOrders.DrawItem += ComboBoxOrders_DrawItem;

            foreach (string name in Order.GetAllPropertiesNames())
            {
                listViewOrders.Columns.Add(name, -2, HorizontalAlignment.Left);
            }
        }

        /// <summary>
        /// Возвращает выделенный в данное время заказ.
        /// </summary>
        /// <returns></returns>
        private Order GetSelectedOrder()
        {
            return listViewOrders.SelectedItems.Cast<ListViewItemOrder>().FirstOrDefault()?.Order;
        }

        /// <summary>
        /// Обновляет панель с заказами, например, при изменении заказа.
        /// </summary>
        public void UpdateOrdersPanel()
        {
            if (UserControlSystem.CurrentUser is Client)
            {
                ShowOrdersOfClient(UserControlSystem.CurrentUser as Client);
                comboBoxOrders_SelectedIndexChanged(new object(), new EventArgs());
            }
            else if (UserControlSystem.CurrentUser is Seller)
            {
                comboBoxOrders_SelectedIndexChanged(new object(), new EventArgs());
            }
        }

        /// <summary>
        /// Возвращает сейчас выделенный ListViewItem во вкладке с продуктами.
        /// </summary>
        /// <returns></returns>
        private ListViewItem GetSelectedListViewProductsItem()
        {
            return listViewProducts.SelectedItems.Cast<ListViewItem>().FirstOrDefault();
        }

        /// <summary>
        /// Добавляет по клику товар в корзину.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddToCartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Client currentClient = UserControlSystem.CurrentUser as Client;
            var selectedListViewItem = GetSelectedListViewProductsItem();
            var selectedProduct = ListViewItemToProduct(selectedListViewItem);
            try
            {
                // Может выбросить исключение InvalidOnprationException. Но не должно.
                selectedProduct.ReduceCount();
                // Заменяю продукт на тот же с уменьшенным кол-вом.
                int indexOfSelected = listViewProducts.Items.IndexOf(selectedListViewItem);
                listViewProducts.Items[indexOfSelected] = ProductToListViewItem(selectedProduct);

                var foundProduct = currentClient.Cart.Products.Find(selectedProduct.IsEqualByNameArticlePrice);
                if (foundProduct != null)
                {
                    // Если товар уже есть в корзине, прибавляю 1 к его кол-ву.
                    foundProduct.Count += 1;
                }
                else
                {
                    // Иначе добавляю его в корзину.
                    selectedProduct.Count = 1;
                    selectedProduct.MinCount = 0;
                    currentClient.Cart.Products.Add(selectedProduct);
                }
            }
            catch (InvalidOperationException ex)
            {
                ShowExceptionMessage(ex);
            }
        }

        /// <summary>
        /// Большой метод. Возникает при нажатии мышки. Включает, отключает другие методы для работы с заказами.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListViewOrders_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                var selectedItem = listViewOrders.GetItemAt(e.X, e.Y);
                if (selectedItem != null)
                {
                    ShowProductsOfStore((selectedItem as ListViewItemOrder).Order);
                }
            }
            if (e.Button == MouseButtons.Right)
            {
                // Только клиент может оформлять и оплачивать заказы.
                contextMenuStripOrders.Show(Cursor.Position);

                var selectedOrder = GetSelectedOrder();
                if (selectedOrder != null)
                {
                    if (UserControlSystem.CurrentUser is Client)
                    {
                        if (selectedOrder is Cart)
                        {
                            if ((selectedOrder as Cart).Products.Count == 0)
                            {
                                EnableButtonsOfOrderContextMenu(false, false, false);
                            }
                            else
                            {
                                EnableButtonsOfOrderContextMenu(true, false, false);
                            }
                        }
                        else
                        // Оплачивать можно только заказы.
                        {
                            // Только "обработанные" заказы.
                            if (selectedOrder.Status == OrderStatus.Processed)
                            {
                                EnableButtonsOfOrderContextMenu(false, true, false);
                            }
                            else
                            {
                                EnableButtonsOfOrderContextMenu(false, false, false);
                            }
                        }
                    }
                    else
                    // Продавец может менять статус заказа.
                    {
                        EnableButtonsOfOrderContextMenu(false, false, true);
                        EnableButtonsOfOrderSubContextMenu(selectedOrder.ReadyForStatusFlag(OrderStatus.Processed)
                            , selectedOrder.ReadyForStatusFlag(OrderStatus.Shipped)
                            , selectedOrder.ReadyForStatusFlag(OrderStatus.Executed));
                    }
                }
                else
                {
                    EnableButtonsOfOrderContextMenu(false, false, false);
                }
            }
        }

        /// <summary>
        /// Блокирует/разблокирует кнопки на контекстном меню для работы с заказами.
        /// </summary>
        /// <param name="addProd"></param>
        /// <param name="editProd"></param>
        /// <param name="deleteProd"></param>
        private void EnableButtonsOfOrderContextMenu(bool placeOrder, bool payForOrder, bool addStatus)
        {
            placeOrderToolStripMenuItem.Enabled = placeOrder;
            payForOrderToolStripMenuItem.Enabled = payForOrder;
            addStatusToolStripMenuItem.Enabled = addStatus;
        }

        /// <summary>
        /// Блокирует/разблокирует кнопки на втором подконтекстном меню для статуса заказов.
        /// </summary>
        /// <param name="processStatus"></param>
        /// <param name="shipStatus"></param>
        /// <param name="executeStatus"></param>
        private void EnableButtonsOfOrderSubContextMenu(bool processStatus, bool shipStatus, bool executeStatus)
        {
            processToolStripMenuItem.Enabled = processStatus;
            shipToolStripMenuItem.Enabled = shipStatus;
            executeToolStripMenuItem.Enabled = executeStatus;
        }

        /// <summary>
        /// Формирует заказ из корзины.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PlaceOrderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Client currentClient = UserControlSystem.CurrentUser as Client;
            currentClient.PlaceOrderFromCart();
        }

        /// <summary>
        /// Пользователь оплачивает выбранный заказ.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PayForOrderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Client currentClient = UserControlSystem.CurrentUser as Client;
            currentClient.PayForOrder(GetSelectedOrder());
            UpdateOrdersStatusChanged?.Invoke();
        }

        /// <summary>
        /// Показывает на панели заказы переданного клиента.
        /// </summary>
        /// <param name="client"></param>
        private void ShowOrdersOfClient(Client client)
        {
            // Прикрепляю к текущей сессии заказы этого пользователя.
            currentOrderSession.SetOrdersStore(client);

            listViewOrders.Items.Clear();
            listViewOrders.Items.Add(new ListViewItemOrder(client.Cart));
            listViewOrders.Items.AddRange(client.Orders.Select(order => new ListViewItemOrder(order)).ToArray());
        }

        /// <summary>
        /// Показывает на панели список переданных заказов.
        /// </summary>
        /// <param name="orders"></param>
        private void ShowListOfOrders(List<Order> orders)
        {
            listViewOrders.Items.Clear();
            listViewOrders.Items.AddRange(orders.Select(order => new ListViewItemOrder(order)).ToArray());
        }

        /// <summary>
        /// Изменяет выорку заказов (все/ активные/ только одного клиента)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxOrders_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (UserControlSystem.CurrentUser is Client)
            {
                if (comboBoxOrders.SelectedIndex == -1 || comboBoxOrders.SelectedIndex == 1 || comboBoxOrders.SelectedIndex == 2)
                {
                    comboBoxOrders.SelectedIndex = 0;
                }
                SetSumPaidOfListOrders((UserControlSystem.CurrentUser as Client).Orders);
            }
            else
            // Если смотрит продавец.
            {
                if (currentOrderSession.Store == null)
                {
                    return;
                }
                List<Order> currentOrders;
                if (comboBoxOrders.SelectedIndex == 0)
                {
                    currentOrders = currentOrderSession.Store.Orders;
                    ShowListOfOrders(currentOrders);
                }
                else if (comboBoxOrders.SelectedIndex == 1)
                {
                    currentOrders = Order.AllOrders;
                    ShowListOfOrders(currentOrders);
                }
                else
                {
                    currentOrders = Order.AllOrders.Where(order => !order.Status.HasFlag(OrderStatus.Executed)).ToList();
                    ShowListOfOrders(currentOrders);
                }
                SetSumPaidOfListOrders(currentOrders);
            }
        }

        /// <summary>
        /// Выставляет суммарную стоимость заказов клиента.
        /// </summary>
        /// <param name="orders"></param>
        private void SetSumPaidOfListOrders(List<Order> orders)
        {
            double resultSum = orders.Where(order => order.Status.HasFlag(OrderStatus.Paid))
                .Select(order => order.Products.Select(p => p.Count * p.Price).Sum()).Sum();
            textBoxSumPaid.Text = resultSum.ToString(CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Нужно для рисования комбобокса, делает второй и третий пункт серыми для клиента.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboBoxOrders_DrawItem(object sender, DrawItemEventArgs e)
        {
            if ((UserControlSystem.CurrentUser is Client) && ((e.Index == 1) || (e.Index == 2)))
            {
                e.Graphics.DrawString(comboBoxOrders.Items[e.Index].ToString(), DefaultFont, Brushes.LightGray, e.Bounds);
            }
            else
            {
                e.DrawBackground();
                e.Graphics.DrawString(comboBoxOrders.Items[e.Index].ToString(), DefaultFont, Brushes.Black, e.Bounds);
                e.DrawFocusRectangle();
            }
        }

        /// <summary>
        /// Изменяет статус заказа на "Processed".
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProcessToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetSelectedOrder().AddStatusFlag(OrderStatus.Processed);
            UpdateOrdersStatusChanged?.Invoke();
        }

        /// <summary>
        /// Изменяет статус заказа на "Shipped".
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShipToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetSelectedOrder().AddStatusFlag(OrderStatus.Shipped);
            UpdateOrdersStatusChanged?.Invoke();
        }

        /// <summary>
        /// Изменяет статус заказа на "Executed".
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExecuteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetSelectedOrder().AddStatusFlag(OrderStatus.Executed);
            UpdateOrdersStatusChanged?.Invoke();
        }
    }
}
