namespace BookingService.Services
{
    public interface IKeyVaultService
    {
        string GetSecret(string name);
    }
}
