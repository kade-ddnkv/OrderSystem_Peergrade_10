using OrderSystem_Peergrade_10.OrderSystem__New_Peergrade_.Sessions;
using OrderSystem_Peergrade_10.OrderSystem__New_Peergrade_.UserControlSystems;
using OrderSystem_Peergrade_10.OrderSystem__New_Peergrade_.Users;
using OrderSystem_Peergrade_10.Storehouse__Old_Peergrade_;
using System;
using System.Linq;
using System.Windows.Forms;

// Этот файл отвечает за всю работу с директориями (работа с treeViewDir).

namespace OrderSystem_Peergrade_10
{
    public partial class MainForm
    {
        /// <summary>
        /// Добавляет директорию внутри выбранной директории (вызывает new CreateDirectoryForm).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddDirInsideToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Использую механику исключений, чтобы обрабатывать нажатие Cancel, отмены события.
            try
            {
                Directory newDirectory = new CreateDirectoryForm().GetDirectory();
                // Если уже существует каталог с таким именем, прекратить действие.
                if (CheckDirNameNonuniqueness(treeViewDir.SelectedNode.Nodes, newDirectory.Text))
                {
                    ShowDirNameNonuniqueness();
                    return;
                }
                treeViewDir.SelectedNode.Nodes.Add(newDirectory);
            }
            catch (CancelledActionException) { }
        }

        /// <summary>
        /// Добавляет директорию на одном уровне с выбранной директорией (либо в корневом каталоге).
        /// (вызывает new CreateDirectoryForm).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddDirOutsideToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Directory newDirectory = new CreateDirectoryForm().GetDirectory();
                TreeNodeCollection parentNodeCollection;
                parentNodeCollection = SelectParentNodeCollection(treeViewDir.SelectedNode);
                if (CheckDirNameNonuniqueness(parentNodeCollection, newDirectory.Text))
                {
                    ShowDirNameNonuniqueness();
                    return;
                }
                parentNodeCollection.Add(newDirectory);
            }
            catch (CancelledActionException) { }
        }

        /// <summary>
        /// Редактирует выюранную директорию. (вызывает new CreateDirectoryForm).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditDirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var selectedNode = treeViewDir.SelectedNode;
                string newName = new CreateDirectoryForm(selectedNode.Text).GetDirectoryName();
                if (CheckDirNameNonuniqueness(SelectParentNodeCollection(selectedNode), newName))
                {
                    ShowDirNameNonuniqueness();
                    return;
                }
                selectedNode.Text = newName;
            }
            catch (CancelledActionException) { }
        }


        /// <summary>
        /// Удаляет выбранную директорию, если в ней нет поддиректорий и продуктов.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteDirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var selectedNode = treeViewDir.SelectedNode;
            var parentNodeCollection = SelectParentNodeCollection(selectedNode);
            parentNodeCollection.Remove(selectedNode);
        }

        /// <summary>
        /// После выделения директории показывает товары из нее.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TreeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            ShowProductsOfStore(e.Node as Directory);
        }

        private void TreeView1_MouseUp(object sender, MouseEventArgs e)
        {
            // Редактирование директорий допускается только продавцу.
            if (UserControlSystem.CurrentUser is Seller)
            {
                CallContextMenuOfDirectoriesForSeller(e);
            }
            // Также метод обновляет панель с товарами, когда фокус возвращается к директориям.
            if (e.Button == MouseButtons.Left)
            {
                TreeNode selectedNode = treeViewDir.GetNodeAt(e.Location);
                if (selectedNode != null)
                {
                    treeViewDir.SelectedNode = null;
                    treeViewDir.SelectedNode = selectedNode;
                }
            }
        }

        /// <summary>
        /// Вызывает контекстное меню для работы с директориями.
        /// В зависимости от места нажатия кнопки могут блокироваться.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CallContextMenuOfDirectoriesForSeller(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                contextMenuStripDirectories.Show(Cursor.Position);
                TreeNode selectedNode = treeViewDir.GetNodeAt(e.Location);
                if (selectedNode != null)
                {
                    treeViewDir.SelectedNode = selectedNode;

                    if (selectedNode.Nodes.Count != 0 || (selectedNode as Directory).Products.Count != 0)
                    {
                        EnableButtonsOfDirContextMenu(true, true, true, false);
                    }
                    else
                    {
                        EnableButtonsOfDirContextMenu(true, true, true, true);
                    }
                }
                else
                {
                    EnableButtonsOfDirContextMenu(false, true, false, false);
                }
            }
        }

        /// <summary>
        /// Блокирует/разблокирует кнопки в контекстном меню для работы с директориями.
        /// </summary>
        /// <param name="addDirInside"></param>
        /// <param name="addDirOutside"></param>
        /// <param name="editDir"></param>
        /// <param name="deleteDir"></param>
        private void EnableButtonsOfDirContextMenu(bool addDirInside, bool addDirOutside, bool editDir, bool deleteDir)
        {
            addDirInsideToolStripMenuItem.Enabled = addDirInside;
            addDirOutsideToolStripMenuItem.Enabled = addDirOutside;
            editDirToolStripMenuItem.Enabled = editDir;
            deleteDirToolStripMenuItem.Enabled = deleteDir;
        }

        /// <summary>
        /// Проверяет, что в переданной директории есть директорий с таким названием.
        /// </summary>
        /// <param name="parentNodeCollection"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        private bool CheckDirNameNonuniqueness(TreeNodeCollection parentNodeCollection, string name)
        {
            return parentNodeCollection.Cast<TreeNode>().Any(node => node.Text == name);
        }

        /// <summary>
        /// Сообщает пользвателю, что в одной директории не может быть двух поддиректорий с одинаком названием.
        /// </summary>
        private void ShowDirNameNonuniqueness()
        {
            MessageBox.Show("In one directory can not be two subdirectories with one name.");
        }

        /// <summary>
        /// Выбирается родительская коллекция, в которой лежит переданный treeNode.
        /// </summary>
        /// <param name="selectedNode"></param>
        /// <returns></returns>
        private TreeNodeCollection SelectParentNodeCollection(TreeNode selectedNode)
        {
            if (treeViewDir.Nodes.Contains(selectedNode) || selectedNode == null)
            {
                return treeViewDir.Nodes;
            }
            else
            {
                return selectedNode.Parent.Nodes;
            }
        }
    }
}
