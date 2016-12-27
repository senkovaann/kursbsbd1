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

namespace BD9
{
    public partial class Stats : Form
    {
        public Stats()
        {
            InitializeComponent();
        }

        DataTable GetComments()
        {
            DataTable dt = new DataTable();
            try
            {
                Program.conn.Open();
                NpgsqlCommand command = new NpgsqlCommand("SELECT COUNT(id_get) FROM buy_apartments", Program.conn);
                NpgsqlDataReader dr = command.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            Program.conn.Close();
            return dt;
        }

        DataTable GetComments1()
        {
            DataTable dt = new DataTable();
            try
            {
                Program.conn.Open();
                NpgsqlCommand command = new NpgsqlCommand("SELECT clients.name, clients.surname, apartments _info.adress FROM clients INNER JOIN buy_apartments  ON buy_apartments.id-clients = id_clients INNER JOIN apartments _info ON apartments_info.id_apartments  = buy_apartments.id_apartments  WHERE apartments_info.city = 'Yoshakar_Ola'", Program.conn);
                NpgsqlDataReader dr = command.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            Program.conn.Close();
            return dt;
        }

        DataTable GetComments2()
        {
            DataTable dt = new DataTable();
            try
            {
                Program.conn.Open();
                NpgsqlCommand command = new NpgsqlCommand("SELECT seller, SUM(count) AS Sum_count FROM apartments LEFT JOIN apartments_info ON apartments_info.id_apartments = apartments.id_apartments GROUP BY seller ORDER BY Sum_count DESC LIMIT 1", Program.conn);
                NpgsqlDataReader dr = command.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            Program.conn.Close();
            return dt;
        }

        DataTable GetComments3()
        {
            DataTable dt = new DataTable();
            try
            {
                Program.conn.Open();
                NpgsqlCommand command = new NpgsqlCommand("SELECT address, phone, COUNT(filials.id_filials) AS Count_filials FROM filials LEFT JOIN buy_apartments ON buy_apartments.id_filials = filials.id_filials GROUP BY filials.id_filials ORDER BY Count_filials DESC LIMIT 1", Program.conn);
                NpgsqlDataReader dr = command.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            Program.conn.Close();
            return dt;
        }

       

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = GetComments();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = GetComments1();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = GetComments2();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = GetComments3();
        }

    }
}

