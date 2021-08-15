using System;
using System.Windows.Forms;

namespace OrderSystem_Peergrade_10.Storehouse__Old_Peergrade_.RandomizeForms
{
    public partial class RandomizeForm : Form
    {
        // Сделано по тому же типу, как и CreateProductForm.

        uint numberOfTextBox1;
        uint numberOfTextBox2;

        /// <summary>
        /// Конструктор формы для получения настроек рандомизации.
        /// </summary>
        public RandomizeForm()
        {
            InitializeComponent();
            button1.DialogResult = DialogResult.OK;
            button2.DialogResult = DialogResult.Cancel;
            button1.Enabled = false;
        }

        /// <summary>
        /// Получает от пользователя настройки рандомизации.
        /// </summary>
        /// <exception cref="CancelledActionException"/>
        /// <returns></returns>
        public (uint dirCount, uint productCount) GetRandomizationSettings()
        {
            if (ShowDialog() != DialogResult.Cancel)
            {
                // Значения в колонках уже корректны.
                return (numberOfTextBox1, numberOfTextBox2);
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

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            CheckCorrectness();
        }

        /// <summary>
        /// Проверяет все textBox-ы на корректность значений в них.
        /// </summary>
        private void CheckCorrectness()
        {
            button1.Enabled = uint.TryParse(textBox1.Text, out numberOfTextBox1)
                & uint.TryParse(textBox2.Text, out numberOfTextBox2);
        }
    }
}
