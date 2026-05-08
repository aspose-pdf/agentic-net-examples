using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "custom_letter.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Use PdfPageEditor (Facades) to change page size
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Load the source PDF
            editor.BindPdf(inputPath);

            // Letter size in points (1 inch = 72 points)
            float width = 8.5f * 72f;   // 612 points
            float height = 11f * 72f;   // 792 points

            // Set custom page size (portrait Letter)
            editor.PageSize = new PageSize(width, height);

            // Apply the changes
            editor.ApplyChanges();

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with custom Letter size to '{outputPath}'.");
    }
}