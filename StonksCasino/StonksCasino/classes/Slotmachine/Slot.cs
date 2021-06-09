using StonksCasino.classes.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StonksCasino.classes.Slotmachine
{
    public class Slot : PropertyChange
    {
		public string ImageURL
		{
			get { return $"/Img/Slotmachine/{Name}.png"; }
		}

        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged("ImageURL"); }
        }



    }
}
