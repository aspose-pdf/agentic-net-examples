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

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages
            foreach (Page page in doc.Pages)
            {
                // Iterate through all XImage resources on the current page
                foreach (XImage img in page.Resources.Images)
                {
                    // Set alternative text for the image; returns true if successful
                    bool result = img.TrySetAlternativeText(altText, page);
                    // Optional: log the outcome
                    // Console.WriteLine($"Image alt text set: {result}");
                }
            }

            // Save the updated PDF (lifecycle rule: use Document.Save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with alternate text to '{outputPath}'.");
    }
}