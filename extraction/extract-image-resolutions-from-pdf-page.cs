using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Text; // required for ImagePlacementAbsorber

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputJsonPath = "image_resolutions.json";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Wrap the Document in a using block for deterministic disposal
        using (Document doc = new Document(inputPdfPath))
        {
            // Page indexing in Aspose.Pdf is 1‑based
            const int targetPageNumber = 22;
            if (targetPageNumber > doc.Pages.Count)
            {
                Console.Error.WriteLine($"Document has only {doc.Pages.Count} pages. Page {targetPageNumber} is out of range.");
                return;
            }

            Page page = doc.Pages[targetPageNumber];

            // Absorb image placements on the selected page
            ImagePlacementAbsorber imgAbsorber = new ImagePlacementAbsorber();
            page.Accept(imgAbsorber);

            // Map generated filenames to resolution strings (e.g., "300x300 DPI")
            Dictionary<string, string> resolutions = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            int imageIndex = 1;

            foreach (ImagePlacement placement in imgAbsorber.ImagePlacements)
            {
                // Generate a deterministic placeholder name for each image
                string fileName = $"Image_{imageIndex}.bin";

                // Retrieve horizontal and vertical resolution (DPI). If not available, defaults to 0.
                int hRes = (int)placement.Resolution.X;
                int vRes = (int)placement.Resolution.Y;

                string resolution = $"{hRes}x{vRes} DPI";
                resolutions[fileName] = resolution;
                imageIndex++;
            }

            // Serialize the dictionary to JSON with indentation for readability
            string json = JsonSerializer.Serialize(resolutions, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(outputJsonPath, json);
            Console.WriteLine($"Image resolution map written to '{outputJsonPath}'.");
        }
    }
}
