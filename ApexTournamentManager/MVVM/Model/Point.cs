using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApexTournamentManager.MVVM.Model
{
    class Point
    {
        public int atLeast;
        public int value;

        public Point(int AtLeast, int Value)
        {
            atLeast = AtLeast;
            value = Value;
        }
    }
}
