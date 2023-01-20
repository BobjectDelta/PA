using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleDaifugo;

namespace ConsoleDaifugo
{
    public class Player
    {
        public List<Card> _hand;
        
        public Player(List<Card> cardHand)
        {
            _hand = cardHand;
        }

        public Player()
        { }

        public List<Card> GetHand()
        {
            return _hand;
        }

        public void SetHand(List<Card> newHand)
        {
            _hand = newHand;
        }
    }
}
