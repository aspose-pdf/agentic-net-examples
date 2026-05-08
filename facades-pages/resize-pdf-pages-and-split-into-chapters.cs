using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdf = "input.pdf";                 // source PDF
        const string resizedPdf = "temp_resized.pdf";        // intermediate resized PDF
        const string outputFolder = "Chapters";              // folder for split chapters

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Uniform size for the content after resizing (points; 1 inch = 72 points)
        // Example: width = 500 points, height = 700 points
        const double targetWidth = 500.0;
        const double targetHeight = 700.0;

        // PdfFileEditor does NOT implement IDisposable – do NOT wrap in using
        PdfFileEditor editor = new PdfFileEditor();

        try
        {
            // 1. Resize all pages uniformly.
            // Passing null for the pages array applies the operation to every page.
            editor.ResizeContents(inputPdf, resizedPdf, null, targetWidth, targetHeight);

            // 2. Split the resized PDF into separate files.
            // %NUM% in the template is replaced by the page number (starting at 1).
            string template = Path.Combine(outputFolder, "Chapter%NUM%.pdf");
            editor.SplitToPages(resizedPdf, template);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
        finally
        {
            // Clean up the temporary resized PDF if it exists
            if (File.Exists(resizedPdf))
            {
                try { File.Delete(resizedPdf); } catch { /* ignore cleanup errors */ }
            }
        }

        Console.WriteLine("Processing completed.");
    }
}