using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "accessible_images.pdf";
        const string altText    = "Descriptive alternate text for the image";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Iterate over each XImage resource on the page
                foreach (XImage img in page.Resources.Images)
                {
                    // Set alternate text for the image; returns true if successful
                    bool success = img.TrySetAlternativeText(altText, page);
                    if (!success)
                    {
                        Console.WriteLine($"Could not set alt text for an image on page {i}.");
                    }
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with alternate text: {outputPath}");
    }
}