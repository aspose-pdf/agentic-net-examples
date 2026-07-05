using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_zoomed.pdf";

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Create and bind the PDF using PdfPageEditor (facade)
            using (PdfPageEditor editor = new PdfPageEditor())
            {
                editor.BindPdf(inputPath);

                // Set zoom coefficient to 1.0 (100%) for all pages
                editor.Zoom = 1.0f;

                // Apply the changes before saving
                editor.ApplyChanges();

                // Save the modified PDF
                editor.Save(outputPath);
            }

            Console.WriteLine($"Zoom set to 1.0 for all pages. Saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}