using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StonksCasino.classes.Roulette
{
    public class Bettingtable
    {
        private ObservableCollection<Bet> _bets = new ObservableCollection<Bet>();

        public ObservableCollection<Bet> Bets
        {
            get { return _bets; }
            set { _bets = value; }
        }

        public Bettingtable()
        {
            _bets.Add(new Bet(new int[] { 1 }));
            _bets.Add(new Bet(new int[] { 1, 2 }, false, 17));
            _bets.Add(new Bet(new int[] { 2 }));
            _bets.Add(new Bet(new int[] { 2, 3 }, false, 17));
            _bets.Add(new Bet(new int[] { 3 }));

            _bets.Add(new Bet(new int[] { 1, 4 }, false, 17));
            _bets.Add(new Bet(new int[] { 1, 2, 4, 5 }, false, 8));
            _bets.Add(new Bet(new int[] { 2, 5 }, false, 17));
            _bets.Add(new Bet(new int[] { 2, 3, 5, 6 }, false, 8));
            _bets.Add(new Bet(new int[] { 3, 6 }, false, 17));

            _bets.Add(new Bet(new int[] { 4 }));
            _bets.Add(new Bet(new int[] { 4, 5 }, false, 17));
            _bets.Add(new Bet(new int[] { 5 }));
            _bets.Add(new Bet(new int[] { 5, 6 }, false, 17));
            _bets.Add(new Bet(new int[] { 6 }));

            _bets.Add(new Bet(new int[] { 4, 7 }, false, 17));
            _bets.Add(new Bet(new int[] { 4, 5, 7, 8 }, false, 8));
            _bets.Add(new Bet(new int[] { 5, 8 }, false, 17));
            _bets.Add(new Bet(new int[] { 5, 6, 8, 9 }, false, 8));
            _bets.Add(new Bet(new int[] { 3, 6 }, false, 17));

            _bets.Add(new Bet(new int[] { 7 }));
            _bets.Add(new Bet(new int[] { 7, 8 }, false, 17));
            _bets.Add(new Bet(new int[] { 8 }));
            _bets.Add(new Bet(new int[] { 8, 9 }, false, 17));
            _bets.Add(new Bet(new int[] { 9 }));

            _bets.Add(new Bet(new int[] { 7, 10 }, false, 17));
            _bets.Add(new Bet(new int[] { 7, 8, 10, 11 }, false, 8));
            _bets.Add(new Bet(new int[] { 8, 11 }, false, 18));
            _bets.Add(new Bet(new int[] { 8, 9, 11, 12 }, false, 8));
            _bets.Add(new Bet(new int[] { 9, 12 }, false, 17));

            _bets.Add(new Bet(new int[] { 10 }));
            _bets.Add(new Bet(new int[] { 10, 11 }, false, 17));
            _bets.Add(new Bet(new int[] { 11 }));
            _bets.Add(new Bet(new int[] { 11, 12 }, false, 17));
            _bets.Add(new Bet(new int[] { 12 }));

            _bets.Add(new Bet(new int[] { 10, 13 }, false, 17));
            _bets.Add(new Bet(new int[] { 10, 11, 13, 14 }, false, 8));
            _bets.Add(new Bet(new int[] { 11, 14 }, false, 17));
            _bets.Add(new Bet(new int[] { 11, 12, 14, 15 }, false, 8));
            _bets.Add(new Bet(new int[] { 12, 15 }, false, 17));

            _bets.Add(new Bet(new int[] { 13 }));
            _bets.Add(new Bet(new int[] { 13, 14 }, false, 17));
            _bets.Add(new Bet(new int[] { 14 }));
            _bets.Add(new Bet(new int[] { 14, 15 }, false, 17));
            _bets.Add(new Bet(new int[] { 15 }));

            _bets.Add(new Bet(new int[] { 13, 16 }, false, 17));
            _bets.Add(new Bet(new int[] { 13, 14, 16, 17 }, false, 8));
            _bets.Add(new Bet(new int[] { 14, 17 }, false, 17));
            _bets.Add(new Bet(new int[] { 14, 15, 17, 18 }, false, 8));
            _bets.Add(new Bet(new int[] { 15, 18 }, false, 17));

            _bets.Add(new Bet(new int[] { 16 }));
            _bets.Add(new Bet(new int[] { 16, 18 }, false, 17));
            _bets.Add(new Bet(new int[] { 17 }));
            _bets.Add(new Bet(new int[] { 17, 18 }, false, 17));
            _bets.Add(new Bet(new int[] { 18 }));

            _bets.Add(new Bet(new int[] { 16, 19 }, false, 17));
            _bets.Add(new Bet(new int[] { 16, 17, 19, 20 }, false, 8));
            _bets.Add(new Bet(new int[] { 17, 20 }, false, 17));
            _bets.Add(new Bet(new int[] { 17, 18, 20, 21 }, false, 8));
            _bets.Add(new Bet(new int[] { 18, 21 }, false, 17));

            _bets.Add(new Bet(new int[] { 19 }));
            _bets.Add(new Bet(new int[] { 19, 20 }, false, 17));
            _bets.Add(new Bet(new int[] { 20 }));
            _bets.Add(new Bet(new int[] { 20, 21  }, false, 17));
            _bets.Add(new Bet(new int[] { 21 }));

            _bets.Add(new Bet(new int[] { 19, 22 }, false, 17));
            _bets.Add(new Bet(new int[] { 19, 20, 22, 23 }, false, 8));
            _bets.Add(new Bet(new int[] { 20, 23 }, false, 17));
            _bets.Add(new Bet(new int[] { 20, 21, 23, 24 }, false, 8));
            _bets.Add(new Bet(new int[] { 21, 24 }, false, 17));

            _bets.Add(new Bet(new int[] { 22 }));
            _bets.Add(new Bet(new int[] { 22, 23 }, false, 17));
            _bets.Add(new Bet(new int[] { 23 }));
            _bets.Add(new Bet(new int[] { 23, 24 }, false, 17));
            _bets.Add(new Bet(new int[] { 24 }));

            _bets.Add(new Bet(new int[] { 22, 25 }, false, 17));
            _bets.Add(new Bet(new int[] { 22, 23, 25, 26 }, false, 8));
            _bets.Add(new Bet(new int[] { 23, 26 }, false, 17));
            _bets.Add(new Bet(new int[] { 23, 24, 26, 27 }, false, 8));
            _bets.Add(new Bet(new int[] { 24, 27 }, false, 17));

            _bets.Add(new Bet(new int[] { 25 }));
            _bets.Add(new Bet(new int[] { 25, 26 }, false, 17));
            _bets.Add(new Bet(new int[] { 26 }));
            _bets.Add(new Bet(new int[] { 26, 27 }, false, 17));
            _bets.Add(new Bet(new int[] { 27 }));

            _bets.Add(new Bet(new int[] { 25, 28 }, false, 17));
            _bets.Add(new Bet(new int[] { 25, 26, 28, 29 }, false, 8));
            _bets.Add(new Bet(new int[] { 26, 29 }, false, 17));
            _bets.Add(new Bet(new int[] { 26, 27, 29, 30 }, false, 8));
            _bets.Add(new Bet(new int[] { 27, 30 }, false, 17));

            _bets.Add(new Bet(new int[] { 28 }));
            _bets.Add(new Bet(new int[] { 28, 29 }, false, 17));
            _bets.Add(new Bet(new int[] { 29 }));
            _bets.Add(new Bet(new int[] { 29, 30 }, false, 17));
            _bets.Add(new Bet(new int[] { 30 }));

            _bets.Add(new Bet(new int[] { 28, 31 }, false, 17));
            _bets.Add(new Bet(new int[] { 28, 29, 31, 32 }, false, 8));
            _bets.Add(new Bet(new int[] { 29, 32 }, false, 17));
            _bets.Add(new Bet(new int[] { 29, 30, 32, 33 }, false, 8));
            _bets.Add(new Bet(new int[] { 30, 32 }, false, 17));

            _bets.Add(new Bet(new int[] { 31 }));
            _bets.Add(new Bet(new int[] { 31, 32 }, false, 17));
            _bets.Add(new Bet(new int[] { 32 }));
            _bets.Add(new Bet(new int[] { 32, 33 }, false, 17));
            _bets.Add(new Bet(new int[] { 33 }));

            _bets.Add(new Bet(new int[] { 31, 34 }, false, 17));
            _bets.Add(new Bet(new int[] { 31, 32, 34, 35 }, false, 8));
            _bets.Add(new Bet(new int[] { 32, 35 }, false, 17));
            _bets.Add(new Bet(new int[] { 32, 33, 35, 36 }, false, 8));
            _bets.Add(new Bet(new int[] { 33, 36 }, false, 18));

            _bets.Add(new Bet(new int[] { 34 }));
            _bets.Add(new Bet(new int[] { 34, 35 }, false, 17));
            _bets.Add(new Bet(new int[] { 35 }));
            _bets.Add(new Bet(new int[] { 35, 36 }, false, 17));
            _bets.Add(new Bet(new int[] { 36 }));

            _bets.Add(new Bet(new int[] { 0 }));

            _bets.Add(new Bet(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 }, false, 3));
            _bets.Add(new Bet(new int[] { 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24 }, false, 3));
            _bets.Add(new Bet(new int[] { 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36 }, false, 3));

            _bets.Add(new Bet(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18 }, false, 2));
           
            _bets.Add(new Bet(new int[] { 2, 4, 6, 8, 10, 12, 14, 16, 18, 20, 22, 24, 26, 28, 30, 32, 34, 36 }, false, 2));
 
            _bets.Add(new Bet(new int[] { 2, 4, 6, 8, 10, 11, 13, 15, 17, 20, 22, 24, 26, 28, 29, 31, 33, 35 }, false, 2));
            _bets.Add(new Bet(new int[] { 1, 3, 5, 7, 9, 12, 14, 16, 18, 19, 21, 23, 25, 27, 30, 32, 34, 36 }, false, 2));

            _bets.Add(new Bet(new int[] { 1, 3, 5, 7, 9, 11, 13, 15, 17, 19, 21, 23, 25, 27, 29, 31, 33, 35 }, false, 2));
            _bets.Add(new Bet(new int[] { 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36 }, false, 2));

            _bets.Add(new Bet(new int[] { 3, 6, 9, 12, 15, 18, 21, 24, 27, 30, 33, 36 }, false, 3));
            _bets.Add(new Bet(new int[] { 2, 5, 8, 11, 14, 17, 20, 23, 26, 29, 32, 35 }, false, 3));
            _bets.Add(new Bet(new int[] { 1, 4, 7, 10, 13, 16, 19, 22, 25, 28, 31, 34 }, false, 3));




        }

        public int Checkwin(int finalnumber)
        {
            int totalwin = 0;
            foreach (Bet bet in _bets)
            {
                totalwin += bet.Checkwin(finalnumber);
            
            }
            return totalwin;
        }
        public void Resetbet()
        {
            foreach (Bet bet in _bets)
            {
               bet.ResetWinningBet();
            }
        }
    }

}
