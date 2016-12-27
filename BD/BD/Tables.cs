using BD3;
using BD4;
using BD5;
using BD6;
using BD7;
using BD8;
using BD9;
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
    public partial class Tables : Form
    {
        private bool _isAdmin;

        public Tables(bool isAdmin)
        {
            InitializeComponent();
            _isAdmin = isAdmin;
            users.Visible = _isAdmin;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Base_clients form = new Base_clients();
            form.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Clients form = new Clients();
            form.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Apartments form = new Apartments();
            form.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Apartments_info form = new Apartments_info();
            form.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Filials form = new Filials();
            form.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Users form = new Users();
            form.ShowDialog();

        }

        private void button7_Click(object sender, EventArgs e)
        {
            Buy_apartments form = new Buy_apartments();
            form.ShowDialog();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Sessions form = new Sessions();
            form.ShowDialog();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Stats form = new Stats();
            form.ShowDialog();
        }

        private void users_Click(object sender, EventArgs e)
        {
            AddUser form = new AddUser();
            form.ShowDialog();
        }
    }
}
