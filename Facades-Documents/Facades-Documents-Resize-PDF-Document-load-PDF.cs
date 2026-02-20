using System;
using System.IO;
using Aspose.Pdf.Facades;   // Facade classes for PDF manipulation
using Aspose.Pdf;           // Contains PageSize enum

class Program
{
    static void Main(string[] args)
    {
        // Input and output PDF file paths (can be passed as arguments or hard‑coded)
        string inputPath = args.Length > 0 ? args[0] : "input.pdf";
        string outputPath = args.Length > 1 ? args[1] : "output_resized.pdf";

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found – {inputPath}");
            return;
        }

        try
        {
            // Create the PdfPageEditor facade
            PdfPageEditor pageEditor = new PdfPageEditor();

            // Load the existing PDF document
            pageEditor.BindPdf(inputPath);

            // Set the desired page size for all pages (e.g., A4)
            pageEditor.PageSize = PageSize.A4;

            // Optionally, adjust zoom if needed (1.0 = 100%)
            // pageEditor.Zoom = 1.0f;

            // Save the resized PDF to the output file
            pageEditor.Save(outputPath);

            // Release resources
            pageEditor.Close();

            Console.WriteLine($"PDF resized successfully and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}