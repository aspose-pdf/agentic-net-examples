using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "resized_output.pdf";

        // Verify source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document
            Document pdfDocument = new Document(inputPath);

            // A4 page size in points (1 point = 1/72 inch)
            const float a4Width = 595f;   // 8.27 in * 72
            const float a4Height = 842f;  // 11.69 in * 72

            // Resize every page while keeping all existing annotations and interactive elements
            foreach (Page page in pdfDocument.Pages)
            {
                page.PageInfo.Width = a4Width;
                page.PageInfo.Height = a4Height;
                // Optional: preserve original orientation if needed
                // page.PageInfo.IsLandscape = a4Width > a4Height;
            }

            // Save the modified PDF
            pdfDocument.Save(outputPath);

            Console.WriteLine($"Resized PDF saved to '{outputPath}'. Annotations are preserved.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during processing: {ex.Message}");
        }
    }
}
