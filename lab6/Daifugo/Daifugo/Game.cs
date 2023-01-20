using Daifugo;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleDaifugo
{
    public class Game
    {
        public List<Player> _players { get; set; }
        public List<Card> tableCards { get; set; }
        public List<Card> discardedCards = new List<Card>();
        public Deck deck { get; set; }

        public Game(int playerAmount)
        {
            _players = CreatePlayers(playerAmount);
            tableCards = new List<Card>();
            deck = new Deck();
            deck.Shuffle();

            int cardsAmount = 52;
            int dealtCards = 0;
            bool isDone = false;
            while (dealtCards < cardsAmount)
            {
                for (int i = 0; i < _players.Count && !isDone; i++)
                {
                    if (dealtCards >= cardsAmount)
                        isDone = true;
                    else
                    {
                        _players[i].GetHand().Add(deck.cards.First());
                        deck.cards.RemoveAt(0);
                        dealtCards++;
                    }
                }
            }
        }
        public bool CheckIfFinished(Player player)
        {
            if (player.GetHand().Count == 0)
                return true;
            return false;
        }

        public List<List<Card>> GetPossibleMoves(Player curPlayer)
        {
            int tmpValue;
            int g = 0;
            List<Card> tmpHand = curPlayer.GetHand();
            List<Card> distinctValues = tmpHand.Distinct().ToList();
            List<List<Card>> possibleMoves = new List<List<Card>>();
            for (int i = 0; i < distinctValues.Count; i++)           
                possibleMoves.Add(new List<Card>());            

            while (g < possibleMoves.Count)
            {
                tmpValue = distinctValues[g].GetCardValue();
                for (int j = 0; j < tmpHand.Count-1; j++)
                {
                    if (tmpHand[j].GetCardValue() == tmpValue)                    
                        possibleMoves[g].Add(tmpHand[j]);                                           
                }
                g++;
            }
            if (tableCards.Count != 0)
            {
                tmpValue = tableCards.First().GetCardValue();
                for (int i = possibleMoves.Count - 1; i >= 0; i--)
                    if (possibleMoves[i].Count == 0)
                        possibleMoves.RemoveAt(i);
                    else
                    if (possibleMoves[i].First().GetCardValue() <= tmpValue ||
                        possibleMoves[i].Count != tableCards.Count)
                        possibleMoves.RemoveAt(i);                
            }

            return possibleMoves;
        }

        public List<Player> CreatePlayers(int playerAmnt)
        {
            List<Player> players = new List<Player>();
            for (int i = 0; i < playerAmnt; i++)
            {
                List<Card> cards = new List<Card>();
                players.Add(new Player(cards));
            }
            return players;
        }
    }

}
