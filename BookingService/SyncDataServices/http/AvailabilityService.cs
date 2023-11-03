using Newtonsoft.Json;
using System.Text.Json;

namespace BookingService.SyncDataServices.http
{
    public class AvailabilityService : IAvailabilityService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public AvailabilityService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }
        public async Task<int> GetRoomAvailability(int hotelId)
        {
            var availabilityEndpoint = $"{_configuration["AvailabilityEndpoint"]}" + $"/{hotelId}/availability";
            var response = await _httpClient.GetAsync(availabilityEndpoint);
            
            if (response.IsSuccessStatusCode) 
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();

                if (int.TryParse(jsonResponse, out int roomId) ) 
                {
                    return roomId;
                }
                else
                {
                    throw new InvalidOperationException("The response from the endpoint was ....");

                }
            }
            else
            {
                throw new HttpRequestException($"Error fetching availability: {response.StatusCode}");
            }
        }
    }
}
