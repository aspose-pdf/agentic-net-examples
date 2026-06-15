using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string altText    = "Accessible description of the image";

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages
            foreach (Page page in doc.Pages)
            {
                // Iterate over all images on the current page
                foreach (XImage img in page.Resources.Images)
                {
                    // Set alternative text for the image.
                    // The method returns a bool indicating success; we ignore it here.
                    img.TrySetAlternativeText(altText, page);
                }
            }

            // Save the modified PDF (lifecycle rule: use Save inside the using block)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with alternative text to '{outputPath}'.");
    }
}