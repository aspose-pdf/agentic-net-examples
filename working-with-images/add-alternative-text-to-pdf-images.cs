using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string altText    = "Description of the image for screen readers";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate over each page in the document (1‑based indexing)
            foreach (Page page in doc.Pages)
            {
                // Iterate over each XImage resource on the page
                foreach (XImage img in page.Resources.Images)
                {
                    // Try to set alternative text for the image on this page
                    bool success = img.TrySetAlternativeText(altText, page);
                    if (!success)
                    {
                        Console.WriteLine("Could not set alt text for an image on page " + page.Number);
                    }
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with alternative text to '{outputPath}'.");
    }
}