using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_background.pdf";
        const string imagePath  = "texture.png"; // subtle texture image

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPath}");
            return;
        }

        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Background image not found: {imagePath}");
            return;
        }

        // Load the PDF document (using rule for loading)
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages (Aspose.Pdf uses 1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Create an Image object for the background texture
                Image bgImage = new Image();
                bgImage.File = imagePath;

                // Assign the image as the page background.
                // Note: Aspose.Pdf does not expose a direct blend‑mode property.
                // The background image will be drawn beneath the page content.
                // For an "overlay" effect you may need to pre‑process the image
                // (e.g., adjust its opacity or apply an overlay blend in an image editor)
                // before assigning it here.
                page.BackgroundImage = bgImage;
            }

            // Save the modified PDF (using rule for saving)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with background image: {outputPath}");
    }
}