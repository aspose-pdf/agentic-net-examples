using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string altText    = "Accessible image description";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Iterate over all pages
                foreach (Page page in doc.Pages)
                {
                    // Iterate over the image collection (not a dictionary)
                    foreach (XImage img in page.Resources.Images)
                    {
                        // Set alternative text for each image; returns true if successful
                        bool result = img.TrySetAlternativeText(altText, page);
                        // Optional: log the outcome
                        // Console.WriteLine($"Alt text set: {result}");
                    }
                }

                // Save the modified PDF (PDF format, no SaveOptions needed)
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF saved with updated image alt text to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}