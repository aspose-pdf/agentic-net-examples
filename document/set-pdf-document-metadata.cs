using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string outputPath = "output.pdf";

        // Ensure the output directory exists (handle case where there is no directory part)
        string outputDir = Path.GetDirectoryName(outputPath) ?? string.Empty;
        if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
            Directory.CreateDirectory(outputDir);

        // Create a new PDF document and set its metadata
        using (Document doc = new Document())
        {
            // Set creation and modification dates (local time is required by Aspose.Pdf)
            doc.Info.CreationDate = DateTime.Now;
            doc.Info.ModDate = DateTime.Now;

            // Set custom keywords (comma‑separated)
            doc.Info.Keywords = "Aspose.Pdf, Metadata, Example";

            // Optionally add a blank page so the PDF is not empty
            doc.Pages.Add();

            // Save the PDF (no SaveOptions needed for PDF format)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with metadata to '{outputPath}'.");
    }
}
