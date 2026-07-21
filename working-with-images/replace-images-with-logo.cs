using System;
using System.IO;
using Aspose.Pdf;

class ReplaceImagesWithLogo
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string logoImagePath = "logo.png";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(logoImagePath))
        {
            Console.Error.WriteLine($"Logo image not found: {logoImagePath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Read the logo image once into a byte array so we can create fresh streams for each replacement
            byte[] logoBytes = File.ReadAllBytes(logoImagePath);

            // Iterate over all pages (Aspose.Pdf uses 1‑based indexing)
            for (int pageIndex = 1; pageIndex <= pdfDoc.Pages.Count; pageIndex++)
            {
                // Create an absorber that will find all image placements on the current page
                ImagePlacementAbsorber absorber = new ImagePlacementAbsorber();

                // Perform the search on the page
                pdfDoc.Pages[pageIndex].Accept(absorber);

                // Replace each found image with the logo
                foreach (ImagePlacement placement in absorber.ImagePlacements)
                {
                    // Create a new memory stream for the logo image (the Replace method consumes the stream)
                    using (MemoryStream logoStream = new MemoryStream(logoBytes))
                    {
                        placement.Replace(logoStream);
                    }
                }
            }

            // Save the modified PDF
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Images replaced with logo and saved to '{outputPdfPath}'.");
    }
}