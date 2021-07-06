using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using StonksCasino.enums.card;

namespace StonksCasino.classes.Main
{
    public class CardBlackjack : INotifyPropertyChanged
    {
		public event PropertyChangedEventHandler PropertyChanged;
		protected void OnPropertyChanged([CallerMemberName] string name = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
		}

		private CardType _type;

		public CardType Type
		{
			get { return _type; }
			set { _type = value; OnPropertyChanged("ImageURL"); OnPropertyChanged("ActiveURL"); }
		}

		private BlackcardValue _value;

		public BlackcardValue Value
		{
			get { return _value; }
			set { _value = value; OnPropertyChanged("ImageURL"); OnPropertyChanged("ActiveURL"); }
		}

		public string SelectedCardskin
		{
			get { return User.SelectedCardskin; }
		}

		private bool _turned;

		public bool Turned
		{
			get { return _turned; }
			set { _turned = value; OnPropertyChanged(); OnPropertyChanged("ActiveURL"); }
		}

		public string ActiveURL
		{
			get
			{
				if (Turned == true)
				{
					return BackURL;
				}
				else
				{
					return ImageURL;
				}
			}
		}

		private CardBackColor _backColor;

		public CardBackColor BackColor
		{
			get { return _backColor; }
			set { _backColor = value; OnPropertyChanged("BackURL"); OnPropertyChanged("ActiveURL"); }
		}

		public string ImageURL
		{
			get { return $"/Img/Cards/{SelectedCardskin}/{Value}{Type}.png"; }
		}

		public string BackURL
		{
			get
			{
				if (BackColor == CardBackColor.Blue)
				{
					return $"/Img/Cards/{SelectedCardskin}/BackBlue.png";
				}
				else
				{
					return $"/Img/Cards/{SelectedCardskin}/BackRed.png";
				}
			}
		}

		public CardBlackjack(CardType cardType, BlackcardValue cardValue, CardBackColor cardBackColor)
		{
			Type = cardType;
			Value = cardValue;
			BackColor = cardBackColor;
			Turned = false;
			OnPropertyChanged("ImageURL");
		}

		public CardBlackjack(CardType cardType, BlackcardValue cardValue, CardBackColor cardBackColor, bool turned)
		{
			Type = cardType;
			Value = cardValue;
			BackColor = cardBackColor;
			Turned = turned;
			OnPropertyChanged("ImageURL");
		}
	}
}
