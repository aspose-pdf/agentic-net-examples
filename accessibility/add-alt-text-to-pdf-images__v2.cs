using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string keywords   = "sample, diagram, example";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages
            foreach (Page page in doc.Pages)
            {
                // Iterate over all images on the current page
                foreach (XImage img in page.Resources.Images)
                {
                    // Create a descriptive alt text that includes the keywords
                    string altText = $"Image description: {keywords}";

                    // Set the alternative text on the image resource
                    img.TrySetAlternativeText(altText, page);
                }
            }

            // Save the modified PDF (PDF format is the default)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with updated image alt text to '{outputPath}'.");
    }
}