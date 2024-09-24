using BicycleSharingSystem.Support.Local.Models;
using BicycleSharingSystem.Support.Local.Services;
using Jamesnet.Core;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace BicycleSharingSystem.Rental.Local.ViewModels
{
    public class RentalContentViewModel : ViewModelBase
    {
        private readonly IRentalOfficeService _rentalOfficeService;
        public ObservableCollection<RentalOfficeModel> Rentals { get; set; }

        private RentalOfficeModel _selectedRental;
        public RentalOfficeModel SelectedRental
        {
            get => _selectedRental;
            set => SetProperty(ref _selectedRental, value);
        }

        public ICommand LoadRentalsCommand { get; }
        public ICommand AddRentalCommand { get; }
        public ICommand UpdateRentalCommand { get; }
        public ICommand DeleteRentalCommand { get; }

        public RentalContentViewModel(IRentalOfficeService rentalOfficeService)
        {
            _rentalOfficeService = rentalOfficeService;
            Rentals = new ObservableCollection<RentalOfficeModel>();
            LoadRentalsCommand = new RelayCommand(LoadRentals);
            AddRentalCommand = new RelayCommand(AddRental);
            UpdateRentalCommand = new RelayCommand(UpdateRental);
            DeleteRentalCommand = new RelayCommand(DeleteRental);
        }

        private async void LoadRentals()
        {
            var rentalOffices = await _rentalOfficeService.GetAllRentalOfficesAsync();
            Rentals.Clear();
            foreach (var office in rentalOffices)
            {
                Rentals.Add(office);
            }
        }

        private async void AddRental()
        {
            var newRentalOffice = new RentalOfficeModel
            {
                OfficeId = Guid.NewGuid(),
                Name = $"New Office {DateTime.Now.Ticks}",
                Region = "Default Region",
                Latitude = 0,
                Longitude = 0
            };

            var result = await _rentalOfficeService.AddRentalOfficesAsync(new List<RentalOfficeModel> { newRentalOffice });
            if (result > 0)
            {
                LoadRentals();
            }
            else
            {
                // 추가 실패 처리
                Console.WriteLine("Failed to add new rental office");
            }
        }

        private async void UpdateRental()
        {
            if (SelectedRental == null) return;

            var result = await _rentalOfficeService.UpdateRentalOfficeAsync(SelectedRental);
            if (result)
            {
                LoadRentals();
            }
            else
            {
                // 업데이트 실패 처리
                Console.WriteLine("Failed to update rental office");
            }
        }

        private async void DeleteRental()
        {
            if (SelectedRental == null) return;

            var result = await _rentalOfficeService.DeleteRentalOfficeAsync(SelectedRental.Name);
            if (result)
            {
                Rentals.Remove(SelectedRental);
                SelectedRental = null;
            }
            else
            {
                // 삭제 실패 처리
                Console.WriteLine("Failed to delete rental office");
            }
        }
    }
}
