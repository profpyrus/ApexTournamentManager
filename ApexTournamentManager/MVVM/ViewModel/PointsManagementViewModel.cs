using ApexTournamentManager.Core;
using ApexTournamentManager.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
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
            ResetKillPoints = new RelayCommand(o => { _session.DefaultKillPoints(); UpdateKillPoints(); });
            RemoveKillPoint = new RelayCommand(o => { _session.killPoints.Remove(_selectedKillPoint.Point); UpdateKillPoints(); });

            AddPlacementPoint = new RelayCommand(o => {
                int newAtLeast = 1;
                if (_session.placementPoints.Count != 0)
                {
                    newAtLeast = _session.placementPoints.Last().atLeast + 1;
                }
                _session.placementPoints.Add(new Point(newAtLeast, 0));
                UpdatePlacementPoints();
            });
            ResetPlacementPoints = new RelayCommand(o => { _session.DefaultPlacementPoints(); UpdatePlacementPoints(); });
            RemovePlacementPoint = new RelayCommand(o => { _session.placementPoints.Remove(_selectedPlacementPoint.Point); UpdatePlacementPoints(); });

            UpdateKillPoints();
            UpdatePlacementPoints();

        }

        private void UpdateKillPoints(object sender = null, EventArgs e = null)
        {
            _killPoints.Clear();
            List<Point> newPoints = new List<Point>(_session.killPoints);
            newPoints.OrderBy(o => o.atLeast);
            foreach (Point point in newPoints)
            {
                PointsViewModel pointVM = new PointsViewModel(point);
                _killPoints.Add(pointVM);
                if (sender == null)
                    pointVM.resortEvent += UpdateKillPoints;
            }

        }

        private void UpdatePlacementPoints(object sender = null, EventArgs e = null)
        {
            _placementPoints.Clear();
            List<Point> newPoints = new List<Point>(_session.placementPoints);
            newPoints = newPoints.OrderBy(o => o.atLeast).ToList();
            foreach (Point point in newPoints)
            {
                PointsViewModel pointVM = new PointsViewModel(point);
                _placementPoints.Add(pointVM);
                if (sender == null)
                    pointVM.resortEvent += UpdatePlacementPoints;
            }
        }
    }
}
