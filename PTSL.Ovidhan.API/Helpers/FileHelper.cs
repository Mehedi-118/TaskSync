using System;
using System.Globalization;
using System.IO;
using System.Linq;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

using PTSL.Ovidhan.Common.Enum;

namespace PTSL.Ovidhan.Api.Helpers;

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
        return IsValidImage(extension);
    }

    public bool IsValidImage(string extension)
    {
        var allowedExtensions = new string[] { ".png", ".jpg", ".jpeg", ".gif", ".avif", ".webp" };
        return allowedExtensions.Contains(extension);
    }

    public bool IsValidDocument(IFormFile file)
    {
        if (file is null) return false;

        var extension = Path.GetExtension(file.FileName).ToLower(CultureInfo.InvariantCulture);
        return IsValidDocument(extension);
    }

    public bool IsValidDocument(string extension)
    {
        var allowedExtensions = new string[] { ".doc", ".docx", ".xls", ".xlsx", ".pdf", ".txt" };
        return allowedExtensions.Contains(extension);
    }

    public bool IsValidVideo(IFormFile file)
    {
        if (file is null) return false;

        var extension = Path.GetExtension(file.FileName).ToLower(CultureInfo.InvariantCulture);
        return IsValidVideo(extension);
    }

    public bool IsValidVideo(string extension)
    {
        //var allowedVideoExtensions = new string[] { ".mp4", ".mov", ".wmv", ".flv", ".avi", ".webm", ".mkv" };
        var allowedExtensions = new string[] { ".mp4" };
        return allowedExtensions.Contains(extension);
    }

    public bool IsValidAudio(IFormFile file)
    {
        if (file is null) return false;

        var extension = Path.GetExtension(file.FileName).ToLower(CultureInfo.InvariantCulture);
        return IsValidAudio(extension);
    }

    public bool IsValidAudio(string extension)
    {
        var allowedExtensions = new string[] { ".wav", ".mp3", ".aac", ".ogg", ".wma", ".m4a" };
        return allowedExtensions.Contains(extension);
    }

    // returns (isSaved, fileUrl, fileName)
    public (bool success, string fileUrl, string fileName, string message) SaveFile(IFormFile file, FileType fileType, string directoryName, string fileName = null)
    {
        string message = string.Empty;
        try
        {
            if (file is null)
            {
                message = "File not found";
                return (false, string.Empty, string.Empty, message);
            }
            if (string.IsNullOrEmpty(directoryName))
            {
                message = "Directory name must not be empty";
                return (false, string.Empty, string.Empty, message);
            }

            // Create upload directory is not exists
            var uploadDirectory = GetFullPath(Uploads);
            if (Directory.Exists(uploadDirectory) == false)
            {
                Directory.CreateDirectory(uploadDirectory);
            }

            // Validate file type
            (bool success, string message) fileTypeValidationResult = FileTypeValidation(file, fileType);
            if (!fileTypeValidationResult.success)
            {
                message = fileTypeValidationResult.message;
                return (false, string.Empty, string.Empty, message);
            }

            //var fileName = file.FileName;
            var fileExtension = Path.GetExtension(file.FileName).ToLower(CultureInfo.InvariantCulture);

            // New file name
            var newFileName = fileName == null ? $"{DateTime.UtcNow.ToString("yyyyMMddHHmmss")}_{Guid.NewGuid()}" : fileName;

            var newDiskFileName = $"{newFileName}{fileExtension}";

            // Create save directory is not exists
            var saveDirectory = Path.Combine(uploadDirectory, directoryName);
            if (Directory.Exists(saveDirectory) == false)
            {
                Directory.CreateDirectory(saveDirectory);
            }

            // Save file
            var newDiskFilePath = Path.Combine(saveDirectory, newDiskFileName);
            var publicFileUrl = $"{Uploads}/{directoryName}/{newDiskFileName}";
            try
            {
                using var fileStream = new FileStream(newDiskFilePath, FileMode.Create, FileAccess.Write);
                file.CopyTo(fileStream);
            }
            catch (Exception)
            {
                message = "Unable to save file unknown error occurred";
                return (false, string.Empty, string.Empty, message);
            }

            message = string.Empty;
            return (true, publicFileUrl, newDiskFileName, message);
        }
        catch (Exception ex)
        {
            message = "Failed to save file";
            return (false, string.Empty, string.Empty, message);
        }
    }

    public (bool success, string message) DeleteFile(string filePath)
    {
        string message = string.Empty;
        try
        {
            var fullPath = GetFullPath(filePath);
            // Check if the file exists
            if (File.Exists(fullPath))
            {
                // Delete the file
                File.Delete(fullPath);
                message = "File deleted successfully";
                return (true, message);
            }
            else
            {
                message = "File does not exist";
                return (false, message);
            }
        }
        catch (IOException ex)
        {
            message = "Failed to delete file";
            return (false, message);
        }
    }

    public (byte[] bytes, string contentType, string fileName, string message) GetFile(string filePath)
    {
        string message = string.Empty;
        try
        {
            // Combine the storage path with the requested filename
            //var imagePath = Path.GetFullPath(Path.Combine(_hostEnvironment.ContentRootPath, "..", filePath));
            var imagePath = GetFullPath(filePath);

            // Check if the file exists
            if (!File.Exists(imagePath))
            {
                message = "File not found";
                return (bytes: null!, contentType: null!, Path.GetFileName(filePath), message);
            }

            // Read the image file into a byte array
            byte[] imageData = File.ReadAllBytes(imagePath);

            // Determine the content type based on the file extension
            string contentType = GetContentType(imagePath);

            // Return the image file as a FileResult
            message = "File found";
            return (bytes: imageData, contentType: contentType, Path.GetFileName(filePath), message);
        }
        catch (Exception ex)
        {
            message = "File not found";
            return (bytes: null!, contentType: null!, Path.GetFileName(filePath), message);
        }
    }

    public string GetFullPath(string filePath)
    {
        // Combine the storage path with the requested filename
        string fullPath = Path.GetFullPath(Path.Combine(_hostEnvironment.ContentRootPath, "..", filePath));
        return fullPath;
    }

    private string GetContentType(string filename)
    {
        // Map file extensions to content types
        switch (Path.GetExtension(filename).ToLower())
        {
            case ".jpg":
            case ".jpeg":
                return "image/jpeg";
            case ".png":
                return "image/png";
            case ".gif":
                return "image/gif";
            case ".avif":
                return "image/avif";
            case ".webp":
                return "image/webp";
            // Add more file extensions and content types as needed
            default:
                return "application/octet-stream"; // Default to binary content type
        }
    }

    private (bool success, string message) FileTypeValidation(IFormFile file, FileType fileType)
    {
        var fileTypeString = fileType.ToString().ToLower();

        if (fileType == FileType.Any)
        {
            return (true, $"Valid {fileTypeString} file");
        }

        if (fileType == FileType.Image && !IsValidImage(file))
        {
            return (false, $"Invalid {fileTypeString} file");
        }

        if (fileType == FileType.Document && !IsValidDocument(file))
        {
            return (false, $"Invalid {fileTypeString} file");
        }

        if (fileType == FileType.Video && !IsValidVideo(file))
        {
            return (false, $"Invalid {fileTypeString} file");
        }

        if (fileType == FileType.Audio && !IsValidAudio(file))
        {
            return (false, $"Invalid {fileTypeString} file");
        }

        return (true, $"Valid {fileTypeString} file");
    }

}
