using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Path to the uploaded PDF file
        const string inputPath = "uploaded.pdf";

        // Set the maximum file size (in megabytes) that can be loaded into memory
        Document.FileSizeLimitToMemoryLoading = 5; // 5 MB limit

        try
        {
            // Attempt to load the PDF; if the file exceeds the limit, an exception will be thrown
            using (Document doc = new Document(inputPath))
            {
                // If loading succeeds, the file size is within the allowed limit
                Console.WriteLine("File size is within 5 MB limit. Form can be submitted.");
            }
        }
        catch (PdfException)
        {
            // Loading failed due to size limit
            Console.WriteLine("File size exceeds 5 MB limit. Form submission blocked.");
        }
        catch (Exception ex)
        {
            // Handle other unexpected errors
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}