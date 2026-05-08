using System;
using System.IO;
using Aspose.Pdf;

class ReplaceImagesWithLogo
{
    static void Main()
    {
        // Paths for the source PDF, the logo image, and the output PDF.
        const string inputPdfPath  = "input.pdf";
        const string logoImagePath = "logo.png";
        const string outputPdfPath = "output.pdf";

        // Validate that the required files exist.
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

        // Load the logo image once into memory; it will be used for all replacements.
        byte[] logoBytes = File.ReadAllBytes(logoImagePath);

        // Open the PDF document inside a using block for deterministic disposal.
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Iterate over all pages (Aspose.Pdf uses 1‑based indexing).
            for (int pageIndex = 1; pageIndex <= pdfDoc.Pages.Count; pageIndex++)
            {
                Page page = pdfDoc.Pages[pageIndex];

                // Create an absorber that finds image placements on the current page.
                ImagePlacementAbsorber absorber = new ImagePlacementAbsorber();

                // Perform the search on the page.
                page.Accept(absorber);

                // Replace each found image with the logo.
                foreach (ImagePlacement placement in absorber.ImagePlacements)
                {
                    // Use a fresh MemoryStream for each replacement.
                    using (MemoryStream logoStream = new MemoryStream(logoBytes))
                    {
                        placement.Replace(logoStream);
                    }
                }
            }

            // Save the modified document.
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Images replaced with logo. Output saved to '{outputPdfPath}'.");
    }
}