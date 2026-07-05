using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_letter.pdf";

        // Verify that the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPath}");
            return;
        }

        // Use PdfPageEditor (IDisposable) to bind, resize, and save the PDF
        using (PdfPageEditor pageEditor = new PdfPageEditor())
        {
            pageEditor.BindPdf(inputPath);

            // Set the target page size to Letter (8.5 x 11 inches).
            // Letter size in points: 8.5" * 72 = 612, 11" * 72 = 792.
            pageEditor.PageSize = new PageSize(612, 792);

            // Save the resized document to the destination path
            pageEditor.Save(outputPath);
        }

        Console.WriteLine($"PDF resized to Letter size and saved as '{outputPath}'.");
    }
}
