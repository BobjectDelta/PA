using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleDaifugo;

namespace ConsoleDaifugo
{
    public class Deck
    {
        public List<Card> cards;
        public Deck()
        {
            cards = new List<Card>();

            foreach (Card.Suit suit in Enum.GetValues(typeof(Card.Suit)))
            {
                foreach (Card.Value value in Enum.GetValues(typeof(Card.Value)))
                    cards.Add(new Card((int)value, suit));                                                                                                                      
            }
        }

        public void Shuffle()
        {
            Random rand = new Random();
            List<Card> deck = cards;

            for (int n = deck.Count - 1; n > 0; --n)
            {
                int k = rand.Next(n + 1);
                Card temp = deck[n];
                deck[n] = deck[k];
                deck[k] = temp;
            }
        }
    }
}
