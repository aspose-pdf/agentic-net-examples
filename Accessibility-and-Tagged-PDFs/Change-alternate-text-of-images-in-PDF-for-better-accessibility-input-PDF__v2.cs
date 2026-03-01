using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string altText    = "Descriptive alternative text for the image";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages
            foreach (Page page in doc.Pages)
            {
                // Iterate over all images on the current page
                foreach (XImage img in page.Resources.Images)
                {
                    // Set alternative text for the image
                    bool success = img.TrySetAlternativeText(altText, page);
                    if (!success)
                    {
                        Console.WriteLine("Could not set alternative text for an image on page " + page.Number);
                    }
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with updated image alternative text to '{outputPath}'.");
    }
}