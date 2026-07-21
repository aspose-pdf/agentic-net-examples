using System;
using System.IO;
using Aspose.Pdf;

class FileSizeValidator
{
    // Maximum allowed file size in megabytes
    private const int MaxFileSizeMb = 5;

    /// <summary>
    /// Validates that the PDF file at <paramref name="filePath"/> does not exceed the allowed size.
    /// Returns true if the file size is within the limit; otherwise false.
    /// </summary>
    public static bool Validate(string filePath)
    {
        if (!File.Exists(filePath))
        {
            Console.Error.WriteLine($"File not found: {filePath}");
            return false;
        }

        // Set the global limit for loading a whole file into memory (value is in MB)
        Document.FileSizeLimitToMemoryLoading = MaxFileSizeMb;

        try
        {
            // Attempt to load the document; if the file exceeds the limit,
            // Aspose.Pdf will throw a PdfException.
            using (Document doc = new Document(filePath))
            {
                // Document loaded successfully – size is within the limit.
                Console.WriteLine($"File '{Path.GetFileName(filePath)}' loaded successfully (≤ {MaxFileSizeMb} MB).");
                return true;
            }
        }
        catch (PdfException ex)
        {
            // Expected when the file size exceeds the configured limit.
            Console.Error.WriteLine($"File size exceeds {MaxFileSizeMb} MB limit: {ex.Message}");
            return false;
        }
        catch (Exception ex)
        {
            // Any other unexpected errors.
            Console.Error.WriteLine($"Error loading file: {ex.Message}");
            return false;
        }
    }

    // Example usage
    static void Main()
    {
        const string uploadedPdfPath = "uploaded_form.pdf";

        bool isValid = Validate(uploadedPdfPath);
        if (isValid)
        {
            Console.WriteLine("Proceed with form submission.");
            // Continue processing the PDF (e.g., extract data, save, etc.)
        }
        else
        {
            Console.WriteLine("Reject form submission due to oversized file.");
        }
    }
}