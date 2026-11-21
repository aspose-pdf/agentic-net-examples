using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string logoPath = "logo.png";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(logoPath))
        {
            Console.Error.WriteLine($"Logo image not found: {logoPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal.
        using (Document doc = new Document(inputPdfPath))
        {
            // Iterate over all pages (Aspose.Pdf uses 1‑based indexing).
            for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
            {
                Page page = doc.Pages[pageNum];

                // Locate existing images on the page.
                ImagePlacementAbsorber imgAbsorber = new ImagePlacementAbsorber();
                page.Accept(imgAbsorber);

                // Replace each found image with the new logo.
                foreach (ImagePlacement placement in imgAbsorber.ImagePlacements)
                {
                    // Create a stamp that uses the logo image.
                    ImageStamp logoStamp = new ImageStamp(logoPath)
                    {
                        // Position the logo exactly where the original image was.
                        // ImageStamp uses XIndent/YIndent for placement and Width/Height for size.
                        XIndent = placement.Rectangle.LLX,
                        YIndent = placement.Rectangle.LLY,
                        Width = placement.Rectangle.Width,
                        Height = placement.Rectangle.Height,

                        // Ensure the logo fully covers the original image area.
                        // Set Background = false to draw the logo over the page content.
                        Background = false
                    };

                    // Add the stamp to the page. This overlays the logo on the original image.
                    page.AddStamp(logoStamp);
                }
            }

            // Save the modified PDF.
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Images replaced with logo and saved to '{outputPdfPath}'.");
    }
}
