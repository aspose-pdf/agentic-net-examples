using System;
using System.IO;
using Aspose.Pdf;               // Core API
using Aspose.Pdf.Text;          // For PageSize class (if needed)

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "resized_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the source PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Target printable area in points (A4 size)
            const double targetWidth  = 595; // points
            const double targetHeight = 842; // points

            // Iterate over all pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Resize the page; this scales the page content proportionally
                page.Resize(new PageSize((float)targetWidth, (float)targetHeight));
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Resized PDF saved to '{outputPath}'.");
    }
}