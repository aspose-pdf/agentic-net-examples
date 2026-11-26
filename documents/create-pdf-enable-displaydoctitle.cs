using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string outputPath = "TitleDisplayedNew.pdf";

        // Ensure the output directory exists
        string outDir = Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(outDir) && !Directory.Exists(outDir))
            Directory.CreateDirectory(outDir);

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a blank page (pages are 1‑based)
            doc.Pages.Add();

            // Enable the DisplayDocTitle flag so PDF viewers show the document title
            doc.DisplayDocTitle = true;

            // Optional: set the document title metadata
            doc.Info.Title = "My PDF with DisplayDocTitle";

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}