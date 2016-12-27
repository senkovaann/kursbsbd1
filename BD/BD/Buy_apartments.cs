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

namespace BD7
{
    public partial class Buy_apartments  : Form
    {

        public int j = 0;
        public int x = 1;
        public int y = 5;
        public int del = 0;
        public int count = 0;

        public Buy_apartments()
        {
            InitializeComponent();
            dataGridView1.DataSource = GetComments();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Standart();
            dataGridView1.DataSource = GetComments();
            create_comb();
        }

        void Standart()
        {
            x = 1;
            j = 0;
            y = 5;
            label11.Text = x.ToString();
            button5.Enabled = false;
            button6.Enabled = true;
        }

        DataTable GetComments()
        {
            DataTable dt = new DataTable();
            try
            {
                Program.conn.Open();
                NpgsqlCommand command = new NpgsqlCommand("SELECT buy_apartments.id_get, Buy_apartments.date_vaplat FROM buy_apartments " +
                "INNER JOIN apartments_info ON apartments_info.id_apartments = buy_apartments.id_apartments INNER JOIN clients ON clients.id_clients = Buy_apartments.id_clients " +
                "ORDER BY buy_apartments.id_get LIMIT " + y + " offset " + j, Program.conn);
                NpgsqlCommand command1 = new NpgsqlCommand("SELECT COUNT(*) FROM Buy_apartments", Program.conn);
                NpgsqlDataReader dr = command.ExecuteReader();
                count = Convert.ToInt32(command1.ExecuteScalar());
                del = count / 5;
                dt.Load(dr);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            Program.conn.Close();
            return dt;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            j = j - 5;
            y = 5;
            x = x - 1;
            label11.Text = x.ToString();
            dataGridView1.DataSource = GetComments();
            if (x == 1) button5.Enabled = false;
            if (x != 1) button5.Enabled = true;
            button6.Enabled = true;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            j = j + 5;
            if (x == del) y = count % 5;
            x = x + 1;
            label11.Text = x.ToString();
            button5.Enabled = true;
            dataGridView1.DataSource = GetComments();
            if (del < x) button6.Enabled = false;
            if (del > x) button6.Enabled = true;
            y = 5;
        }

        void create_comb()
        {
            comboBox1.Items.Clear();
            comboBox2.Items.Clear();
            comboBox3.Items.Clear();
            comboBox4.Items.Clear();
            comboBox5.Items.Clear();
            comboBox6.Items.Clear();
            comboBox7.Items.Clear();
            comboBox8.Items.Clear();
            try
            {
                Program.conn.Open();
                NpgsqlCommand command2 = new NpgsqlCommand("SELECT * FROM buy_apartments", Program.conn);
                NpgsqlDataReader dr2 = command2.ExecuteReader();
                NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM filials", Program.conn);
                NpgsqlDataReader dr = command.ExecuteReader();
                NpgsqlCommand command3 = new NpgsqlCommand("SELECT * FROM apartments_info", Program.conn);
                NpgsqlDataReader dr3 = command3.ExecuteReader();
                NpgsqlCommand command4 = new NpgsqlCommand("SELECT * FROM users", Program.conn);
                NpgsqlDataReader dr4 = command4.ExecuteReader();
                NpgsqlCommand command5 = new NpgsqlCommand("SELECT * FROM clients", Program.conn);
                NpgsqlDataReader dr5 = command5.ExecuteReader();
                foreach (DataGridViewColumn column in dataGridView1.Columns)
                    comboBox3.Items.Add(column.HeaderText);
                comboBox3.Items.Remove(0);
                while (dr2.Read())
                {
                    string id_get = dr2.GetValue(0).ToString();
                    comboBox2.Items.Add(id_get);
                }
                while (dr.Read())
                {
                    string id_filials = dr.GetValue(0).ToString();
                    comboBox1.Items.Add(id_filials);
                }
                while (dr3.Read())
                {
                    string city = dr3.GetValue(1).ToString();
                    string adress = dr3.GetValue(2).ToString();
                    comboBox4.Items.Add(city);
                    comboBox7.Items.Add(adress);
                }
                while (dr4.Read())
                {
                    string id_users = dr4.GetValue(0).ToString();
                    comboBox6.Items.Add(id_users);
                }
                while (dr5.Read())
                {
                    string name = dr5.GetValue(1).ToString();
                    string surname = dr5.GetValue(2).ToString();
                    comboBox5.Items.Add(name);
                    comboBox8.Items.Add(surname);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            comboBox1.Items.Add("");
            comboBox2.Items.Add("");
            comboBox3.Items.Add("apartments");
            comboBox3.Items.Add("client");
            comboBox3.Items.Add("");
            comboBox4.Items.Add("");
            comboBox5.Items.Add("");
            comboBox6.Items.Add("");
            comboBox3.Items.Remove("id_get");
            comboBox3.Items.Remove("id_apartments");
            comboBox3.Items.Remove("id_clients");
            comboBox3.Items.Remove("city");
            comboBox3.Items.Remove("adress");
            comboBox3.Items.Remove("name");
            comboBox3.Items.Remove("surname");
            Program.conn.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                Program.conn.Open();
                string Text1 = "";
                string Text2 = "";
                string sqlQuery1 = "SELECT id_apartments FROM apartments_info WHERE city = @select1 AND adress = @select2";
                string sqlQuery2 = "SELECT id_clients FROM clients WHERE name = @select3 AND surname = @select4";
                NpgsqlCommand command1 = new NpgsqlCommand(sqlQuery1, Program.conn);
                NpgsqlCommand command2 = new NpgsqlCommand(sqlQuery2, Program.conn);
                try
                {
                    command1.Parameters.Add("@select1", NpgsqlTypes.NpgsqlDbType.Varchar).Value = comboBox4.Text;
                    command1.Parameters.Add("@select2", NpgsqlTypes.NpgsqlDbType.Varchar).Value = comboBox7.Text;
                    command2.Parameters.Add("@select3", NpgsqlTypes.NpgsqlDbType.Varchar).Value = comboBox5.Text;
                    command2.Parameters.Add("@select4", NpgsqlTypes.NpgsqlDbType.Varchar).Value = comboBox8.Text;
                    NpgsqlDataReader dr1 = command1.ExecuteReader();
                    NpgsqlDataReader dr2 = command2.ExecuteReader();
                    Text1 = Convert.ToString(command1.ExecuteScalar());
                    Text2 = Convert.ToString(command2.ExecuteScalar());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                string sqlQuery = "INSERT INTO buy_apartments(id_apartments,id_clients, id_users,id_filials,date_prodachi,date_vaplat) VALUES (@add1, @add2, @add3, @add4, @add5, @add6)";
                NpgsqlCommand command = new NpgsqlCommand(sqlQuery, Program.conn);
                try
                {
                    command.Parameters.Add("@add1", NpgsqlTypes.NpgsqlDbType.Integer).Value = Convert.ToInt32(Text1);
                    command.Parameters.Add("@add2", NpgsqlTypes.NpgsqlDbType.Integer).Value = Convert.ToInt32(Text2);
                    command.Parameters.Add("@add3", NpgsqlTypes.NpgsqlDbType.Integer).Value = Convert.ToInt32(comboBox6.Text);
                    command.Parameters.Add("@add4", NpgsqlTypes.NpgsqlDbType.Integer).Value = Convert.ToInt32(comboBox1.Text);
                    command.Parameters.Add("@add5", NpgsqlTypes.NpgsqlDbType.Date).Value = Convert.ToDateTime(maskedTextBox1.Text);
                    command.Parameters.Add("@add6", NpgsqlTypes.NpgsqlDbType.Date).Value = Convert.ToDateTime(maskedTextBox2.Text);
                    command.ExecuteNonQuery();
                    MessageBox.Show("Sucsess!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                maskedTextBox1.Text = string.Empty;
                maskedTextBox2.Text = string.Empty;
                comboBox1.Text = "";
                comboBox2.Text = "";
                comboBox3.Text = "";
                comboBox4.Text = "";
                comboBox5.Text = "";
                comboBox6.Text = "";
                comboBox7.Text = "";
                comboBox8.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            Program.conn.Close();
            Standart();
            dataGridView1.DataSource = GetComments();
            create_comb();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                Program.conn.Open();
                string sqlQuery = "DELETE FROM buy_apartments   WHERE id_get = @select";
                NpgsqlCommand command = new NpgsqlCommand(sqlQuery, Program.conn);
                try
                {
                    command.Parameters.Add("@select", NpgsqlTypes.NpgsqlDbType.Integer).Value = Convert.ToInt32(comboBox2.Text);
                    command.ExecuteNonQuery();
                    MessageBox.Show("Sucsess!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                maskedTextBox1.Text = string.Empty;
                maskedTextBox2.Text = string.Empty;
                comboBox1.Text = "";
                comboBox2.Text = "";
                comboBox3.Text = "";
                comboBox4.Text = "";
                comboBox5.Text = "";
                comboBox6.Text = "";
                comboBox7.Text = "";
                comboBox8.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            Program.conn.Close();
            Standart();
            dataGridView1.DataSource = GetComments();
            create_comb();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                Program.conn.Open();
                if (comboBox3.Text == "apartments ")
                {
                    string Text1 = "";
                    string sqlQuery1 = "SELECT id_apartments  FROM apartments _info WHERE city = @select3 AND adress = @select4";
                    NpgsqlCommand command1 = new NpgsqlCommand(sqlQuery1, Program.conn);
                    try
                    {
                        command1.Parameters.Add("@select3", NpgsqlTypes.NpgsqlDbType.Varchar).Value = comboBox4.Text;
                        command1.Parameters.Add("@select4", NpgsqlTypes.NpgsqlDbType.Varchar).Value = comboBox7.Text;
                        NpgsqlDataReader dr1 = command1.ExecuteReader();
                        Text1 = Convert.ToString(command1.ExecuteScalar());
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    string sqlQuery = "UPDATE buy_apartments  SET id_apartments  = @select1 WHERE id_get = @select2";
                    NpgsqlCommand command = new NpgsqlCommand(sqlQuery, Program.conn);
                    try
                    {
                        command.Parameters.Add("@select1", NpgsqlTypes.NpgsqlDbType.Integer).Value = Convert.ToInt32(Text1);
                        command.Parameters.Add("@select2", NpgsqlTypes.NpgsqlDbType.Integer).Value = Convert.ToInt32(comboBox2.Text);
                        command.ExecuteNonQuery();
                        MessageBox.Show("Sucsess!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                if (comboBox3.Text == "client")
                {
                    string Text2 = "";
                    string sqlQuery2 = "SELECT id_clients FROM clients WHERE name = @select3 AND surname = @select4";
                    NpgsqlCommand command2 = new NpgsqlCommand(sqlQuery2, Program.conn);
                    try
                    {
                        command2.Parameters.Add("@select3", NpgsqlTypes.NpgsqlDbType.Varchar).Value = comboBox5.Text;
                        command2.Parameters.Add("@select4", NpgsqlTypes.NpgsqlDbType.Varchar).Value = comboBox8.Text;
                        NpgsqlDataReader dr2 = command2.ExecuteReader();
                        Text2 = Convert.ToString(command2.ExecuteScalar());
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    string sqlQuery = "UPDATE buy_apartments  SET id_clients = @select1 WHERE id_get = @select2";
                    NpgsqlCommand command = new NpgsqlCommand(sqlQuery, Program.conn);
                    try
                    {
                        command.Parameters.Add("@select1", NpgsqlTypes.NpgsqlDbType.Integer).Value = Convert.ToInt32(Text2);
                        command.Parameters.Add("@select2", NpgsqlTypes.NpgsqlDbType.Integer).Value = Convert.ToInt32(comboBox2.Text);
                        command.ExecuteNonQuery();
                        MessageBox.Show("Sucsess!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                if (comboBox3.Text == "id_users")
                {
                    string sqlQuery = "UPDATE buy_apartments  SET id_users = @select1 WHERE id_get = @select2";
                    NpgsqlCommand command = new NpgsqlCommand(sqlQuery, Program.conn);
                    try
                    {
                        command.Parameters.Add("@select1", NpgsqlTypes.NpgsqlDbType.Integer).Value = Convert.ToInt32(comboBox6.Text);
                        command.Parameters.Add("@select2", NpgsqlTypes.NpgsqlDbType.Integer).Value = Convert.ToInt32(comboBox2.Text);
                        command.ExecuteNonQuery();
                        MessageBox.Show("Sucsess!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                if (comboBox3.Text == "id_filials")
                {
                    string sqlQuery = "UPDATE buy_apartments  SET id_filials = @select1 WHERE id_get = @select2";
                    NpgsqlCommand command = new NpgsqlCommand(sqlQuery, Program.conn);
                    try
                    {
                        command.Parameters.Add("@select1", NpgsqlTypes.NpgsqlDbType.Integer).Value = Convert.ToInt32(comboBox1.Text);
                        command.Parameters.Add("@select2", NpgsqlTypes.NpgsqlDbType.Integer).Value = Convert.ToInt32(comboBox2.Text);
                        command.ExecuteNonQuery();
                        MessageBox.Show("Sucsess!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                if (comboBox3.Text == "date_prodachi")
                {
                    string sqlQuery = "UPDATE buy_apartments  SET date_prodachi = @select1 WHERE date_prodachi = @select2";
                    NpgsqlCommand command = new NpgsqlCommand(sqlQuery, Program.conn);
                    try
                    {
                        command.Parameters.Add("@select1", NpgsqlTypes.NpgsqlDbType.Date).Value = Convert.ToDateTime(maskedTextBox1.Text);
                        command.Parameters.Add("@select2", NpgsqlTypes.NpgsqlDbType.Integer).Value = Convert.ToInt32(comboBox2.Text);
                        command.ExecuteNonQuery();
                        MessageBox.Show("Sucsess!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                if (comboBox3.Text == "date_vaplat ")
                {
                    string sqlQuery = "UPDATE buy_apartments  SET date_vaplat  = @select1 WHERE id_get = @select2";
                    NpgsqlCommand command = new NpgsqlCommand(sqlQuery, Program.conn);
                    try
                    {
                        command.Parameters.Add("@select1", NpgsqlTypes.NpgsqlDbType.Date).Value = Convert.ToDateTime(maskedTextBox2.Text);
                        command.Parameters.Add("@select2", NpgsqlTypes.NpgsqlDbType.Integer).Value = Convert.ToInt32(comboBox2.Text);
                        command.ExecuteNonQuery();
                        MessageBox.Show("Sucsess!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                maskedTextBox1.Text = string.Empty;
                maskedTextBox2.Text = string.Empty;
                comboBox1.Text = "";
                comboBox2.Text = "";
                comboBox3.Text = "";
                comboBox4.Text = "";
                comboBox5.Text = "";
                comboBox6.Text = "";
                comboBox7.Text = "";
                comboBox8.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            Program.conn.Close();
            Standart();
            dataGridView1.DataSource = GetComments();
            create_comb();
        }
    }
}

