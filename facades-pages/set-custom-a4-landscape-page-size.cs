using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_widescreen.pdf";

        // Ensure the source PDF exists – create a minimal placeholder if it does not.
        if (!File.Exists(inputPath))
        {
            using (var placeholder = new Document())
            {
                placeholder.Pages.Add();
                placeholder.Save(inputPath);
            }
        }

        // Initialize the facade and bind the source PDF
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            editor.BindPdf(inputPath);

            // Convert millimetres to points (1 inch = 72 points, 1 mm = 72/25.4 points)
            const double mmToPoints = 72.0 / 25.4;

            // A4 landscape dimensions: width = 297 mm, height = 210 mm
            double width = 297 * mmToPoints;   // ≈ 842.52 points
            double height = 210 * mmToPoints;  // ≈ 595.28 points

            // Set a custom page size using Width and Height (landscape A4)
            PageSize customSize = new PageSize((float)width, (float)height);
            editor.PageSize = customSize;

            // Apply the size change to all pages
            editor.ApplyChanges();

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with custom A4 landscape size to '{outputPath}'.");
    }
}
