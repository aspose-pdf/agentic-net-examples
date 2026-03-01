using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "accessible_images.pdf";
        const string altText    = "Image description for accessibility";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal.
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages.
            foreach (Page page in doc.Pages)
            {
                // Iterate over all XImage resources on the current page.
                foreach (XImage img in page.Resources.Images)
                {
                    // Set alternative text for the image. The method returns a bool indicating success.
                    bool success = img.TrySetAlternativeText(altText, page);
                    if (!success)
                    {
                        Console.WriteLine("Failed to set alt text for an image on page " + page.Number);
                    }
                }
            }

            // Save the modified PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Accessible PDF saved to '{outputPath}'.");
    }
}