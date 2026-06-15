using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string logoPath = "logo.png";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        if (!File.Exists(logoPath))
        {
            Console.Error.WriteLine($"Logo image not found: {logoPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Iterate over all pages (1‑based indexing)
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                // Create a new absorber for the current page
                ImagePlacementAbsorber absorber = new ImagePlacementAbsorber();

                // Search for image placements on this page
                doc.Pages[pageIndex].Accept(absorber);

                // Replace each found image with the new logo
                foreach (ImagePlacement placement in absorber.ImagePlacements)
                {
                    // Open the logo file as a stream and replace the image
                    using (FileStream logoStream = File.OpenRead(logoPath))
                    {
                        placement.Replace(logoStream);
                    }
                }
            }

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"All images replaced and saved to '{outputPdf}'.");
    }
}