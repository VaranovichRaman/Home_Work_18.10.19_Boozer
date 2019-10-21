using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boozer.Model
{
    public class Card
    {
        public Suits CardSuit { get; set; }
        public Values CardValue { get; set; }
        public string Mark => MArk(CardSuit); 
        public string MArk(Suits cardSuit)
        {
            switch (cardSuit)
            {
                case Suits.Diamonds:
                    return "♦";
                case Suits.Hearts:
                    return "♥";
                case Suits.Clubs:
                    return "♠";
                case Suits.Spades:
                    return "♣";
            }
            return "";
        }
    }
}


