using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql;
using MySql.Data.MySqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace jason
{
    public partial class Form1 : Form
    {
        string connectionString = "server=localhost; uid=root; pwd=; database=premier_league";
        MySqlConnection sqlConnect;
        MySqlCommand sqlCommand;
        MySqlDataAdapter sqlAdapter;
        string sqlQuery;

        DataTable dttambahplayer = new DataTable();
        DataTable teamname = new DataTable();
        DataTable manager = new DataTable();
        DataTable Managerrekruit = new DataTable();
        
        public Form1()
        {
            InitializeComponent();
        }

        
        private void Form1_Load(object sender, EventArgs e)
        {
            paneljembot.Visible = false;
            panel2.Visible = false;

            sqlConnect = new MySqlConnection(connectionString);
            sqlQuery = "select t.team_name from team t;";
            sqlCommand = new MySqlCommand(sqlQuery, sqlConnect);
            sqlAdapter = new MySqlDataAdapter(sqlCommand);
            sqlAdapter.Fill(teamname);
            teamnamecombobox.DataSource = teamname;
            teamnamecombobox.ValueMember = "team_name";
            teamnamecombobox.DisplayMember = "team_name";
            /// ini datatable player
            
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void addPlayerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            paneljembot.Visible = true;
            panel2.Visible = false;
        }

        private void buttonSubmit_Click(object sender, EventArgs e)
        {
            sqlConnect.Open();
            sqlQuery = $""
        }

        private void editManagerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            paneljembot.Visible = false;
            panel2.Visible = true;

            sqlConnect = new MySqlConnection(connectionString);
            sqlQuery = "select t.team_name from team t;";
            sqlCommand = new MySqlCommand(sqlQuery, sqlConnect);
            sqlAdapter = new MySqlDataAdapter(sqlCommand);
            sqlAdapter.Fill(teamname);
            comboBox1.DataSource = teamname;
            comboBox1.ValueMember = "team_name";
            comboBox1.DisplayMember = "team_name";
            ///
            sqlConnect = new MySqlConnection(connectionString);
            sqlQuery = "select m.manager_name as nama, n.nation as nation, m.birthdate as birthday, if (m.working = 1, 'Available', 'Not') as status from manager m inner join nationality n on n.nationality_id = m.nationality_id and m.working = 1;";
            sqlCommand = new MySqlCommand(sqlQuery, sqlConnect);
            sqlAdapter = new MySqlDataAdapter(sqlCommand);
            sqlAdapter.Fill(Managerrekruit);
            dataGridView2.DataSource = Managerrekruit;
        }

        private void teamnamecombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            dttambahplayer.Rows.Clear();
            sqlConnect = new MySqlConnection(connectionString);
            sqlQuery = $"select p.player_name as nama, p.team_number as angkatim, p.nationality_id as nation, p.playing_pos as pos, p.height as height, p.weight as weight, p.birthdate as birthday, t.team_name as namatim from player p, team t where p.team_id = t.team_id AND t.team_name = '{teamnamecombobox.SelectedValue}';";
            sqlCommand = new MySqlCommand(sqlQuery, sqlConnect);
            sqlAdapter = new MySqlDataAdapter(sqlCommand);
            sqlAdapter.Fill(dttambahplayer);
            dataGridViewlistplayer.DataSource = dttambahplayer;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            manager.Rows.Clear();
            sqlConnect = new MySqlConnection(connectionString);
            sqlQuery = $"select m.manager_name as nama, t.team_name as nama_tim, m.birthdate as birthday, n.nation as nation from manager m, nationality n, team t where m.manager_id = t.manager_id and m.nationality_id = n.nationality_id and t.team_name = '{comboBox1.SelectedValue}';";
            sqlCommand = new MySqlCommand(sqlQuery, sqlConnect);
            sqlAdapter = new MySqlDataAdapter(sqlCommand);
            sqlAdapter.Fill(manager);
            dataGridView1.DataSource = manager;
        }
    }
}
