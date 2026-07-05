using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";          // source PDF
        const string outputPdf = "output.pdf";        // result PDF
        const string logoPath = "logo.png";           // new logo image

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        if (!File.Exists(logoPath))
        {
            Console.Error.WriteLine($"Logo file not found: {logoPath}");
            return;
        }

        // Load the PDF document (wrapped in using for deterministic disposal)
        using (Document doc = new Document(inputPdf))
        {
            // Iterate over all pages (Aspose.Pdf uses 1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Search for image placements on the current page
                ImagePlacementAbsorber absorber = new ImagePlacementAbsorber();
                page.Accept(absorber);

                // Replace each found image with the new logo
                foreach (ImagePlacement placement in absorber.ImagePlacements)
                {
                    // Open the logo image stream for each replacement
                    using (FileStream logoStream = File.OpenRead(logoPath))
                    {
                        placement.Replace(logoStream);
                    }
                }
            }

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Images replaced and saved to '{outputPdf}'.");
    }
}