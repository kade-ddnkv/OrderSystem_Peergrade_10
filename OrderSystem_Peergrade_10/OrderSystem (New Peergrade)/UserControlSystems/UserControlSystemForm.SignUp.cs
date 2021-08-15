using OrderSystem_Peergrade_10.OrderSystem__New_Peergrade_.Users;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading;

namespace OrderSystem_Peergrade_10.OrderSystem__New_Peergrade_.UserControlSystems
{
    /// <summary>
    /// Файл отвечает за панель регистрации.
    /// </summary>
    public partial class UserControlSystemForm
    {
        /// <summary>
        /// При нажатии кнопки Submit данные, уже проверенные ранее, помещаются в пользователя.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSubmitSignUp_Click(object sender, EventArgs e)
        {
            // Добавляю в список пользователей.
            UserControlSystem.Users.Add(CreateClientFromTextBoxes());
            // Заодно можно обновить панель с пользователями.
            MainForm.RaiseUsersChangedUpdate();
            ShowGreenMessage(labelMessageSignUp, "Registered!");
            buttonSubmitSignUp.Enabled = false;
        }


        private void textBoxNameSignUp_TextChanged(object sender, EventArgs e)
        {
            CheckCorrectnessSignUp();
        }

        private void textBoxSurnameSignUp_TextChanged(object sender, EventArgs e)
        {
            CheckCorrectnessSignUp();
        }

        private void textBoxPatronymicSignUp_TextChanged(object sender, EventArgs e)
        {
            CheckCorrectnessSignUp();
        }

        private void textBoxAdressSignUp_TextChanged(object sender, EventArgs e)
        {
            CheckCorrectnessSignUp();
        }

        private void textBoxPhoneSignUp_TextChanged(object sender, EventArgs e)
        {
            CheckCorrectnessSignUp();
        }

        private void textBoxEmailSignUp_TextChanged(object sender, EventArgs e)
        {
            CheckCorrectnessSignUp();
        }

        private void textBoxPasswordSignUp_TextChanged(object sender, EventArgs e)
        {
            CheckCorrectnessSignUp();
        }

        string[] names = { "Bhisha", "Dro'shanji", "Hassiri", "J'baana", "J'Baasha", "J'bari", "J'Ghasta"
                , "J'mhad", "J'riska", "J'skar", "J'zidzo", "J'zin-Dar", "Ja'Fazir", "K'Sharr", "M'aiq"
                , "Ma'Raska", "Ma'zaddha", "M'dasha", "M'desi", "M'dirr", "M'raaj-Dar", "Ranarr-Jo"
                , "R'vanni", "Ra'dirsha", "Ra'Jahirr", "Ra'Jhan", "Ra'jiradh", "Ri'Bassa", "Ri'Jirr"
                , "Ri'Zakar", "S'drassa", "S'rathad", "S'razirr", "S'shani", "Urjabhi" };

        string[] surnames = { "Ahzini", "Aina", "Ajira", "Anjari", "Arabhi", "Aravi", "Ashidasha", "Bahdahna", "Bahdrashi"
                , "Baissa", "Bhusari", "Chirranirr", "Dahleena", "Dahnara", "Ekapi", "Harassa", "Habasi", "Idhassi", "Inerri"
                , "Inorra", "Kaasha", "Khamuzi", "Khazura", "Khinjarsi", "Kiseena", "Kishni", "Kisimba", "Kisisa", "Nisaba"
                , "Rabinna", "Shaba", "Shivani" };

        string[] locations = { "Morrowind", "Oblivion", "F'ig znae't gde" };

        /// <summary>
        /// Рандомизатор значений для ргистрации.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonRandomizeSignUp_Click(object sender, EventArgs e)
        {
            Random rand = new Random();
            textBoxNameSignUp.Text = names[rand.Next(names.Length)];
            textBoxSurnameSignUp.Text = surnames[rand.Next(surnames.Length)];
            textBoxPatronymicSignUp.Text = names[rand.Next(names.Length)];
            textBoxAdressSignUp.Text = locations[rand.Next(locations.Length)];
            textBoxPhoneSignUp.Text = "+7196425027" + rand.Next(0, 10);
            textBoxEmailSignUp.Text = textBoxSurnameSignUp.Text + rand.Next(10) + "@gmail.com";
            textBoxPasswordSignUp.Text = "111" + rand.Next(0, 10);
        }

        /// <summary>
        /// Проверяет корректность значений в текстовых блоках. Возникает при любом изменении.
        /// </summary>
        private void CheckCorrectnessSignUp()
        {
            bool enableButton;
            // Если текст во всех колонках соответствует требованиям.
            if ((textBoxNameSignUp.Text.Trim() != "")
                && (textBoxSurnameSignUp.Text.Trim() != "")
                && (textBoxPatronymicSignUp.Text.Trim() != "")
                && (textBoxAdressSignUp.Text.Trim() != "")
                && Phone.IsValid(textBoxPhoneSignUp.Text)
                && Email.IsValid(textBoxEmailSignUp.Text)
                && Password.IsValid(textBoxPasswordSignUp.Text)
                )
            {
                var checkClient = CreateClientFromTextBoxes();
                // Проверяется уникальность пользователя.
                if (User.UserAlreadyExists(checkClient))
                {
                    ShowRedWarning(labelMessageSignUp, "Email already exists!");
                    enableButton = false;
                }
                else
                {
                    enableButton = true;
                }
            }
            else
            {
                ShowRedWarning(labelMessageSignUp, "Check gaps!");
                enableButton = false;
            }

            buttonSubmitSignUp.Enabled = enableButton;
        }

        /// <summary>
        /// Создает пользователя по значениям в текстовых блоках.
        /// </summary>
        /// <returns></returns>
        private Client CreateClientFromTextBoxes()
        {
            return new Client(textBoxNameSignUp.Text, textBoxSurnameSignUp.Text, textBoxPatronymicSignUp.Text
                    , textBoxAdressSignUp.Text, new Phone(textBoxPhoneSignUp.Text)
                    , new Email(textBoxEmailSignUp.Text), new Password(textBoxPasswordSignUp.Text));
        }
    }
}
