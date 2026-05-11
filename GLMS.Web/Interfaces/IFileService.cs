namespace GLMS.Web.Interfaces;

public interface IFileService
{
    Task<string> UploadContractFileAsync(IFormFile file);
    bool DeleteFile(string fileName);
}
