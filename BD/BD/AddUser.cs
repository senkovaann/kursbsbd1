using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BD2
{
    public partial class AddUser : Form
    {
        public AddUser()
        {
            InitializeComponent();
            FillUsers();
        }

        private void add_Click(object sender, EventArgs e)
        {   
            if ( String.IsNullOrEmpty(username.Text) || String.IsNullOrWhiteSpace(password.Text))
            {
                MessageBox.Show("Логин или пароль не могут быть пустыми");
                return;
            }

            InsertNewUser();
        }

        private void FillUsers()
        {
            DataTable dataTable = new DataTable();
            try
            {
                Program.conn.Open();
                NpgsqlCommand command = new NpgsqlCommand("select Name from UserInfo", Program.conn);
                NpgsqlDataReader dr = command.ExecuteReader();
                dataTable.Load(dr);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Неизвестная ошибка");
            }
            finally
            {
                Program.conn.Close();
            }
            dataGridView1.DataSource = dataTable;
        }

        private void InsertNewUser()
        {
            try
            {
                Program.conn.Open();
                NpgsqlCommand command = new NpgsqlCommand("insert into UserInfo(Name,Password) values(@username,@password)", Program.conn);
                command.Parameters.Add("@username", NpgsqlDbType.Varchar).Value = username.Text;
                command.Parameters.Add("@password", NpgsqlDbType.Varchar).Value = HashUtil.Md5(password.Text);
                command.ExecuteNonQuery();
                MessageBox.Show("Пользователь успешно добавлен");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Неизвестная ошибка");
            }
            finally
            {
                Program.conn.Close();
            }
        }

        private void update_Click(object sender, EventArgs e)
        {
            FillUsers();
        }
    }
}
