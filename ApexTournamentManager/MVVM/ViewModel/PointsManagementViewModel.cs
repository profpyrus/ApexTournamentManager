using ApexTournamentManager.Core;
using ApexTournamentManager.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApexTournamentManager.MVVM.ViewModel
{
    class PointsManagementViewModel : ObservableObject
    {
        private readonly Session _session;

        public PointsManagementViewModel(Session session)
        {
            _session = session;
        }
    }
}
