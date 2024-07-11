using Azure.Storage.Blobs;
using Azure.Storage.Sas;
using Microsoft.Extensions.Options;
using Restaurants.Domain.Interface;
using Restaurants.Infrastructure.Configuration;

namespace Restaurants.Infrastructure.Storage;

public class BlobStorageService : IBlobStorageService
{
    private readonly BlobStorageSettings _blobStorageSettings;

    public BlobStorageService(IOptions<BlobStorageSettings> blobStorageSettings)
    {
        _blobStorageSettings = blobStorageSettings.Value;
    }
    public async Task<string> UploadToBlobAsync(Stream data, string filename)
    {
        var blobServiceClient = new BlobServiceClient(_blobStorageSettings.ConnectionString);
        var containerClient = blobServiceClient.GetBlobContainerClient(_blobStorageSettings.LogoContainerName);
        var blobClient = containerClient.GetBlobClient(filename);
        await blobClient.UploadAsync(data);
        var blobUri = blobClient.Uri.ToString();
        return blobUri;
    }
    public string GetBlobSasUrl(string? blobUrl) 
    {
        if (blobUrl == null) return null;
        var sasbuilder = new BlobSasBuilder()
        {
            BlobContainerName = _blobStorageSettings.LogoContainerName,
            Resource = "b",
            StartsOn = DateTimeOffset.UtcNow,
            ExpiresOn = DateTimeOffset.UtcNow.AddMinutes(30),
            BlobName = GetBlobNameFromUrl(blobUrl)
        };
        sasbuilder.SetPermissions(BlobSasPermissions.Read);
        var blobServiceClient = new BlobServiceClient(_blobStorageSettings.ConnectionString);
        var sasToken = sasbuilder
            .ToSasQueryParameters(new Azure.Storage.StorageSharedKeyCredential(blobServiceClient.AccountName, _blobStorageSettings.AccountKey))
            .ToString();
        return $"{blobUrl}?{sasToken}";
    }
    private string GetBlobNameFromUrl(string blobUrl)
    {
        var url = new Uri(blobUrl);
        return url.Segments.Last();
    }
}
