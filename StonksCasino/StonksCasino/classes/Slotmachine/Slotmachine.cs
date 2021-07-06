using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using StonksCasino.classes.Main;


namespace StonksCasino.classes.Slotmachine
{
    public class Slotmachine : PropertyChange
    {
        Dictionary<string, int> _symbolValues = new Dictionary<string, int>();
        public List<SlotRow> SlotRows { get; set; } = new List<SlotRow>();





        public Slotmachine()
        {
            SlotRows.Add(new SlotRow());
            SlotRows.Add(new SlotRow());
            SlotRows.Add(new SlotRow());
            SetSymbolValues();
        }

        public void SetSymbolValues()
        {
            _symbolValues.Add("Cherry", 100);
            _symbolValues.Add("Grape", 100);
            _symbolValues.Add("Melon", 100);
            _symbolValues.Add("Orange", 100);
            _symbolValues.Add("Seven", 1500);
            _symbolValues.Add("Star", 300);
            _symbolValues.Add("Diamond", 400);
            _symbolValues.Add("bell", 200);
            _symbolValues.Add("Bar", 600);
        }

        public async Task<bool> Activate()
        {
            SlotRows[0].Activate();
            SlotRows[1].Activate();
            SlotRows[2].Activate();
            for (int i = 0; i < 10; i++)
            {
                SlotRows[0].Activate();
                SlotRows[1].Activate();
                SlotRows[2].Activate();
                await Task.Delay(100);
            }
            for (int i = 0; i < 10; i++)
            {
                SlotRows[1].Activate();
                SlotRows[2].Activate();
                await Task.Delay(100);
            }
            for (int i = 0; i < 10; i++)
            {
                SlotRows[2].Activate();
                await Task.Delay(100);
            }
           return true;
        }

        public int CheckWin()
        {
            int winnings = 0;

            if (
                SlotRows[0].Slots[0].Name == SlotRows[1].Slots[0].Name &&
                SlotRows[0].Slots[0].Name == SlotRows[2].Slots[0].Name
               )
            {
                winnings += _symbolValues[SlotRows[0].Slots[0].Name];
            }
            if (
               SlotRows[0].Slots[1].Name == SlotRows[1].Slots[1].Name &&
               SlotRows[0].Slots[1].Name == SlotRows[2].Slots[1].Name
              )
            {
                winnings += _symbolValues[SlotRows[0].Slots[1].Name];
            }
            if (
               SlotRows[0].Slots[2].Name == SlotRows[1].Slots[2].Name &&
               SlotRows[0].Slots[2].Name == SlotRows[2].Slots[2].Name
              )
            {
                winnings += _symbolValues[SlotRows[0].Slots[2].Name];
            }
            if (
                SlotRows[0].Slots[0].Name == SlotRows[1].Slots[1].Name &&
                SlotRows[0].Slots[0].Name == SlotRows[2].Slots[2].Name
                )
            {
                winnings += _symbolValues[SlotRows[0].Slots[0].Name];
            }
            if (
            SlotRows[0].Slots[2].Name == SlotRows[1].Slots[1].Name &&
            SlotRows[0].Slots[2].Name == SlotRows[2].Slots[0].Name
                )
            {
                winnings += _symbolValues[SlotRows[0].Slots[2].Name];
            }
            if (
                SlotRows[0].Slots[0].Name == SlotRows[0].Slots[1].Name &&
                SlotRows[0].Slots[0].Name == SlotRows[0].Slots[2].Name
                )
            {
                winnings += _symbolValues[SlotRows[0].Slots[0].Name];
            }
            if (
                SlotRows[1].Slots[0].Name == SlotRows[1].Slots[1].Name &&
                SlotRows[1].Slots[0].Name == SlotRows[1].Slots[2].Name
                )
            {
                winnings += _symbolValues[SlotRows[1].Slots[0].Name];
            }
            if (
                SlotRows[2].Slots[0].Name == SlotRows[2].Slots[1].Name &&
                SlotRows[2].Slots[0].Name == SlotRows[2].Slots[2].Name
                )
            {
                winnings += _symbolValues[SlotRows[2].Slots[0].Name];
            }


            return winnings;
        }
    }
}
