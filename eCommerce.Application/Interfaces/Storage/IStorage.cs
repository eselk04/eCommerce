using Domain.Entities.Common;

namespace eCommerce.Application.Interfaces.Storage;

public interface IStorage 
{
    Task<List<(string fileName, string path )>> UploadAsync(string pathOrContainer , IFormFileCollection files);
    
    Task DeleteAsync(string pathOrContainerName ,string fileName);
    
    IList<string> GetFiles(string pathOrContainerName);
    
    bool HasFile(string pathOrContainerName, string fileName);
    
}