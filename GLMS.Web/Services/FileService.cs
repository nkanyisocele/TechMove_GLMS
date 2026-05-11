using GLMS.Web.Interfaces;

namespace GLMS.Web.Services;

public class FileService(IWebHostEnvironment environment) : IFileService
{
    private readonly string _uploadFolder = Path.Combine(environment.WebRootPath, "uploads/contracts");

    public async Task<string> UploadContractFileAsync(IFormFile file)
    {
        // 1. Validation: Ensure it's a PDF
        if (file == null || Path.GetExtension(file.FileName).ToLower() != ".pdf")
        {
            throw new InvalidOperationException("Only PDF files are allowed.");
        }

        // 2. Create directory if it doesn't exist
        if (!Directory.Exists(_uploadFolder)) Directory.CreateDirectory(_uploadFolder);

        // 3. Generate Unique Name (Rubric Requirement)
        string uniqueName = Guid.NewGuid().ToString() + "_" + file.FileName;
        string filePath = Path.Combine(_uploadFolder, uniqueName);

        // 4. Save to Disk
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        return uniqueName; // Return only the name to store in the Database
    }

    public bool DeleteFile(string fileName)
    {
        string filePath = Path.Combine(_uploadFolder, fileName);
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
            return true;
        }
        return false;
    }
}
