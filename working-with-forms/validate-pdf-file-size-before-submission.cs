using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string filePath = "uploaded.pdf";
        const long maxSizeBytes = 5L * 1024 * 1024; // 5 MB

        // Verify the file exists
        FileInfo fileInfo = new FileInfo(filePath);
        if (!fileInfo.Exists)
        {
            Console.Error.WriteLine($"File not found: {filePath}");
            return;
        }

        // Reject files larger than 5 MB
        if (fileInfo.Length > maxSizeBytes)
        {
            Console.Error.WriteLine("File size exceeds 5 MB limit. Submission rejected.");
            return;
        }

        // Additional safety: limit Aspose.Pdf memory loading to 5 MB
        Document.FileSizeLimitToMemoryLoading = 5; // value is in megabytes

        try
        {
            // Load the PDF within a using block for proper disposal
            using (Document doc = new Document(filePath))
            {
                // At this point the file size is acceptable and the document is loaded
                Console.WriteLine("Document loaded successfully. Proceeding with form submission.");

                // Example processing: save a copy (replace with actual submission logic)
                doc.Save("processed.pdf");
            }
        }
        catch (Exception ex)
        {
            // Handle any loading errors (e.g., corrupted file, exceeding internal limits)
            Console.Error.WriteLine($"Error loading document: {ex.Message}");
        }
    }
}