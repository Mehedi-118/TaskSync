using System.Globalization;

using PTSL.Ovidhan.Web.Core.Helper.Enum;

namespace PTSL.Ovidhan.Web.Core.Helper;

public sealed class FileHelper
{
    public const string Uploads = "uploads";
    private readonly IWebHostEnvironment _hostEnvironment;

    public FileHelper(IWebHostEnvironment hostEnvironment)
    {
        _hostEnvironment = hostEnvironment;
    }

    public bool IsValidImage(IFormFile file)
    {
        if (file is null) return false;

        var extension = Path.GetExtension(file.FileName).ToLower(CultureInfo.InvariantCulture);
        var allowedImageExtensions = new string[] { ".png", ".jpg", ".jpeg", ".gif", ".avif", ".webp" };

        return allowedImageExtensions.Contains(extension);
    }

    public bool IsValidDocument(IFormFile file)
    {
        if (file is null) return false;

        var extension = Path.GetExtension(file.FileName).ToLower(CultureInfo.InvariantCulture);
        var allowedImageExtensions = new string[] { ".doc", ".docx", ".xls", ".xlsx", ".pdf", ".txt" };

        return allowedImageExtensions.Contains(extension);
    }

    // returns (isSaved, fileUrl, fileName)
    public (bool, string, string) SaveFile(IFormFile file, FileType fileType, string directoryName, out string errorMessage)
    {
        if (file is null)
        {
            errorMessage = "File not found";
            return (false, string.Empty, string.Empty);
        }
        if (string.IsNullOrEmpty(directoryName))
        {
            errorMessage = "Directory name must not be empty";
            return (false, string.Empty, string.Empty);
        }

        // Create upload directory is not exists
        var uploadDirectory = Path.GetFullPath(Path.Combine(_hostEnvironment.ContentRootPath, "..", Uploads));
        if (Directory.Exists(uploadDirectory) == false)
        {
            Directory.CreateDirectory(uploadDirectory);
        }

        // Validate file type
        if (fileType == FileType.Image && IsValidImage(file) == false)
        {
            errorMessage = "Invalid image file";
            return (false, string.Empty, string.Empty);
        }
        if (fileType == FileType.Document && IsValidDocument(file) == false)
        {
            errorMessage = "Invalid document file";
            return (false, string.Empty, string.Empty);
        }

        //var fileName = file.FileName;
        var fileExtension = Path.GetExtension(file.FileName).ToLower(CultureInfo.InvariantCulture);

        // New file name
        var currentDateTimeString = DateTime.UtcNow.ToString("yyyyMMddHHmmss");
        var newGuid = Guid.NewGuid().ToString();
        var newDiskFileName = $"{currentDateTimeString}_{newGuid}{fileExtension}";

        // Create save directory is not exists
        var saveDirectory = Path.Combine(uploadDirectory, directoryName);
        if (Directory.Exists(saveDirectory) == false)
        {
            Directory.CreateDirectory(saveDirectory);
        }

        // Save file
        var newDiskFilePath = Path.Combine(saveDirectory, newDiskFileName);
        var publicFileUrl = $"/{Uploads}/{directoryName}/{newDiskFileName}";
        try
        {
            using var fileStream = new FileStream(newDiskFilePath, FileMode.Create, FileAccess.Write);
            file.CopyTo(fileStream);
        }
        catch (Exception)
        {
            errorMessage = "Unable to save file unknown error occurred";
            return (false, string.Empty, string.Empty);
        }

        errorMessage = string.Empty;
        return (true, publicFileUrl, file.FileName);
    }
}
