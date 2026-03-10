using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string altText = "Description of the image for accessibility";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document (using statement ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Iterate through each page (Aspose.Pdf uses 1‑based indexing)
            foreach (Page page in doc.Pages)
            {
                // Iterate over all XImage objects on the current page
                foreach (XImage img in page.Resources.Images)
                {
                    // Attempt to set alternative text for the image on this page
                    bool success = img.TrySetAlternativeText(altText, page);
                    if (!success)
                    {
                        // If setting fails, log the page number for diagnostics
                        Console.WriteLine($"Failed to set alt text for an image on page {page.Number}");
                    }
                }
            }

            // Save the modified PDF (no SaveOptions needed for PDF output)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with alternative text to '{outputPath}'.");
    }
}