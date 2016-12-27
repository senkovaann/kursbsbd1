using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;
using System.Configuration;
using System.Data.SqlClient;
using NpgsqlTypes;

namespace BD2
{
    public partial class Autorization : Form
    {
        private int _userId;

        /// <summary>
        /// hard hack)
        /// </summary>
        private const string AdminLogin = "admin";

        public Autorization()
        {
            InitializeComponent();
            string connectionString = ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString;
            Program.conn = new NpgsqlConnection(connectionString);
        }
        
        private bool IsCorrectUser()
        {
            DataTable dataTable = new DataTable();
            try
            {
                Program.conn.Open();
                NpgsqlCommand command = new NpgsqlCommand("select UserId,Password from UserInfo where Name=@username", Program.conn);
                command.Parameters.Add("@username", NpgsqlDbType.Varchar).Value = textBox1.Text;
                NpgsqlDataReader dr = command.ExecuteReader();
                dataTable.Load(dr);
                string password = "";
                if (dataTable.Rows.Count > 0)
                {
                    password = dataTable.Rows[0]["Password"].ToString();
                    _userId = Convert.ToInt32(dataTable.Rows[0]["UserId"]);
                    if (password ==  HashUtil.Md5(textBox2.Text))
                    {
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("Некорректный пользователь или пароль");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Неизвестная ошибка " + ex.Message);
            }
            finally
            {
                Program.conn.Close();
            }
            return false;
        }

        private void InsertNewSession()
        {
            try
            {
                Program.conn.Open();
                NpgsqlCommand command = new NpgsqlCommand("insert into UserSession(UserId,LoginTime) values(@userId,now())", Program.conn);
                command.Parameters.Add("@userId", NpgsqlDbType.Integer).Value = _userId;
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Неизвестная ошибка " + ex.Message);
            }
            finally
            {
                Program.conn.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool isUserCorrect = IsCorrectUser();
            if (!isUserCorrect)
            {
                return;
            }

            bool isAdmin = textBox1.Text == AdminLogin;

            InsertNewSession();
            textBox1.Text = string.Empty;
            textBox2.Text = string.Empty;
            Tables form = new Tables(isAdmin);
            this.Hide();
            form.ShowDialog();
            this.Show();
        }

        private void Autorization_Load(object sender, EventArgs e)
        {

        }
    }
}
