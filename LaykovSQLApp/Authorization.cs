using MySqlConnector;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LaykovSQLApp
{
    public partial class Authorization : Form
    {
        public Authorization()
        {
            InitializeComponent();
            Login.Text = "Введите логин";
            Login.ForeColor = Color.Gray;
            Password.Text = "Введите пароль";
            Password.ForeColor = Color.Gray;
        }

        private void Login_Enter(object sender, EventArgs e)
        {
            Login.Text = "";
            Login.ForeColor = Color.Black;
        }

        private void Login_Leave(object sender, EventArgs e)
        {
            if (Login.Text == "")
            {
                Login.Text = "Введите логин";
                Login.ForeColor = Color.Gray;
            }
        }

        private void Password_Enter(object sender, EventArgs e)
        {
            Password.Text = "";
            Password.ForeColor = Color.Black;
            Password.UseSystemPasswordChar = true;
            pictureBox6.Image = LaykovSQLApp.Properties.Resources.hidden;
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            if (Password.UseSystemPasswordChar == true)
            {
                Password.UseSystemPasswordChar = false;
                pictureBox6.Image = LaykovSQLApp.Properties.Resources.eye;
            }
            else
            {
                Password.UseSystemPasswordChar = true;
                pictureBox6.Image = LaykovSQLApp.Properties.Resources.hidden;
            }
        }

        private void Password_Leave(object sender, EventArgs e)
        {
            if (Password.Text == "")
            {
                Password.Text = "Введите пароль";
                Password.ForeColor = Color.Gray;
                Password.UseSystemPasswordChar = false;
                pictureBox6.Image = LaykovSQLApp.Properties.Resources.eye;
            }
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            ViewData VIEWF = new ViewData();
            String LoginU = Login.Text;
            String PasswordU = Password.Text;
            DBConnect ConnectData = new DBConnect();
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            if (Login.ForeColor == Color.Gray || Password.ForeColor == Color.Gray)
            {
                MessageBox.Show("Заполните поля!");

            }
            else
            {
                MySqlCommand command = new MySqlCommand("SELECT * FROM `users` WHERE `login` = @uL AND `password` = @uP", ConnectData.getConnection());
                command.Parameters.Add("@uL", MySqlDbType.VarChar).Value = LoginU;
                command.Parameters.Add("@uP", MySqlDbType.VarChar).Value = PasswordU;

                adapter.SelectCommand = command;
                adapter.Fill(table);

                if (table.Rows.Count > 0)
                {
                    MessageBox.Show("Успешный вход!");
                    VIEWF.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Данные неверные / Аккаунт не зарегистрирован!");
                }

            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Registration RForm = new Registration();
            RForm.Show();
            this.Hide();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("Выйти из приложения?", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation))
            {
                Application.Exit();
            }
            else
            {
                return;
            }
        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            pictureBox1.BackColor = Color.Gray;
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            pictureBox1.BackColor = Color.Transparent;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void pictureBox2_MouseEnter(object sender, EventArgs e)
        {
            pictureBox2.BackColor = Color.Gray;
        }

        private void pictureBox2_MouseLeave(object sender, EventArgs e)
        {
            pictureBox2.BackColor = Color.Transparent;
        }
    }
}
