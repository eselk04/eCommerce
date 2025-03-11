using eCommerce.Application.Interfaces.Storage;

namespace eCommerce.Infrastructure.Storage;

public class LocalStorage : IStorage
{
    private IWebHostEnvironment webHostEnvironment;

    public LocalStorage(IWebHostEnvironment _webHostEnvironment)
    {
        webHostEnvironment = _webHostEnvironment;
    }
    public async Task<List<(string fileName, string path)>> UploadAsync(string pathOrContainer, IFormFileCollection files)
    {
        string path = Path.Combine(webHostEnvironment.WebRootPath, pathOrContainer);
        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);
        
        List<(string fileName, string path)> filesList = new();
        foreach (IFormFile file in files)
        {
            await CopyFileAsync(Path.Combine(path,renameFileAsync(file.Name).ToString()),file);
            filesList.Add((file.Name , $"{path}//{file.Name}"));
        }
        return filesList;
    }

    public async Task DeleteAsync(string pathOrContainerName, string fileName) =>  System.IO.File.Delete(pathOrContainerName +"/" +  fileName);

    public IList<string> GetFiles(string pathOrContainerName)
    {
        throw new NotImplementedException();
    }

    public bool HasFile(string pathOrContainerName, string fileName) =>  System.IO.File.Exists(pathOrContainerName +"/" +  fileName);
    
            
    
    private async Task<bool> CopyFileAsync(string path, IFormFile file)
    {
            await using FileStream fileStream = new(path, FileMode.Create, FileAccess.Write, FileShare.None, 1024 * 1024, 
                useAsync: true);
            await file.CopyToAsync(fileStream);
            await fileStream.FlushAsync();
            return true;
    }
    private async Task<string> renameFileAsync(string fileName)
    {
        
        string newFileName = await Task.Run(() =>
        {
            string extension = Path.GetExtension(fileName);
            string oldName = fileName;
            string newName = $"{fileName}{Guid.NewGuid().ToString()}{DateTime.Now.ToString("yyyyMMddHHmmss")}";
            string fileNewName = $"{newName}{extension}";
            return fileNewName;
        });

        return newFileName;


    }
}