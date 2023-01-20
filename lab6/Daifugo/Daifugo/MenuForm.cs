using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Daifugo
{
    public partial class MenuForm : Form
    {
        List<int> players = new List<int>();
        BindingSource playersBS = new BindingSource();
        public MenuForm()
        {
            InitializeComponent();
            for (int i = 1; i < 10; i++)            
                players.Add(i + 1);           
        }

        private void Menu_Load(object sender, EventArgs e)
        {
            playersBS.DataSource = players;
            playersComboBox.DataSource = playersBS;
        }

        private void playButton_Click(object sender, EventArgs e)
        {
            if (int.TryParse(playersComboBox.Text, out int choice))
            {
                GameForm game = new GameForm(this, choice);
                game.Show();
                this.Hide();
            }
        }
    }
}
