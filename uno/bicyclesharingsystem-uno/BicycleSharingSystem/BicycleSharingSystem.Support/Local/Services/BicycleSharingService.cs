using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using BicycleSharingSystem.Support.Local.Models;

namespace BicycleSharingSystem.Support.Local.Services;

public class BicycleSharingService : IBicycleSharingService
{
    private readonly HttpClient _httpClient;
    private const string BaseUrl = "https://localhost:7178"; // API의 기본 URL을 여기에 설정하세요

    public BicycleSharingService()
    {
        _httpClient = new HttpClient
        {
            BaseAddress = new Uri(BaseUrl)
        };
    }

    public async Task<BicycleModel?> GetBicycleAsync(Guid id)
    {
        return await _httpClient.GetFromJsonAsync<BicycleModel>($"/Bicycle/{id}");
    }

    public async Task<int> AddBicyclesAsync(IEnumerable<BicycleModel> bicycles)
    {
        var response = await _httpClient.PostAsJsonAsync("/Bicycle", bicycles);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<int>();
    }

    public async Task<bool> UpdateBicycleAsync(Guid id, BicycleModel bicycle)
    {
        var response = await _httpClient.PutAsJsonAsync($"/Bicycle/{id}", bicycle);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteBicycleAsync(Guid id)
    {
        var response = await _httpClient.DeleteAsync($"/Bicycle/{id}");
        return response.IsSuccessStatusCode;
    }
}
