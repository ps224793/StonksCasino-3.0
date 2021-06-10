using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using StonksCasino.classes.Api;
using StonksCasino.classes.Main;

namespace StonksCasino.classes.Roulette
{
   
    public class Bet : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private const string _sender = "Roulette";

        private string _imageUrl = "/Img/Roulette/transparant.png";

        public string ImageUrl
        {
            get { return _imageUrl; }
            set { _imageUrl = value; OnPropertyChanged(); }
        }
        private int _fontsize = 12;

        public int Myfontsize
        {
            get { return _fontsize; }
            set { _fontsize = value; OnPropertyChanged();}
        }

        private double _opacity;

        public double Opacity 
        {
            get { return _opacity; }
            set { _opacity = value; OnPropertyChanged(); }
        }


        private int[] _values;

        public int[] Values
        {
            get { return _values; }
            set { _values = value; }
        }

        private bool _special;

        public bool Special
        {
            get { return _special; }
            set { _special = value; }
        }

        private int _amount = 0;

        public int Amount
        {
            get { return _amount; }
            set { _amount = value; OnPropertyChanged(); }
        }

        private string _amountLabel = "";

        public string AmountLabel
        {
            get { return _amountLabel; }
            set { _amountLabel = value; OnPropertyChanged(); }
        }

        private bool _set;

        public bool Set
        {
            get { return _set; }
            set { _set = value; OnPropertyChanged(); }
        }
        private int _finalnumber;

        public int MyFinalNumber
        {
            get { return _finalnumber; }
            set { _finalnumber = value; }
        }
        private int _totalinzet;

        public int MyTotalinzet
        {
            get { return _totalinzet; }
            set { _totalinzet = value; OnPropertyChanged(); }
        }
        private string _Inzet = "1"; 

        public string MyInzet
        {
            get { return _Inzet; }
            set { _Inzet = value; OnPropertyChanged(); }
        }
        private int _value;

        public int MyValue
        {
            get { return _value; }
            set { _value = value; }
        }




        private int _multiplier = 36;

        public Bet(int[] values = null, bool special = false, int multiplier = 36)
        {
            Values = values;
            _multiplier = multiplier; 
         
        }

        public int Checkwin(int value)
        {
            //Controleer of er ingezet is op deze
            if (Set)
            {
                if (Values.Contains(value))
                {
                    Opacity = 0.5;
                    return Amount * _multiplier;
                   
                }
                else
                {
                    ResetBet();
                }
            }
            else
            {
                ResetBet();
            }

             return 0;
        }
   

   

        public void ResetBet()
        {
            
            Amount = 0;
            AmountLabel = "";
       
            Opacity = 1;
            ImageUrl = "/Img/Roulette/transparant.png";
            Set = false;
        }
        public void ResetWinningBet()
        {
            if (Set)
            {
    
               
                Amount = 0;
                AmountLabel = "";
                MyValue = 0;
                Opacity = 1;
                ImageUrl = "/Img/Roulette/transparant.png";
                Set = false;
            }
           
        }

        public void SetBet(int amount)
        {
          
            if (Set)
            {
                int Current = int.Parse(AmountLabel);
                AmountLabel = (Current + amount).ToString();
                Amount = Current + amount;
            }
            else
            {
                AmountLabel = amount.ToString();
                Amount = amount;
               
                Opacity = 1;
                ImageUrl = "/Img/Roulette/Token.png";
                Set = true;
                if (Amount < 100)
                {
                    Myfontsize = 12;
                }
                if (Amount >= 100)
                {
                    Myfontsize = 10;
                }
                if (Amount >= 1000)
                {
                    Myfontsize = 6;
                }

            }
        }
        public void PreviewBet()
        {
            if (Set != true)
            {
                ImageUrl = "/Img/Roulette/Token.png";
                Opacity = 0.3;
            }
        }
        public void dePreviewBet()
        {
            if (Set != true)
            {
                ImageUrl = "/Img/Roulette/transparant.png";
              
            }
          

        }
        public void DeleteBet()
        {
            if (Set == true)
            {


                ImageUrl = "/Img/Roulette/transparant.png";
                AmountLabel = "";
                Amount = 0;
                Set = false;

            }


        }
        public void Addtotal(int amount)
        {
            MyTotalinzet += amount;
        }
        public void RemoveTotal(int amount)
        {
            MyTotalinzet -= amount;
        }
        public void Plusinzet()
        {
            int inzet = int.Parse(MyInzet);
            inzet += 1;
            MyInzet = inzet.ToString();
        }
        public void Mininzet()
        {
            int inzet = int.Parse(MyInzet);
            inzet -= 1;
            MyInzet = inzet.ToString();
        }



    }
}
