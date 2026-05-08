using System;
using System.IO;
using Aspose.Pdf;               // Core API
using Aspose.Pdf.Devices;      // For page processing if needed (not used here)

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_grayscale_even_pages.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Pages are 1‑based in Aspose.Pdf
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                // Process only even‑numbered pages
                if (i % 2 == 0)
                {
                    // Convert the entire page to grayscale (includes all images on the page)
                    doc.Pages[i].MakeGrayscale();
                }
            }

            // Save the modified document (PDF format)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}