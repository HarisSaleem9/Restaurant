namespace Restaurants.Domain.Interface;

public interface IBlobStorageService
{
    Task<string> UploadToBlobAsync(Stream data , string filename);
    string GetBlobSasUrl(string? blobUrl);
}
