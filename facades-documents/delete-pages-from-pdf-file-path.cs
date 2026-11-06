using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        Document pdfDoc = new Document(inputPath);

        // Delete pages 2 through 5 (inclusive).
        // The Pages.Delete method in this Aspose.Pdf version only accepts a single page number.
        // Deleting from the highest page downwards preserves the correct indices.
        for (int page = 5; page >= 2; page--)
        {
            if (page <= pdfDoc.Pages.Count)
                pdfDoc.Pages.Delete(page);
        }

        // Save the modified PDF to the desired output path
        pdfDoc.Save(outputPath);

        Console.WriteLine($"Pages 2‑5 removed. Result saved to '{outputPath}'.");
    }
}
