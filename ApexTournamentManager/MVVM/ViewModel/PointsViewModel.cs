using ApexTournamentManager.Core;
using ApexTournamentManager.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApexTournamentManager.MVVM.ViewModel
{
    class PointsViewModel : ObservableObject
    {
        private readonly Point _point;

        public Point Point { get { return _point; } }

        public string AtLeast
        {
            get
            {
                return _point.atLeast.ToString();
            }
            set
            {
                int newValue;
                if (int.TryParse(value, out newValue)
                    && newValue >= 1
                    && newValue <= 30)
                {
                    _point.atLeast = newValue;
                    resortEvent?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public string Value
        {
            get
            {
                return _point.value.ToString();
            }
            set
            {
                int newValue;
                if (int.TryParse(value, out newValue)
                    && newValue >= 1)
                {
                    _point.value = newValue;
                    OnPropertyChanged(nameof(Value));
                }
            }
        }

        public PointsViewModel(Point point)
        {
            _point = point;
        }

        public event EventHandler resortEvent;
    }
}
