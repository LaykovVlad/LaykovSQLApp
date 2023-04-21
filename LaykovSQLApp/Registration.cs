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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace LaykovSQLApp
{
    public partial class Registration : Form
    {
        public Registration()
        {
            InitializeComponent();
            LoginRega.Text = "Введите логин";
            LoginRega.ForeColor = Color.Gray;
            PasswordRega.Text = "Введите пароль";
            PasswordRega.ForeColor = Color.Gray;
            NameRega.Text = "Введите имя";
            NameRega.ForeColor = Color.Gray;
            FamkaRega.Text = "Введите фамилию";
            FamkaRega.ForeColor = Color.Gray;
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Authorization AForm = new Authorization();
            AForm.Show();
            this.Hide();
        }

        private void LoginRega_Enter(object sender, EventArgs e)
        {
            LoginRega.Text = "";
            LoginRega.ForeColor = Color.Black;
        }

        private void LoginRega_Leave(object sender, EventArgs e)
        {
            if (LoginRega.Text == "")
            {
               LoginRega.Text = "Введите логин";
               LoginRega.ForeColor = Color.Gray;
            }
        }

        private void PasswordRega_Enter(object sender, EventArgs e)
        {
            PasswordRega.Text = "";
            PasswordRega.ForeColor = Color.Black;
            PasswordRega.UseSystemPasswordChar = true;
        }

        private void PasswordRega_Leave(object sender, EventArgs e)
        {
            if (PasswordRega.Text == "")
            {
                PasswordRega.Text = "Введите пароль";
                PasswordRega.ForeColor = Color.Gray;
                PasswordRega.UseSystemPasswordChar = false;
            }
        }

        private void NameRega_Enter(object sender, EventArgs e)
        {
            NameRega.Text = "";
            NameRega.ForeColor = Color.Black;
        }

        private void NameRega_Leave(object sender, EventArgs e)
        {
            if (NameRega.Text == "")
            {
                NameRega.Text = "Введите имя";
                NameRega.ForeColor = Color.Gray;
            }
        }

        private void FamkaRega_Enter(object sender, EventArgs e)
        {
            FamkaRega.Text = "";
            FamkaRega.ForeColor = Color.Black;
        }

        private void FamkaRega_Leave(object sender, EventArgs e)
        {
            if (FamkaRega.Text == "")
            {
                FamkaRega.Text = "Введите фамилию";
                FamkaRega.ForeColor = Color.Gray;
            }
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

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            pictureBox1.BackColor = Color.Gray;
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            pictureBox1.BackColor = Color.Transparent;
        }

        private void pictureBox2_MouseEnter(object sender, EventArgs e)
        {
            pictureBox2.BackColor = Color.Gray;
        }

        private void pictureBox2_MouseLeave(object sender, EventArgs e)
        {
            pictureBox2.BackColor = Color.Transparent;
        }

        private void RegaButton_Click(object sender, EventArgs e)
        {
            if (LoginRega.ForeColor == Color.Gray || PasswordRega.ForeColor == Color.Gray || NameRega.ForeColor == Color.Gray || FamkaRega.ForeColor == Color.Gray)
            {
                MessageBox.Show("Заполните поля!");
            }
            else
            {
                if (isUserExists())
                    return;
                DBConnect ConnectData = new DBConnect();
                MySqlCommand command = new MySqlCommand("INSERT INTO `users` (`login`, `password`, `name`, `surname`) VALUES (@login, @password, @name, @surname)", ConnectData.getConnection());

                command.Parameters.Add("@login", MySqlDbType.VarChar).Value = LoginRega.Text;
                command.Parameters.Add("@password", MySqlDbType.VarChar).Value = PasswordRega.Text;
                command.Parameters.Add("@name", MySqlDbType.VarChar).Value = NameRega.Text;
                command.Parameters.Add("@surname", MySqlDbType.VarChar).Value = FamkaRega.Text;

                ConnectData.openConnection();

                if (command.ExecuteNonQuery() == 1)
                    MessageBox.Show("Регистрация успешна!");
                else
                    MessageBox.Show("Ошибка! Аккаунт не зарегистрирован.");
                ConnectData.closeConnection();
            }
        }

        public Boolean isUserExists()
        {
            DBConnect ConnectData = new DBConnect();
            DataTable table = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand("SELECT * FROM `users` WHERE `login` = @uL", ConnectData.getConnection());
            command.Parameters.Add("@uL", MySqlDbType.VarChar).Value = LoginRega.Text;


            adapter.SelectCommand = command;
            adapter.Fill(table);

            if (table.Rows.Count > 0)
            {
                MessageBox.Show("Логин уже зарезервирован!");
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
