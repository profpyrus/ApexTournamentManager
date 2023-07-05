using ApexTournamentManager.Core;
using ApexTournamentManager.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ApexTournamentManager.MVVM.ViewModel
{
    class PointsManagementViewModel : ObservableObject
    {
        private readonly Session _session;

        public RelayCommand AddKillPoint { get; set; }
        public RelayCommand ResetKillPoints { get; set; }
        public RelayCommand RemoveKillPoint { get; set; }
        public RelayCommand AddPlacementPoint { get; set; }
        public RelayCommand ResetPlacementPoints { get; set; }
        public RelayCommand RemovePlacementPoint { get; set; }

        private ObservableCollection<PointsViewModel> _placementPoints;
        private ObservableCollection<PointsViewModel> _killPoints;

        public IEnumerable<PointsViewModel> PlacementPoints { get { return _placementPoints; } }
        public IEnumerable<PointsViewModel> KillPoints { get { return _killPoints; } }

        public int KillPointCount { get { return _killPoints.Count; } }
        public int PlacementPointCount { get { return _placementPoints.Count; } }

        private PointsViewModel _selectedKillPoint;
        private PointsViewModel _selectedPlacementPoint;

        public PointsViewModel SelectedKillPoint
        {
            get
            {
                return _selectedKillPoint;
            }
            set
            {
                if (value != null)
                    _selectedKillPoint = value;
            }
        }
        public PointsViewModel SelectedPlacementPoint
        {
            get
            {
                return _selectedPlacementPoint;
            }
            set
            {
                if (value != null)
                    _selectedPlacementPoint = value;
            }
        }

        public PointsManagementViewModel(Session session)
        {
            _session = session;
            _killPoints = new ObservableCollection<PointsViewModel>();
            _placementPoints = new ObservableCollection<PointsViewModel>();

            AddKillPoint = new RelayCommand(o => {
                int newAtLeast = 1;
                if (_session.killPoints.Count != 0)
                {
                    newAtLeast = _session.killPoints.Last().atLeast + 1;
                }
                _session.killPoints.Add(new Point(newAtLeast, 0));
                UpdateKillPoints();
            });
            ResetKillPoints = new RelayCommand(o => { _session.DefaultKillPoints(); UpdateKillPoints(0); });
            RemoveKillPoint = new RelayCommand(o => { _session.killPoints.Remove(_selectedKillPoint.Point); UpdateKillPoints(_killPoints.IndexOf(_selectedKillPoint)); });

            AddPlacementPoint = new RelayCommand(o => {
                int newAtLeast = 1;
                if (_session.placementPoints.Count != 0)
                {
                    newAtLeast = _session.placementPoints.Last().atLeast + 1;
                }
                _session.placementPoints.Add(new Point(newAtLeast, 0));
                UpdatePlacementPoints();
            });
            ResetPlacementPoints = new RelayCommand(o => { _session.DefaultPlacementPoints(); UpdatePlacementPoints(0); });
            RemovePlacementPoint = new RelayCommand(o => { _session.placementPoints.Remove(_selectedPlacementPoint.Point); UpdatePlacementPoints(_placementPoints.IndexOf(_selectedPlacementPoint)); });

            UpdateKillPoints(0);
            UpdatePlacementPoints(0);

        }

        private void UpdateKillPoints()
        {
            _killPoints.Clear();
            List<Point> newPoints = new List<Point>(_session.killPoints);
			newPoints = newPoints.OrderBy(o => o.atLeast).ToList();
			foreach (Point point in newPoints)
            {
                PointsViewModel pointVM = new PointsViewModel(point);
                _killPoints.Add(pointVM);
                pointVM.resortEvent += UpdateKillPoints;
			}
			_selectedKillPoint = _killPoints.Last();
			OnPropertyChanged(nameof(SelectedKillPoint));
			OnPropertyChanged(nameof(KillPointCount));
		}

		private void UpdateKillPoints(object sender, EventArgs e)
		{
			_killPoints.Clear();
			List<Point> newPoints = new List<Point>(_session.killPoints);
			newPoints = newPoints.OrderBy(o => o.atLeast).ToList();
			int ind = newPoints.IndexOf((sender as PointsViewModel).Point);
			foreach (Point point in newPoints)
			{
				PointsViewModel pointVM = new PointsViewModel(point);
				_killPoints.Add(pointVM);
				pointVM.resortEvent += UpdateKillPoints;
			}
			_selectedKillPoint = _killPoints.ElementAtOrLast(ind);
			OnPropertyChanged(nameof(SelectedKillPoint));
			OnPropertyChanged(nameof(KillPointCount));
		}

		private void UpdateKillPoints(int index)
		{
			_killPoints.Clear();
			List<Point> newPoints = new List<Point>(_session.killPoints);
			newPoints = newPoints.OrderBy(o => o.atLeast).ToList();
			foreach (Point point in newPoints)
			{
				PointsViewModel pointVM = new PointsViewModel(point);
				_killPoints.Add(pointVM);
				pointVM.resortEvent += UpdateKillPoints;
			}
			_selectedKillPoint = _killPoints.ElementAtOrLast(index);
			OnPropertyChanged(nameof(SelectedKillPoint));
			OnPropertyChanged(nameof(KillPointCount));
		}

		private void UpdatePlacementPoints()
		{
			_placementPoints.Clear();
			List<Point> newPoints = new List<Point>(_session.placementPoints);
			newPoints = newPoints.OrderBy(o => o.atLeast).ToList();
			foreach (Point point in newPoints)
			{
				PointsViewModel pointVM = new PointsViewModel(point);
				_placementPoints.Add(pointVM);
				pointVM.resortEvent += UpdatePlacementPoints;
			}
			_selectedPlacementPoint = _placementPoints.Last();
			OnPropertyChanged(nameof(SelectedPlacementPoint));
			OnPropertyChanged(nameof(PlacementPointCount));
		}

		private void UpdatePlacementPoints(object sender, EventArgs e)
		{
			_placementPoints.Clear();
			List<Point> newPoints = new List<Point>(_session.placementPoints);
			newPoints = newPoints.OrderBy(o => o.atLeast).ToList();
			int ind = newPoints.IndexOf((sender as PointsViewModel).Point);
			foreach (Point point in newPoints)
			{
				PointsViewModel pointVM = new PointsViewModel(point);
				_placementPoints.Add(pointVM);
				pointVM.resortEvent += UpdatePlacementPoints;
			}
			_selectedPlacementPoint = _placementPoints.ElementAtOrLast(ind);
			OnPropertyChanged(nameof(SelectedPlacementPoint));
			OnPropertyChanged(nameof(PlacementPointCount));
		}

		private void UpdatePlacementPoints(int index)
		{
			_placementPoints.Clear();
			List<Point> newPoints = new List<Point>(_session.placementPoints);
			newPoints = newPoints.OrderBy(o => o.atLeast).ToList();
			foreach (Point point in newPoints)
			{
				PointsViewModel pointVM = new PointsViewModel(point);
				_placementPoints.Add(pointVM);
				pointVM.resortEvent += UpdatePlacementPoints;
			}
			_selectedPlacementPoint = _placementPoints.ElementAtOrLast(index);
			OnPropertyChanged(nameof(SelectedPlacementPoint));
			OnPropertyChanged(nameof(PlacementPointCount));
		}
	}
}
