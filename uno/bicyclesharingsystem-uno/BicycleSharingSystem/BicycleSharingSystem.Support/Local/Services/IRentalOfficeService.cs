using BicycleSharingSystem.Support.Local.Models;

namespace BicycleSharingSystem.Support.Local.Services;


public interface IRentalOfficeService
{
    Task<IEnumerable<RentalOfficeModel>> GetAllRentalOfficesAsync();
    Task<RentalOfficeModel> GetRentalOfficeAsync(string name);
    Task<int> AddRentalOfficesAsync(IEnumerable<RentalOfficeModel> rentalOffices);
    Task<bool> UpdateRentalOfficeAsync(RentalOfficeModel rentalOffice);
    Task<bool> DeleteRentalOfficeAsync(string name);
}
