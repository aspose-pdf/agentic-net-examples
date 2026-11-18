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

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Iterate over each page and each image resource on the page
                foreach (Page page in doc.Pages)
                {
                    foreach (XImage img in page.Resources.Images)
                    {
                        // Set the alternative text for the image
                        img.TrySetAlternativeText(altText, page);
                    }
                }

                // Save the updated PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Alternative text applied and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}