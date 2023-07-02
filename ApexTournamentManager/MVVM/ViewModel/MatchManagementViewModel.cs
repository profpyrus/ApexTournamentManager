using ApexTournamentManager.Core;
using ApexTournamentManager.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApexTournamentManager.MVVM.ViewModel
{
    class MatchManagementViewModel : ObservableObject
    {
        private Session _session;

        private readonly ObservableCollection<MatchViewModel> _matches;
        public IEnumerable<MatchViewModel> Matches => _matches;

        private MatchViewModel _selectedMatch;
        public MatchViewModel SelectedMatch
        {
            get
            {
                return _selectedMatch;
            }
            set
            {
                _selectedMatch = value;
            }
        }
        public int MatchCount { get { return _matches.Count; } }

        public RelayCommand AddMatchCommand { get; set; }
        public RelayCommand RemoveMatchCommand { get; set; }

        public MatchManagementViewModel(Session session)
        {
            _matches = new ObservableCollection<MatchViewModel>();
            _session = session;

            AddMatchCommand = new RelayCommand(o => { _session.AddMatch(Guid.NewGuid()); UpdateData(); });
            RemoveMatchCommand = new RelayCommand(o => { _session.RemoveMatch(SelectedMatch.Match); UpdateData(_matches.IndexOf(_selectedMatch)); });

            UpdateData(0);
        }

        private void UpdateData()
        {
            _matches.Clear();
            foreach (Match match in _session.matches)
            {
                _matches.Add(new MatchViewModel(match, _session.teams));
            }
            _selectedMatch = _matches.First();
            OnPropertyChanged(nameof(SelectedMatch));
            OnPropertyChanged(nameof(MatchCount));
        }

		private void UpdateData(int index)
		{
			_matches.Clear();
			foreach (Match match in _session.matches)
			{
				_matches.Add(new MatchViewModel(match, _session.teams));
			}
			_selectedMatch = _matches.ElementAtOrLast(index);
			OnPropertyChanged(nameof(SelectedMatch));
			OnPropertyChanged(nameof(MatchCount));
		}
	}
}
