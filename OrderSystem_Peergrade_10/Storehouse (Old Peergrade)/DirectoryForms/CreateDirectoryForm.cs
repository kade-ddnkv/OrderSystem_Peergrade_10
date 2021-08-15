using System;
using System.Windows.Forms;

namespace OrderSystem_Peergrade_10.Storehouse__Old_Peergrade_
{
    public partial class CreateDirectoryForm : Form
    {
        // Сделано по тому же типу, как и CreateProductForm.
        /// <summary>
        /// Конструктор формы для создания директории.
        /// </summary>
        public CreateDirectoryForm()
        {
            InitializeComponent();
            button1.DialogResult = DialogResult.OK;
            button2.DialogResult = DialogResult.Cancel;
            button1.Enabled = false;
        }

        /// <summary>
        /// Конструктор, дополнительно помещающий значение name в textBox.
        /// </summary>
        /// <param name="name"></param>
        public CreateDirectoryForm(string name) : this()
        {
            textBox1.Text = name;
        }

        /// <summary>
        /// Получает созданную директорию после ответа пользователя.
        /// </summary>
        /// <exception cref="CancelledActionException"/>
        /// <returns></returns>
        public Directory GetDirectory()
        {
            if (ShowDialog() != DialogResult.Cancel)
            {
                return new Directory(textBox1.Text);
            }
            throw new CancelledActionException();
        }

        /// <summary>
        /// Получает только имя директории из формы.
        /// </summary>
        /// <returns></returns>
        public string GetDirectoryName()
        {
            if (ShowDialog() != DialogResult.Cancel)
            {
                return textBox1.Text;
            }
            throw new CancelledActionException();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            CheckCorrectness();
        }

        /// <summary>
        /// Проверяет все textBox-ы на корректность значений в них.
        /// </summary>
        private void CheckCorrectness()
        {
            button1.Enabled = textBox1.Text.Trim().Length > 0;
        }
    }
}
