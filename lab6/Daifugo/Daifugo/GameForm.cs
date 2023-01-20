using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ConsoleDaifugo;
using Daifugo;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace Daifugo
{
    public partial class GameForm : Form
    {
        int curPlayerId;
        int playerAmount;
        const string imgPath = @"C:/Users/pavle/source/repos/Daifugo/Daifugo/CardsImg/";
        List<List<Card>> playerMoves;
        BindingSource movesBS = new BindingSource();
        MenuForm menu;
        Game game;

        List<PictureBox> handPictures = new List<PictureBox>();
        List<PictureBox> tablePictures = new List<PictureBox>();
        public GameForm(MenuForm menuForm, int playerAmount)
        {           
            menu = menuForm;
            InitializeComponent();
            this.playerAmount = playerAmount;
            game = new Game(playerAmount);
        }

        private void Game_Load(object sender, EventArgs e)
        {
            Random rand = new Random();
            curPlayerId = rand.Next(playerAmount);
            UpdateControls(game);
            UpdateTable(game);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void nextPlayerBtn_Click(object sender, EventArgs e)
        {
            for (int i = 0; i<game._players.Count; i++)            
                label1.Text += "Player " + i + ": " + game._players[i]._hand.Count() + "\n";   
            
            if (curPlayerId != 0)
            {
                BotMove(game);
                UpdateTable(game);                         
            }
            else
            {
                playCardsBtn.Enabled = true;
                skipBtn.Enabled = true;
                nextPlayerBtn.Enabled = false;             
                PlayerMove(game);               
            }
            if (game.CheckIfFinished(game._players[curPlayerId]))
            {
                MessageBox.Show("Player " + curPlayerId + " wins!");
                menu.Show();
                this.Close();
            }
        }

        private void skipBtn_Click(object sender, EventArgs e)
        {
            //if (curPlayerId < game._players.Count - 1)
                curPlayerId++;
            //else
            //    curPlayerId = 0;
            nextPlayerBtn.Enabled = true;
        }

        private void playCardsBtn_Click(object sender, EventArgs e)
        {
            game.tableCards = playerMoves[movesComboBox.SelectedIndex];
            for (int i = game._players[curPlayerId].GetHand().Count-1; i > 0; i--)
            {
                if (game._players[curPlayerId].GetHand()[i].GetCardValue() == game.tableCards.First().GetCardValue())
                    game._players[curPlayerId]._hand.RemoveAt(i);
            }
            UpdateTable(game);
            UpdateControls(game);
            if (curPlayerId < game._players.Count - 1)
                curPlayerId++;
            else
                curPlayerId = 0;
            nextPlayerBtn.Enabled = true;
            playCardsBtn.Enabled = false;
            skipBtn.Enabled = false;

        }

        private void BotMove(Game game)
        {
            List<List<Card>> moves = game.GetPossibleMoves(game._players[curPlayerId]);
            moves.Sort((a, b) => (a[0]).GetCardValue().CompareTo((b[0]).GetCardValue()));
            game.tableCards = moves.First();
            for (int i = game._players[curPlayerId].GetHand().Count-1; i > 0; i--)
            {
                if (game._players[curPlayerId].GetHand()[i].GetCardValue() == game.tableCards.First().GetCardValue())
                    game._players[curPlayerId]._hand.RemoveAt(i);
            }
            if (curPlayerId < game._players.Count - 1)
                curPlayerId++;
            else
                curPlayerId = 0;
        }

        private void PlayerMove(Game game)
        {
            List<string> movesStr = new List<string>();
            playerMoves = game.GetPossibleMoves(game._players[curPlayerId]);
            for (int i = 0; i < playerMoves.Count; i++)
            {
                if (playerMoves[i].Count != 0)
                movesStr.Add(Convert.ToString(playerMoves[i].First().GetCardValue()) + "-" +
                    playerMoves[i].Count);
            }
            movesBS.DataSource = movesStr;
            movesComboBox.DataSource = movesBS;
        }

        private void UpdateControls(Game game)
        {
            CreateControls(game._players[0]._hand, true);
            DisplayHand();            
        }

        private void UpdateTable(Game game)
        {           
            CreateControls(game.tableCards, false);
            DisplayTable();
        }

        private void CreateControls(List<Card> cards, bool isHand)
        {
            if (isHand)
                foreach (var picture in handPictures)
                    picture.Dispose();     
            else
                foreach (var picture in tablePictures)                
                    picture.Dispose();
            
            for (int i = 0; i < cards.Count; i++)
            {
                PictureBox picture = new PictureBox();
                picture.Height = 100;
                picture.Width = 60;
                if (isHand)
                {
                    picture.Top = 255;
                    handPictures.Add(SizeImage(cards[i], picture));
                }
                else
                {
                    picture.Top = 50;
                    tablePictures.Add(SizeImage(cards[i], picture));
                }
            }
        }

        private PictureBox SizeImage(Card card, PictureBox picture)
        {
            Image image = Image.FromFile(imgPath + Convert.ToString(card.GetCardValue()) + '_'
                + card.GetCardSuite() + ".png");
            picture.Image = image;
            picture.SizeMode = PictureBoxSizeMode.StretchImage;
            return picture;
        }

        private void DisplayHand()
        {
            for (int i = 0; i < handPictures.Count; i++)
            {
                handPictures[i].Left = i * 20 + 50;
                this.Controls.Add(handPictures[i]);
                //handPictures[i].Refresh();
            }
        }

        private void DisplayTable()
        {         
            for (int i = 0; i < tablePictures.Count; i++)
            {
                tablePictures[i].Left = i * 50 + 400;
                this.Controls.Add(tablePictures[i]);
            }
        }

       
    }
}
