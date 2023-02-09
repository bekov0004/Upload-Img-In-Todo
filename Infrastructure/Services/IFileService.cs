using Microsoft.AspNetCore.Http;

namespace Infrastructure.Services;

public interface IFileService
{
    Task<string> AddFile(string filename, string folder, IFormFile formFile);
    Task<bool> DeleteFile(string filename, string folder);
}