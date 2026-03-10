using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "trimmed.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Retrieve the original page dimensions
                double pageWidth = page.PageInfo.Width;
                double pageHeight = page.PageInfo.Height;

                // Define a uniform margin to trim from each side (adjust as needed)
                double margin = 20.0; // points

                // Set a new CropBox that excludes the surrounding white space
                page.CropBox = new Aspose.Pdf.Rectangle(
                    margin,                     // lower‑left X
                    margin,                     // lower‑left Y
                    pageWidth - margin,         // upper‑right X
                    pageHeight - margin);       // upper‑right Y
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Trimmed PDF saved to '{outputPath}'.");
    }
}