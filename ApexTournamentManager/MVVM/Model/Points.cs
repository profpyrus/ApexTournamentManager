using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApexTournamentManager.MVVM.Model
{
    class Points
    {
        public int atLeast;
        public int value;

        public Points(int AtLeast, int Value)
        {
            atLeast = AtLeast;
            value = Value;
        }
    }
}
