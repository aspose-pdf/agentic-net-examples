using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

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
                // Iterate over all layers on the current page (if any)
                foreach (Layer layer in page.Layers)
                {
                    // Make the layer visible by flattening it.
                    // Flatten(true) removes the optional content group markers and merges the content into the page.
                    layer.Flatten(true);

                    // To hide a layer you could instead call:
                    // layer.Delete();
                }
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Layer visibility updated and saved to '{outputPath}'.");
    }
}