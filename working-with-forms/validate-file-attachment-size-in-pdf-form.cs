using System;
using System.IO;

class FileValidator
{
    // Maximum allowed file size: 5 MB (5 * 1024 * 1024 bytes)
    private const long MaxFileSizeBytes = 5L * 1024 * 1024;

    /// <summary>
    /// Checks whether the file at <paramref name="filePath"/> is within the allowed size limit.
    /// </summary>
    /// <param name="filePath">Full path to the file to validate.</param>
    /// <returns>True if the file size is ≤ 5 MB; otherwise false.</returns>
    /// <exception cref="FileNotFoundException">Thrown when the file does not exist.</exception>
    public static bool ValidateFileSize(string filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath))
            throw new ArgumentException("File path must be provided.", nameof(filePath));

        if (!File.Exists(filePath))
            throw new FileNotFoundException("File not found.", filePath);

        // Use FileInfo to obtain the length without loading the file into memory.
        FileInfo info = new FileInfo(filePath);
        long fileSize = info.Length;

        // Return true if size is within limit, false otherwise.
        return fileSize <= MaxFileSizeBytes;
    }

    // Example usage:
    static void Main()
    {
        string path = "uploadedDocument.pdf";

        try
        {
            bool isValid = ValidateFileSize(path);
            if (isValid)
            {
                Console.WriteLine("File size is acceptable. Proceed with form submission.");
                // TODO: Add form submission logic here.
            }
            else
            {
                Console.WriteLine("File exceeds the 5 MB size limit. Reject submission.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Validation error: {ex.Message}");
        }
    }
}