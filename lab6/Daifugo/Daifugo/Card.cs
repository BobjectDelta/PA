using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ConsoleDaifugo.Card;

namespace ConsoleDaifugo
{
    public class Card : IComparable<Card>
    {
        public enum Suit
        {
            clubs,
            diamonds,
            hearts,
            spades
        }

        public enum Value
        {            
            Three = 3,
            Four = 4,
            Five = 5,
            Six = 6,
            Seven = 7,
            Eight = 8,
            Nine = 9,
            Ten = 10,
            Jack = 11,
            Queen = 12,
            King = 13,
            Ace = 14,
            Two = 15,
        }

        private int _cardValue;
        private Suit _cardSuit;
        public Card(int value, Suit suit)
        {
             _cardValue = value;
             _cardSuit = suit;
        }

        public int GetCardValue()
        {
            return _cardValue;
        }

        public Suit GetCardSuite()
        {
            return _cardSuit;
        }

        public int CompareTo(Card card)
        {
            return (int)this._cardValue.CompareTo(Convert.ToInt32(card._cardValue));
        }

        public override bool Equals(object obj)
        {
            if (obj is Card card)
                return _cardValue == card._cardValue;
            return false;
        }

        public override int GetHashCode()
        {
            return _cardValue.GetHashCode();
        }
    }
}
