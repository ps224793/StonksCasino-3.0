using StonksCasino.classes.blackjack;
using StonksCasino.classes.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StonksCasino.classes.HorseRace
{
    public class HorseGame : PropertyChange
    {
        private int _horsebet;

        public int MyHorseBet
        {
            get { return _horsebet; }
            set { _horsebet = value; OnPropertyChanged(); }
        }

        private bool _horseone;

        public bool MyHorseone
        {
            get { return _horseone; }
            set { _horseone = value; OnPropertyChanged(); }
        }

        private bool _horsetwo;

        public bool MyHorsetwo
        {
            get { return _horsetwo; }
            set { _horsetwo = value; OnPropertyChanged(); }
        }

        private bool _horsethree;

        public bool MyHorsethree
        {
            get { return _horsethree; }
            set { _horsethree = value; OnPropertyChanged(); }
        }

        private bool _horsefour;

        public bool MyHorsefour
        {
            get { return _horsefour; }
            set { _horsefour = value; OnPropertyChanged(); }
        }

        private bool _bethorse;

        public bool MyBetHorse
        {
            get { return _bethorse; }
            set { _bethorse = value; OnPropertyChanged(); }
        }

        public void HorseBet()
        {
            //MessageBox.Show($"{ MyHorseBet}");
            MyHorseone = false;
            MyHorsetwo = false;
            MyHorsethree = false;
            MyHorsefour = false;
            MyBetHorse = false;
        }

        public void ButtonsHorse()
        {
            MyHorseone = true;
            MyHorsetwo = true;
            MyHorsethree = true;
            MyHorsefour = true;
            MyBetHorse = true;
        }
    }
}
