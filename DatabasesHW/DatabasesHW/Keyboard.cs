using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabasesHW
{
    internal class Keyboard
    {
        public int Id { get; set; }
        public string Size { get; set; }
        public string SwitchType { get; set; }
        public int SwitchAmount { get; set; }
        public string Lube {  get; set; }

        public Keyboard(string size, string switchType, int switchAmount, string lube)
        {
            Size = size;
            SwitchType = switchType;
            SwitchAmount = switchAmount;
            Lube = lube;
        }
        public override string ToString()
        {
            return $"{Id}\t{Size}\t{SwitchType}\t{SwitchAmount}\t{Lube}";
        }
    }
}
