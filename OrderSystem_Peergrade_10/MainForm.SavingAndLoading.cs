using OrderSystem_Peergrade_10.OrderSystem__New_Peergrade_.UserControlSystems;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Windows.Forms;
using Directory = OrderSystem_Peergrade_10.Storehouse__Old_Peergrade_.Directory;

// Этот файл содержит методы для загрузки и сохранения информации.

namespace OrderSystem_Peergrade_10
{
    public partial class MainForm
    {
        /// <summary>
        /// Нажатие на кнопку Save - предлагает пользователю сохранить файл.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Сначала вызывается окно сохранения файла.
            saveFileDialog1.Filter = "bin files (*.bin)|*.bin";
            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }

            SaveData(saveFileDialog1.FileName);
            userSaved = true;
        }

        /// <summary>
        /// Сохраняет данные в выбранный файл. Данные загружает из treeView1.
        /// </summary>
        /// <param name="fileName"></param>
        private void SaveData(string fileName)
        {
            try
            {
                // Создаю нод, в Nodes которого кладу все начальные каталоги treeView.
                var wholeTreeDirChildren = treeViewDir.Nodes.Cast<Directory>().Select(dir => dir.Clone() as Directory).ToArray();
                Directory wholeTreeDir = new Directory("wholeTreeDir", wholeTreeDirChildren);
                BinarySerializingClass binarySave = new BinarySerializingClass(wholeTreeDir, UserControlSystem.Users, UserControlSystem.CurrentUser);

                IFormatter formatter = new BinaryFormatter();
                using (Stream stream = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    formatter.Serialize(stream, binarySave);
                }
            }
            catch (SerializationException ex)
            {
                ShowExceptionMessage(ex);
            }
            catch (IOException ex)
            {
                ShowExceptionMessage(ex);
            }
        }

        /// <summary>
        /// Метод AutoSave для подключения как обработчик события.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EventHandlerOfAutoSave(object sender, EventArgs e)
        {
            SaveData("backup.bin");
            userSaved = false;
        }

        /// <summary>
        /// Нажатие на кнопку Open - предлагает пользователю открыть файл.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Сначала вызывается окно открытия файла.
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }

            OpenData(openFileDialog1.FileName);
            userSaved = true;
        }

        /// <summary>
        /// Считывает данные из файла по переданному пути. Сразу загружает их в treeView1 и UserControlSystem.
        /// </summary>
        /// <param name="fileName"></param>
        private void OpenData(string fileName)
        {
            try
            {
                IFormatter formatter = new BinaryFormatter();
                using (Stream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    BinarySerializingClass binaryOpen = formatter.Deserialize(stream) as BinarySerializingClass;
                    UserControlSystem.Users = binaryOpen.Users;
                    UserControlSystem.CurrentUser = binaryOpen.CurrentUser;

                    treeViewDir.Nodes.Clear();
                    // В корневой каталог добавляю все ноды из сохраненной директории (из wholeTreeDir).
                    treeViewDir.Nodes.AddRange(binaryOpen.WholeTreeDir.Nodes.Cast<Directory>().ToArray());

                    // После загрузки нужно обновить таблицы.
                    UpdateUsersPanel();
                    UpdateOrdersPanel();
                }
            }
            catch (SerializationException)
            {
                MessageBox.Show("A loaded data was corrupted.");
                treeViewDir.Nodes.Clear();
                UserControlSystem.Users.Clear();
                UserControlSystem.CurrentUser = null;
            }
            catch (OutOfMemoryException ex)
            {
                MessageBox.Show("Sorry. Out of memory exception. App will use Environment.FailFast.");
                Environment.FailFast("out of memory");
            }
            catch (IOException ex)
            {
                ShowExceptionMessage(ex);
                treeViewDir.Nodes.Clear();
                UserControlSystem.Users.Clear();
                UserControlSystem.CurrentUser = null;
            }
        }

        /// <summary>
        /// Сохраняет настройку - то, что последний открытый файл был сохранен самим пользователем.
        /// </summary>
        public void SaveSettings()
        {
            try
            {
                Storehouse__Old_Peergrade_.Properties.Settings.Default.UserSaved = userSaved;
                Storehouse__Old_Peergrade_.Properties.Settings.Default.Save();
            }
            // У меня не получилось исключение от настроек, но вдруг.
            catch (Exception ex)
            {
                ShowExceptionMessage(ex);
            }
        }

        /// <summary>
        /// Загружает сохраненную настройку - то, что последний открытый файл был сохранен самим пользователем.
        /// </summary>
        public void LoadSettings()
        {
            try
            {
                userSaved = Storehouse__Old_Peergrade_.Properties.Settings.Default.UserSaved;
            }
            catch (Exception ex)
            {
                ShowExceptionMessage(ex);
            }
        }

        /// <summary>
        /// При закрытии формы сохраняет настройку.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveSettings();
        }

        /// <summary>
        /// При открытии формы загружает настройку и спрашивает пользователя, если нужно, о восстановлении данных.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Load(object sender, EventArgs e)
        {
            LoadSettings();
            if (userSaved == false)
            {
                var dialogResult = MessageBox.Show("You have some usaved data from last (or near) session. Restore it?", "Restoring", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    OpenData("backup.bin");
                }
            }
            UserControlSystem.ValidateRestoredData();
        }
    }
}
