using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using BicycleSharingSystem.Support.Local.Models;
using BicycleSharingSystem.Support.Local.Services;
using Jamesnet.Core;
using System.Linq;

namespace BicycleSharingSystem.Bicycle.Local.ViewModels
{
    public class BicycleContentViewModel : ViewModelBase
    {
        private readonly IBicycleSharingService _bicycleService;
        private readonly IRentalOfficeService _rentalOfficeService;

        public ObservableCollection<RentalOfficeModel> RentalOffices { get; private set; }
        public ObservableCollection<BicycleModel> Bicycles { get; private set; }

        private RentalOfficeModel _selectedRentalOffice;
        public RentalOfficeModel SelectedRentalOffice
        {
            get => _selectedRentalOffice;
            set
            {
                if (SetProperty(ref _selectedRentalOffice, value))
                {
                    LoadBicyclesForSelectedOffice();
                    (AddBicycleCommand as RelayCommand)?.RaiseCanExecuteChanged();
                }
            }
        }

        private BicycleModel _selectedBicycle;
        public BicycleModel SelectedBicycle
        {
            get => _selectedBicycle;
            set
            {
                if (SetProperty(ref _selectedBicycle, value))
                {
                    (UpdateBicycleCommand as RelayCommand)?.RaiseCanExecuteChanged();
                    (DeleteBicycleCommand as RelayCommand)?.RaiseCanExecuteChanged();
                }
            }
        }

        public ICommand LoadRentalOfficesCommand { get; }
        public ICommand AddBicycleCommand { get; }
        public ICommand UpdateBicycleCommand { get; }
        public ICommand DeleteBicycleCommand { get; }

        public BicycleContentViewModel(IBicycleSharingService bicycleService, IRentalOfficeService rentalOfficeService)
        {
            _bicycleService = bicycleService;
            _rentalOfficeService = rentalOfficeService;

            RentalOffices = new ObservableCollection<RentalOfficeModel>();
            Bicycles = new ObservableCollection<BicycleModel>();

            LoadRentalOfficesCommand = new RelayCommand(async () => await LoadRentalOfficesAsync());
            AddBicycleCommand = new RelayCommand(async () => await AddBicycleAsync(), () => SelectedRentalOffice != null);
            UpdateBicycleCommand = new RelayCommand(async () => await UpdateBicycleAsync(), () => SelectedBicycle != null);
            DeleteBicycleCommand = new RelayCommand(async () => await DeleteBicycleAsync(), () => SelectedBicycle != null);
        }

        private async Task LoadRentalOfficesAsync()
        {
            var offices = await _rentalOfficeService.GetAllRentalOfficesAsync();
            RentalOffices.Clear();
            foreach (var office in offices)
            {
                RentalOffices.Add(office);
            }
        }

        private async Task LoadBicyclesForSelectedOffice()
        {
            if (SelectedRentalOffice != null)
            {
                var officeDetails = await _rentalOfficeService.GetRentalOfficeAsync(SelectedRentalOffice.Name);
                if (officeDetails is RentalOfficeModel detailedOffice && detailedOffice.Bicycles != null)
                {
                    Bicycles.Clear();
                    foreach (var bicycle in detailedOffice.Bicycles)
                    {
                        Bicycles.Add(bicycle);
                    }
                }
            }
        }

        private async Task AddBicycleAsync()
        {
            if (SelectedRentalOffice != null)
            {
                var newBicycle = new BicycleModel
                {
                    BicycleId = Guid.NewGuid(),
                    Name = $"New Bicycle {DateTime.Now.Ticks}",
                    RentalOfficeId = SelectedRentalOffice.OfficeId,
                    StartRentalTime = DateTime.Now,
                    ExpireRentalTime = DateTime.Now.AddDays(1)
                };

                await _bicycleService.AddBicyclesAsync(new[] { newBicycle });
                await LoadBicyclesForSelectedOffice();
                SelectedBicycle = Bicycles.FirstOrDefault(b => b.BicycleId == newBicycle.BicycleId);
            }
        }

        private async Task UpdateBicycleAsync()
        {
            if (SelectedBicycle != null)
            {
                await _bicycleService.UpdateBicycleAsync(SelectedBicycle.BicycleId, SelectedBicycle);
                await LoadBicyclesForSelectedOffice();
            }
        }

        private async Task DeleteBicycleAsync()
        {
            if (SelectedBicycle != null)
            {
                await _bicycleService.DeleteBicycleAsync(SelectedBicycle.BicycleId);
                await LoadBicyclesForSelectedOffice();
                SelectedBicycle = null;
            }
        }
    }
}
