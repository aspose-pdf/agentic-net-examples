using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_zoomed.pdf";

        // Verify the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Use PdfPageEditor (facade) to set zoom on page 7
        using (Aspose.Pdf.Facades.PdfPageEditor editor = new Aspose.Pdf.Facades.PdfPageEditor())
        {
            // Load the PDF document
            editor.BindPdf(inputPath);

            // Specify the page(s) to process – page numbers are 1‑based
            editor.ProcessPages = new int[] { 7 };

            // Set zoom to 2.0 (200 % magnification)
            editor.Zoom = 2.0f;

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Page 7 zoom set to 200% and saved to '{outputPath}'.");
    }
}