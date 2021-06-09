using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace StonksCasino.classes.Slotmachine
{
    public class SlotRow
    {
        public List<Slot> Slots { get; set; } = new List<Slot>();

        Random _random = new Random();

        string[] _slotNames = new string[] {"Cherry", "Grape", "Melon", "Orange", "Cherry", "Grape", "Melon", "Orange", "Cherry", "Grape", "Melon", "Orange", "Cherry", "Grape", "Melon", "Orange", "Seven", "Star", "Star", "Diamond", "bell", "bell", "bell", "Bar" }; 


        public SlotRow()
        {
            Slots.Add(new Slot() { Name = "Cherry" }) ;
            Slots.Add(new Slot() { Name = "Cherry" });
            Slots.Add(new Slot() { Name = "Cherry" });

        }


        public void Activate()
        {
            Slots[2].Name = Slots[1].Name;
            Slots[1].Name = Slots[0].Name;
            Slots[0].Name = _slotNames[_random.Next(_slotNames.Length)];
        }


    }


}
