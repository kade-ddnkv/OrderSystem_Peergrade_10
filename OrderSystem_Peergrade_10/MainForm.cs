using OrderSystem_Peergrade_10.OrderSystem__New_Peergrade_.Users;
using OrderSystem_Peergrade_10.OrderSystem__New_Peergrade_.Orders;
using OrderSystem_Peergrade_10.OrderSystem__New_Peergrade_.Sessions;
using OrderSystem_Peergrade_10.Storehouse__Old_Peergrade_;
using OrderSystem_Peergrade_10.Storehouse__Old_Peergrade_.RandomizeForms;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Globalization;


// Этот файл отвечает за начальную инициализацию формы (за подключение кучи обработчиков событий).

namespace OrderSystem_Peergrade_10
{
    public partial class MainForm : Form
    {
        // userSaved нужно для вопроса о восстанавлении автоматически сохраненных файлов.
        bool userSaved;

        ProductSession currentProductSession;
        OrderSession currentOrderSession;

        public MainForm()
        {
            InitializeComponent();
            InitializeProductsPanel();
            InitializeOrdersPanel();
            InitializeUsersPanel();
            InitializeCurrentSession();

            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { openToolStripMenuItem, saveToolStripMenuItem });

            //Подключение методов к ивентам.
            ConnectDirectoriesEventHandlers();
            ConnectProductsEventHandlers();
            ConnectOrdersEventHandlers();
            ConnectStripMenuEventHandlers();
            ConnectUsersEventHandlers();
            ConnectUpdatingWhileChangingUser();

            FormClosing += MainForm_FormClosing;

            ConnectAutoSaver();
            ConnectAutoWidth();
            ConnectProductsSaver();
            ConnectRefreshingProductPanel();
        }

        private void ConnectUsersEventHandlers()
        {
            listViewUsers.MouseUp += ListViewUsers_MouseUp;
        }

        private void ConnectStripMenuEventHandlers()
        {
            saveToolStripMenuItem.Click += SaveToolStripMenuItem_Click;
            openToolStripMenuItem.Click += OpenToolStripMenuItem_Click;
            csvAnalysisToolStripMenuItem.Click += CsvAnalysisToolStripMenuItem_Click;
            randomizeToolStripMenuItem.Click += RandomizeToolStripMenuItem_Click;
            changeUserToolStripMenuItem.Click += ChangeUserToolStripMenuItem_Click;
            helpToolStripMenuItem.Click += HelpToolStripMenuItem_Click;
        }

        private void InitializeCurrentSession()
        {
            currentProductSession = new ProductSession(listViewProducts.Items);
            currentOrderSession = new OrderSession(listViewOrders.Items);
        }

        /// <summary>
        /// Нажатие на кнопку Randomize - предлагает пользователю создать рандомные директории с товарами.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RandomizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var randomizingSettings = new RandomizeForm().GetRandomizationSettings();
                treeViewDir.Nodes.Clear();
                treeViewDir.Nodes.AddRange(Randomizing.CreateRandomTreeNodeArray(randomizingSettings.dirCount, randomizingSettings.productCount));
            }
            catch (CancelledActionException) { }
        }

        /// <summary>
        /// Показывает Message исключения. Использовать, если исключение не должно произойти.
        /// </summary>
        /// <param name="ex"></param>
        public static void ShowExceptionMessage(Exception ex)
        {
            MessageBox.Show($"An unexpected error occured: {Environment.NewLine}{ex.Message}");
        }

        /// <summary>
        /// Нажатие на кнопку CsvAnalysis - предлагает пользователю сохранить список недостающих товаров.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CsvAnalysisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "csv files (*.csv)|*.csv";
            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }

            Product.CsvAnalyseSave(GetAllProducts().ToList(), saveFileDialog1.FileName);
        }

        /// <summary>
        /// Подключает к ивентам работы с директориями соответувующие методы.
        /// </summary>
        private void ConnectDirectoriesEventHandlers()
        {
            treeViewDir.MouseUp += TreeView1_MouseUp;
            treeViewDir.AfterSelect += TreeView1_AfterSelect;

            addDirOutsideToolStripMenuItem.Click += AddDirOutsideToolStripMenuItem_Click;
            addDirInsideToolStripMenuItem.Click += AddDirInsideToolStripMenuItem_Click;
            editDirToolStripMenuItem.Click += EditDirToolStripMenuItem_Click;
            deleteDirToolStripMenuItem.Click += DeleteDirToolStripMenuItem_Click;
        }

        /// <summary>
        /// Подключает к ивентам работы с товарами соответувующие методы.
        /// </summary>
        private void ConnectProductsEventHandlers()
        {
            listViewProducts.MouseUp += ListView1_MouseUp;

            addProdToolStripMenuItem.Click += AddProdToolStripMenuItem_Click;
            editProdToolStripMenuItem.Click += EditProdToolStripMenuItem_Click;
            deleteProdToolStripMenuItem.Click += DeleteProdToolStripMenuItem_Click;
            addToCartToolStripMenuItem.Click += AddToCartToolStripMenuItem_Click;
        }

        private void ConnectOrdersEventHandlers()
        {
            listViewOrders.MouseUp += ListViewOrders_MouseUp;

            placeOrderToolStripMenuItem.Click += PlaceOrderToolStripMenuItem_Click;
            payForOrderToolStripMenuItem.Click += PayForOrderToolStripMenuItem_Click;
            processToolStripMenuItem.Click += ProcessToolStripMenuItem_Click;
            shipToolStripMenuItem.Click += ShipToolStripMenuItem_Click;
            executeToolStripMenuItem.Click += ExecuteToolStripMenuItem_Click;
            UpdateOrdersStatusChanged += new StatusChangedHandler(UpdateOrdersPanel);
        }

        /// <summary>
        /// Подключает метод сохранения к ивентам всех действий.
        /// </summary>
        private void ConnectAutoSaver()
        {
            // Программа будет автоматически сохранять файл после любого действия (создание, удаление).
            addDirOutsideToolStripMenuItem.Click += EventHandlerOfAutoSave;
            addDirInsideToolStripMenuItem.Click += EventHandlerOfAutoSave;
            editDirToolStripMenuItem.Click += EventHandlerOfAutoSave;
            deleteDirToolStripMenuItem.Click += EventHandlerOfAutoSave;

            addProdToolStripMenuItem.Click += EventHandlerOfAutoSave;
            editProdToolStripMenuItem.Click += EventHandlerOfAutoSave;
            deleteProdToolStripMenuItem.Click += EventHandlerOfAutoSave;
            randomizeToolStripMenuItem.Click += EventHandlerOfAutoSave;

            addToCartToolStripMenuItem.Click += EventHandlerOfAutoSave;
            placeOrderToolStripMenuItem.Click += EventHandlerOfAutoSave;
            payForOrderToolStripMenuItem.Click += EventHandlerOfAutoSave;

            addStatusToolStripMenuItem.Click += EventHandlerOfAutoSave;
            processToolStripMenuItem.Click += EventHandlerOfAutoSave;
            shipToolStripMenuItem.Click += EventHandlerOfAutoSave;
            executeToolStripMenuItem.Click += EventHandlerOfAutoSave;

            changeUserToolStripMenuItem.Click += EventHandlerOfAutoSave;
        }

        /// <summary>
        /// Подключает метод авто-ширины столбцов к ивентам действий с товарами. 
        /// </summary>
        private void ConnectAutoWidth()
        {
            treeViewDir.AfterSelect += RefreshWidthOfProductsEventHandler;
            addProdToolStripMenuItem.Click += RefreshWidthOfProductsEventHandler;
            editProdToolStripMenuItem.Click += RefreshWidthOfProductsEventHandler;
            deleteProdToolStripMenuItem.Click += RefreshWidthOfProductsEventHandler;

            addToCartToolStripMenuItem.Click += RefreshWidthOfProductsEventHandler;
            placeOrderToolStripMenuItem.Click += RefreshWidthOfProductsEventHandler;
            payForOrderToolStripMenuItem.Click += RefreshWidthOfProductsEventHandler;
            //listViewOrders.SelectedIndexChanged += RefreshWidthOfProductsEventHandler;

            addStatusToolStripMenuItem.Click += RefreshWidthOfProductsEventHandler;
            processToolStripMenuItem.Click += RefreshWidthOfProductsEventHandler;
            shipToolStripMenuItem.Click += RefreshWidthOfProductsEventHandler;
            executeToolStripMenuItem.Click += RefreshWidthOfProductsEventHandler;

            changeUserToolStripMenuItem.Click += RefreshWidthOfProductsEventHandler;
        }

        private void ProductSaverEventHandler(object sender, EventArgs eventArgs)
        {
            currentProductSession.WriteSessionProductsToOrigin();
        }

        private void ConnectProductsSaver()
        {
            addProdToolStripMenuItem.Click += ProductSaverEventHandler;
            editProdToolStripMenuItem.Click += ProductSaverEventHandler;
            deleteProdToolStripMenuItem.Click += ProductSaverEventHandler;
            addToCartToolStripMenuItem.Click += ProductSaverEventHandler;
        }

        private void ConnectRefreshingProductPanel()
        {
            openToolStripMenuItem.Click += ResetProductsIfNoSelectEventHandler;
            randomizeToolStripMenuItem.Click += ResetProductsIfNoSelectEventHandler;
            deleteDirToolStripMenuItem.Click += ResetProductsIfNoSelectEventHandler;
            placeOrderToolStripMenuItem.Click += ResetProductsIfNoSelectEventHandler;
        }

        /// <summary>
        /// Получение справки о работе с программой.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HelpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string message = $"Some useful info:" +
                $"{Environment.NewLine}" +
                $"{Environment.NewLine}- You can edit directories/products/orders with a right-click menus." +
                $"{Environment.NewLine}- Opening file or creating random will erase all current data so save it previously." +
                $"{Environment.NewLine}- Use randomize if it fasten you." +
                $"{Environment.NewLine}- Products can not be removed from Orders after Processing by Seller, logically right." +
                $"{Environment.NewLine}" +
                $"{Environment.NewLine}Mmm, I dunno, that's all.";
            MessageBox.Show(message);
        }
    }
}
