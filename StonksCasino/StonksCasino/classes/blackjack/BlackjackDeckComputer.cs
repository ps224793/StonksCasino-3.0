using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StonksCasino.classes.Main;
using StonksCasino.enums.card;

namespace StonksCasino.classes.blackjack
{
    class BlackjackDeckComputer : CardDeckBlack
    {
        public BlackjackDeckComputer()
        {
            AssembleDeck(6);
        }
    }
}
