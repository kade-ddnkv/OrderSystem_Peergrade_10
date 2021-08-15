using OrderSystem_Peergrade_10.OrderSystem__New_Peergrade_.UserControlSystems;
using OrderSystem_Peergrade_10.OrderSystem__New_Peergrade_.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

// Этот файл отвечает за обработку событий, связанных с пользователями.

namespace OrderSystem_Peergrade_10
{
    public partial class MainForm
    {
        public delegate void UsersChangedHandler();
        static public event UsersChangedHandler UsersChangedUpdate;

        /// <summary>
        /// Инициализирует панель для клиентов.
        /// </summary>
        private void InitializeUsersPanel()
        {
            foreach (string name in User.GetAllPropertiesNames())
            {
                listViewUsers.Columns.Add(name, -2, HorizontalAlignment.Left);
            }
        }

        /// <summary>
        /// Event хэндлеры для обновления информации после смены пользователя.
        /// </summary>
        private void ConnectUpdatingWhileChangingUser()
        {
            UsersChangedUpdate += new UsersChangedHandler(UpdateUsersPanel);
            UsersChangedUpdate += new UsersChangedHandler(UpdateOrdersPanel);
            UsersChangedUpdate += new UsersChangedHandler(ResetProductsIfNoSelect);
            UsersChangedUpdate += new UsersChangedHandler(() => comboBoxOrders.SelectedIndex = 0);
        }

        /// <summary>
        /// Обработка тыка по панели с клиентами.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListViewUsers_MouseUp(object sender, MouseEventArgs e)
        {
            var selectedItem = listViewUsers.GetItemAt(e.X, e.Y);
            if (selectedItem != null)
            {
                Client currentClient = (selectedItem as ListViewItemUser).User as Client;
                currentOrderSession.SetOrdersStore(currentClient);
                // После выбора клиента автоматически выбираются его заказы.
                comboBoxOrders.SelectedIndex = -1;
                comboBoxOrders.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// Запускает форму выбора пользователя.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChangeUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new UserControlSystemForm().ShowDialog();
            UsersChangedUpdate?.Invoke();
        }

        /// <summary>
        /// Обновляет панель с польователями, нужно для быстрого просмотра изменений.
        /// </summary>
        private void UpdateUsersPanel()
        {
            listViewUsers.Items.Clear();
            listViewUsers.Items.AddRange(UserControlSystem.Users
                .Where(user => user is Client).Select(user => new ListViewItemUser(user)).ToArray());

            if (UserControlSystem.CurrentUser is Seller)
            {
                listViewUsers.Enabled = true;
            }
            else
            {
                listViewUsers.Enabled = false;
            }
        }

        /// <summary>
        ///  Вызывает событие изменения смены пользователя.
        /// </summary>
        static public void RaiseUsersChangedUpdate()
        {
            UsersChangedUpdate?.Invoke();
        }
    }
}
