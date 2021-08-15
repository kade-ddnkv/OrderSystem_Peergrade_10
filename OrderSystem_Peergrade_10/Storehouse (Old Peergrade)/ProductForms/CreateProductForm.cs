using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Forms;

namespace OrderSystem_Peergrade_10.Storehouse__Old_Peergrade_.ProductForms
{
    public partial class CreateProductForm : Form
    {
        /// <summary>
        /// Конструктор формы для создания товара.
        /// </summary>
        public CreateProductForm()
        {
            InitializeComponent();
            button1.DialogResult = DialogResult.OK;
            button2.DialogResult = DialogResult.Cancel;
            button1.Enabled = false;
        }

        /// <summary>
        /// Конструктор, дополнительно помещающий значения в textBox-ы.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="article"></param>
        /// <param name="count"></param>
        /// <param name="minCount"></param>
        /// <param name="price"></param>
        public CreateProductForm(string name, string article, string count, string minCount, string price) : this()
        {
            textBox1.Text = name;
            textBox2.Text = article;
            textBox3.Text = count;
            textBox4.Text = minCount;
            textBox5.Text = price;
        }

        /// <summary>
        /// Получает созданный пользователем продукт.
        /// </summary>
        /// <exception cref="CancelledActionException"/>
        /// <returns></returns>
        public Product GetProduct()
        {
            if (ShowDialog() != DialogResult.Cancel)
            {
                // Если нажата кнопка ОК, значит, значения в колонках уже корректны.
                return new Product(new List<string>() { textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text });
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

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            CheckCorrectness();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            CheckCorrectness();
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            CheckCorrectness();
        }

        /// <summary>
        /// Проверяет все textBox-ы на корректность значений в них.
        /// </summary>
        private void CheckCorrectness()
        {
            button1.Enabled = textBox1.Text.Trim().Length > 0
                & uint.TryParse(textBox2.Text, out uint number2)
                & uint.TryParse(textBox3.Text, out uint number3)
                & uint.TryParse(textBox4.Text, out uint number4)
                & double.TryParse(textBox5.Text, NumberStyles.Float ^ NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture, out double number5);
        }
    }
}
