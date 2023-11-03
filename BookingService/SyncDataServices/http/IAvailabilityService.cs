namespace BookingService.SyncDataServices.http
{
    public interface IAvailabilityService
    {
        Task<int> GetRoomAvailability(int hotelId);
    }
}
