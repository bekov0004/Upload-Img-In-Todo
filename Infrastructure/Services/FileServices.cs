using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Services;

public class FileServices:IFileService
{
    private IWebHostEnvironment _environment;

    public FileServices(IWebHostEnvironment environment)
    {
        _environment = environment;
    }


    public async Task<string> AddFile(string filename, string folder, IFormFile formFile)
    {
        try
        {
            var path = Path.Combine(_environment.WebRootPath, folder);
            if (Directory.Exists(path) == false )
            {
                Directory.CreateDirectory(path);
            }
            path = Path.Combine(_environment.WebRootPath, folder,filename);
            using (var stream = File.Create(path))
            {
                await formFile.CopyToAsync(stream);
            }
            
            return filename;

        }
        catch (Exception e)
        {
            return null;
        }
    }

    public async Task<bool> DeleteFile(string filename, string folder)
    {
        var path = Path.Combine(_environment.WebRootPath, folder, filename);
        if (File.Exists(path))
        {
            await Task.Run(()=>File.Delete(path));
            return true;
        }

        return false;
    }
}