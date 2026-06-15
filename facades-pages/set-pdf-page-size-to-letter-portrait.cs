using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf; // for PageSize

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_letter.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Letter size in points (8.5 x 11 inches)
        const double widthPoints = 8.5 * 72;
        const double heightPoints = 11 * 72;

        // Use PdfPageEditor facade to change page size
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Load the PDF
            editor.BindPdf(inputPath);

            // Set page size for all pages
            editor.PageSize = new PageSize((float)widthPoints, (float)heightPoints);

            // Apply changes
            editor.ApplyChanges();

            // Save the result
            editor.Save(outputPath);
        }

        Console.WriteLine($"Page size set to Letter portrait. Saved to '{outputPath}'.");
    }
}