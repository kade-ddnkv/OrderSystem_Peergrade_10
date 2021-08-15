using OrderSystem_Peergrade_10.OrderSystem__New_Peergrade_.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderSystem_Peergrade_10.OrderSystem__New_Peergrade_.UserControlSystems
{
    /// <summary>
    /// Файл отвечает за панель логина.
    /// </summary>
    public partial class UserControlSystemForm
    {
        /// <summary>
        /// При нажатии Login проверяет корректность данных и либо продолжает, либо останавливает пользователя.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonLoginSignIn_Click(object sender, EventArgs e)
        {
            if (CheckCorrectnessSignIn() == true)
            {
                UserControlSystem.CurrentUser = UserControlSystem.FindUserByEmail(textBoxEmailSignIn.Text);
                // При смене юзера нужно обновить доступ к панели с пользователями.
                MainForm.RaiseUsersChangedUpdate();
                Close();
            }
            else
            {
                ShowRedWarning(labelMessageSignIn, "Wrong email or pwd!");
            }
        }

        private void textBoxEmailSignIn_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void textBoxPasswordSignIn_TextChanged(object sender, EventArgs e)
        {
            
        }

        /// <summary>
        /// Проверяет корректность поданных данных.
        /// </summary>
        /// <returns></returns>
        private bool CheckCorrectnessSignIn()
        {
            User foundUser = UserControlSystem.FindUserByEmail(textBoxEmailSignIn.Text);
            if (foundUser != null && foundUser.Password.Equals(new Password(textBoxPasswordSignIn.Text)))
            {
                return true;
            }
            return false;
        }
    }
}
