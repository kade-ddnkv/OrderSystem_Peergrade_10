using OrderSystem_Peergrade_10.OrderSystem__New_Peergrade_.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace OrderSystem_Peergrade_10.OrderSystem__New_Peergrade_.UserControlSystems
{
    /// <summary>
    /// Класс, отвечающий за общую работы формы по регистрации и логину.
    /// </summary>
    public partial class UserControlSystemForm : Form
    {
        public UserControlSystemForm()
        {
            InitializeComponent();
            
            buttonSignInPanel_Click(new object(), new EventArgs());
            buttonSignInPanel.Select();
            
            panelSignUp.Dock = DockStyle.Fill;
            panelSignIn.Dock = DockStyle.Fill;

            buttonSubmitSignUp.Enabled = false;

            FormClosing += UserControlSystemForm_FormClosing;
        }

        /// <summary>
        /// Форма не закроется, если пользователь не залогинен.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserControlSystemForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (UserControlSystem.CurrentUser == null)
            {
                e.Cancel = true;
                MessageBox.Show("Please, sign in firstly.");
            }
        }

        //Либо показывает панель регистрации, либо панель логина.

        private void buttonSignUpPanel_Click(object sender, EventArgs e)
        {
            panelSignIn.Visible = false;
            panelSignUp.Visible = true;
        }

        private void buttonSignInPanel_Click(object sender, EventArgs e)
        {
            panelSignIn.Visible = true;
            panelSignUp.Visible = false;
        }

        /*
        private void WaitSecond(object label)
        {
            (label as Label).Visible = true;
            Thread.Sleep(1000);
            (label as Label).Visible = false;
        }
        */

        /// <summary>
        /// Сообщает пользователю зеленое сообщение.
        /// </summary>
        /// <param name="label"></param>
        /// <param name="text"></param>
        private void ShowGreenMessage(Label label, string text)
        {
            label.Text = text;
            label.ForeColor = Color.Green;
            label.Visible = true;
            // С потоками не получилось.
            //Thread t = new Thread(WaitSecond);
            //t.Start();
            //label.Visible = false;
        }

        /// <summary>
        /// Сообщает пользователю красное предупреждение.
        /// </summary>
        /// <param name="label"></param>
        /// <param name="text"></param>
        private void ShowRedWarning(Label label, string text)
        {
            label.Text = text;
            label.ForeColor = Color.Red;
            label.Visible = true;
            //Thread t = new Thread(new ParameterizedThreadStart(WaitSecond));
            //t.Start(label);
        }
    }
}
