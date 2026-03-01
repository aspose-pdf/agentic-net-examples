using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_accessible.pdf";
        const string altText    = "Descriptive alternate text for the image";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages (1‑based indexing)
            foreach (Page page in doc.Pages)
            {
                // Iterate over all XImage objects on the page
                foreach (XImage img in page.Resources.Images)
                {
                    // Set alternate text for the image on the current page
                    // Returns true if the text was set successfully
                    bool success = img.TrySetAlternativeText(altText, page);
                    if (!success)
                    {
                        Console.WriteLine("Could not set alternate text for an image on page " + page.Number);
                    }
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Accessible PDF saved to '{outputPath}'.");
    }
}