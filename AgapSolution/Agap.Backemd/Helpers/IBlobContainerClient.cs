using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs;

namespace Agap.Backemd.Helpers
{
    public interface IBlobContainerClient
    {
        Task<BlobClient> GetBlobClientAsync(string name);

        Task CreateIfNotExistsAsync();

        Task SetAccessPolicyAsync(PublicAccessType accessType);
    }
}
