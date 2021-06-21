using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StonksCasino.classes.poker
{
    public class PokerPot
    {
        private List<PokerPlayer> _eligablePlayers;

        public List<PokerPlayer> EligablePlayers
        {
            get { return _eligablePlayers; }
            set { _eligablePlayers = value; }
        }

        private int _chips;

        public int Chips
        {
            get { return _chips; }
            set { _chips = value; }
        }

        public PokerPot(List<PokerPlayer> eligablePlayers, int chips)
        {
            EligablePlayers = eligablePlayers;
            Chips = chips;
        }
    }
}
