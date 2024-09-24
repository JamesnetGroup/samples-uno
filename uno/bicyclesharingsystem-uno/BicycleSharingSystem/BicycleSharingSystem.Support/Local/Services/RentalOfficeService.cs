using System.Net.Http.Json;
using BicycleSharingSystem.Support.Local.Models;

namespace BicycleSharingSystem.Support.Local.Services
{

    public class RentalOfficeService : IRentalOfficeService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://localhost:7178";

        public RentalOfficeService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(BaseUrl)
            };
        }

        public async Task<IEnumerable<RentalOfficeModel>> GetAllRentalOfficesAsync()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<RentalOfficeModel>>("/RentalOffice") ?? Enumerable.Empty<RentalOfficeModel>();
        }

        public async Task<RentalOfficeModel> GetRentalOfficeAsync(string name)
        {
            return await _httpClient.GetFromJsonAsync<RentalOfficeModel>($"/RentalOffice/{name}");
        }


        public async Task<int> AddRentalOfficesAsync(IEnumerable<RentalOfficeModel> rentalOffices)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("/RentalOffice", rentalOffices);

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error adding rental offices. Status: {response.StatusCode}, Content: {errorContent}");
                    return -1;
                }

                var content = await response.Content.ReadAsStringAsync();
                if (int.TryParse(content, out int result))
                {
                    return result;
                }
                Console.WriteLine($"Failed to parse response content: {content}");
                return -1;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"HTTP request failed: {ex.Message}");
                return -1;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
                return -1;
            }
        }

        public async Task<bool> UpdateRentalOfficeAsync(RentalOfficeModel rentalOffice)
        {
            var response = await _httpClient.PutAsJsonAsync($"/RentalOffice/{rentalOffice.OfficeId}", rentalOffice);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteRentalOfficeAsync(string name)
        {
            var response = await _httpClient.DeleteAsync($"/RentalOffice/{name}");
            return response.IsSuccessStatusCode;
        }
    }
}
