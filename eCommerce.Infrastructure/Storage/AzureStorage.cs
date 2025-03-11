using eCommerce.Application.Interfaces.Storage;

namespace eCommerce.Infrastructure.Storage;

public class AzureStorage : IStorage
{
    public Task<List<(string fileName, string path)>> UploadAsync(string pathOrContainer, IFormFileCollection files)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(string pathOrContainerName, string fileName)
    {
        throw new NotImplementedException();
    }

    public IList<string> GetFiles(string pathOrContainerName)
    {
        throw new NotImplementedException();
    }

    public bool HasFile(string pathOrContainerName, string fileName)
    {
        throw new NotImplementedException();
    }
}