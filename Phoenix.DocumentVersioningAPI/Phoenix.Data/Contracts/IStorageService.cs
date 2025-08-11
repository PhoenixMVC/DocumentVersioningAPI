namespace Phoenix.Data.Contracts
{
    public interface IStorageService
    {
        Task<string> SaveFileAsync(Stream fileStream, string fileName);
    }
}
