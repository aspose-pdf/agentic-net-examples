using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (wrapped in using for deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // Define the corporate brand color (example RGB 30,144,255)
            Aspose.Pdf.Color brandColor = Aspose.Pdf.Color.FromArgb(30, 144, 255);

            // Apply the background color to every page (Aspose.Pdf uses 1‑based page indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                doc.Pages[i].Background = brandColor;
            }

            // Save the modified PDF (PDF format does not require explicit SaveOptions)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with page background color to '{outputPath}'.");
    }
}