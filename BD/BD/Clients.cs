﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;

namespace BD2
{
    public partial class Clients : Form
    {

        public int j = 0;
        public int x = 1;
        public int y = 5;
        public int del = 0;
        public int count = 0;

        public Clients()
        {
            InitializeComponent();
            dataGridView1.DataSource = GetComments();
        }

        void Standart()
        {
            x = 1;
            j = 0;
            y = 5;
            label10.Text = x.ToString();
            button5.Enabled = false;
            button6.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Standart();
            dataGridView1.DataSource = GetComments();
            create_comb();
        }

        DataTable GetComments()
        {
            DataTable dt = new DataTable();
            try
            {
                Program.conn.Open();
                NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM clients ORDER BY id_clients LIMIT " + y + " offset " + j, Program.conn);
                NpgsqlCommand command1 = new NpgsqlCommand("SELECT COUNT(*) FROM clients", Program.conn);
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
            label10.Text = x.ToString();
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
            label10.Text = x.ToString();
            button5.Enabled = true;
            dataGridView1.DataSource = GetComments();
            if (del < x) button6.Enabled = false;
            if (del > x) button6.Enabled = true;
            y = 5;
        }

        void create_comb()
        {
            comboBox2.Items.Clear();
            comboBox3.Items.Clear();
            try
            {
                Program.conn.Open();
                NpgsqlCommand command2 = new NpgsqlCommand("SELECT * FROM base_clients", Program.conn);
                NpgsqlDataReader dr2 = command2.ExecuteReader();
                foreach (DataGridViewColumn column in dataGridView1.Columns)
                    comboBox3.Items.Add(column.HeaderText);
                comboBox3.Items.Remove(0);
                while (dr2.Read())
                {
                    string id_clients = dr2.GetValue(0).ToString();
                    comboBox2.Items.Add(id_clients);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            comboBox2.Items.Add("");
            comboBox3.Items.Add("");
            comboBox3.Items.Remove("id_clients");
            Program.conn.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                Program.conn.Open();
                string sqlQuery = "INSERT INTO clients (id_clients,name,surname,birth_day,gender,phone) VALUES (@add1, @add2, @add3, @add4, @add5, @add6)";
                NpgsqlCommand command = new NpgsqlCommand(sqlQuery, Program.conn);
                try
                {
                    command.Parameters.Add("@add1", NpgsqlTypes.NpgsqlDbType.Integer).Value = Convert.ToInt32(comboBox2.Text);
                    command.Parameters.Add("@add2", NpgsqlTypes.NpgsqlDbType.Varchar).Value = maskedTextBox1.Text;
                    command.Parameters.Add("@add3", NpgsqlTypes.NpgsqlDbType.Varchar).Value = maskedTextBox2.Text;
                    command.Parameters.Add("@add4", NpgsqlTypes.NpgsqlDbType.Date).Value = Convert.ToDateTime(maskedTextBox3.Text);
                    command.Parameters.Add("@add5", NpgsqlTypes.NpgsqlDbType.Boolean).Value = Convert.ToBoolean(maskedTextBox4.Text);
                    command.Parameters.Add("@add6", NpgsqlTypes.NpgsqlDbType.Varchar).Value = maskedTextBox5.Text;
                    command.ExecuteNonQuery();
                    MessageBox.Show("Sucsess!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }                
                maskedTextBox1.Text = string.Empty;
                maskedTextBox2.Text = string.Empty;
                maskedTextBox3.Text = string.Empty;
                maskedTextBox4.Text = string.Empty;
                maskedTextBox5.Text = string.Empty;
                comboBox2.Text = "";
                comboBox3.Text = "";
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
                string sqlQuery = "DELETE FROM clients WHERE id_clients = @select";
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
                maskedTextBox3.Text = string.Empty;
                maskedTextBox4.Text = string.Empty;
                maskedTextBox5.Text = string.Empty;
                comboBox2.Text = "";
                comboBox3.Text = "";
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
                if (comboBox3.Text == "name")
                {             
                    string sqlQuery = "UPDATE clients SET name = @select1 WHERE id_clients = @select2";
                    NpgsqlCommand command = new NpgsqlCommand(sqlQuery, Program.conn);
                    try
                    {
                        command.Parameters.Add("@select1", NpgsqlTypes.NpgsqlDbType.Varchar).Value = maskedTextBox1.Text;
                        command.Parameters.Add("@select2", NpgsqlTypes.NpgsqlDbType.Integer).Value = Convert.ToInt32(comboBox2.Text);
                        command.ExecuteNonQuery();
                        MessageBox.Show("Sucsess!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                if (comboBox3.Text == "surname")
                {
                    string sqlQuery = "UPDATE clients SET surname = @select1 WHERE id_clients = @select2";
                    NpgsqlCommand command = new NpgsqlCommand(sqlQuery, Program.conn);
                    try
                    {
                        command.Parameters.Add("@select1", NpgsqlTypes.NpgsqlDbType.Varchar).Value = maskedTextBox2.Text;
                        command.Parameters.Add("@select2", NpgsqlTypes.NpgsqlDbType.Integer).Value = Convert.ToInt32(comboBox2.Text);
                        command.ExecuteNonQuery();
                        MessageBox.Show("Sucsess!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                if (comboBox3.Text == "birth_day")
                {
                    string sqlQuery = "UPDATE clients SET birth_day = @select1 WHERE id_clients = @select2";
                    NpgsqlCommand command = new NpgsqlCommand(sqlQuery, Program.conn);
                    try
                    {
                        command.Parameters.Add("@select1", NpgsqlTypes.NpgsqlDbType.Date).Value = Convert.ToDateTime(maskedTextBox3.Text);
                        command.Parameters.Add("@select2", NpgsqlTypes.NpgsqlDbType.Integer).Value = Convert.ToInt32(comboBox2.Text);
                        command.ExecuteNonQuery();
                        MessageBox.Show("Sucsess!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                if (comboBox3.Text == "gender")
                {
                    string sqlQuery = "UPDATE clients SET gender = @select1 WHERE id_clients = @select2";
                    NpgsqlCommand command = new NpgsqlCommand(sqlQuery, Program.conn);
                    try
                    {
                        command.Parameters.Add("@select1", NpgsqlTypes.NpgsqlDbType.Boolean).Value = Convert.ToBoolean(maskedTextBox4.Text);
                        command.Parameters.Add("@select2", NpgsqlTypes.NpgsqlDbType.Integer).Value = Convert.ToInt32(comboBox2.Text);
                        command.ExecuteNonQuery();
                        MessageBox.Show("Sucsess!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                if (comboBox3.Text == "phone")
                {
                    string sqlQuery = "UPDATE clients SET phone = @select1 WHERE id_clients = @select2";
                    NpgsqlCommand command = new NpgsqlCommand(sqlQuery, Program.conn);
                    try
                    {
                        command.Parameters.Add("@select1", NpgsqlTypes.NpgsqlDbType.Varchar).Value = maskedTextBox5.Text;
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
                maskedTextBox3.Text = string.Empty;
                maskedTextBox4.Text = string.Empty;
                maskedTextBox5.Text = string.Empty;
                comboBox2.Text = "";
                comboBox3.Text = "";
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

