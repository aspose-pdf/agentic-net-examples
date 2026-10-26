using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "accessible_images.pdf";
        const string altText    = "Description of the image for screen readers";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Iterate all pages and all images on each page
                foreach (Page page in doc.Pages)
                {
                    foreach (XImage img in page.Resources.Images)
                    {
                        // Set alternative text for the image; this links the description to the image resource
                        img.TrySetAlternativeText(altText, page);
                    }
                }

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Image alternative text set and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}