using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // ---------------------------------------------------------------------
        // 1. Create a sample input PDF that contains an image (so we have something
        //    for ImagePlacementAbsorber to find).
        // ---------------------------------------------------------------------
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";

        // Minimal 1x1 red PNG (will be the original image in the source PDF)
        byte[] originalImageBytes = Convert.FromBase64String(
            "iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAIAAACQd1PeAAAADUlEQVR42mP8z8BQDwAF/AL+XK5ZVQAAAABJRU5ErkJggg==");

        // Create the source PDF only if it does not already exist (makes the demo idempotent)
        if (!File.Exists(inputPdf))
        {
            using (Document seed = new Document())
            {
                Page page = seed.Pages.Add();
                // Add the original image to the page
                // Keep the stream alive for the lifetime of the document – do NOT dispose it here.
                MemoryStream imgStream = new MemoryStream(originalImageBytes);
                Image img = new Image { ImageStream = imgStream };
                // Position the image (coordinates are in points; 0,0 is bottom‑left)
                img.FixHeight = 100; // 100 points high
                img.FixWidth = 100;  // 100 points wide
                page.Paragraphs.Add(img);
                seed.Save(inputPdf);
                // The stream can be disposed after the document is saved because the PDF now contains the image data.
                imgStream.Dispose();
            }
        }

        // ---------------------------------------------------------------------
        // 2. Prepare the new logo that will replace every image found on the page.
        //    This is also created in‑memory – no external file is required.
        // ---------------------------------------------------------------------
        // Minimal 1x1 blue PNG (the replacement logo)
        byte[] logoBytes = Convert.FromBase64String(
            "iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAIAAACQd1PeAAAADUlEQVR42mP8z/CfAQADhgJ/6V6ZVQAAAABJRU5ErkJggg==");

        // ---------------------------------------------------------------------
        // 3. Load the PDF, locate all image placements and replace them.
        // ---------------------------------------------------------------------
        using (Document doc = new Document(inputPdf))
        {
            ImagePlacementAbsorber absorber = new ImagePlacementAbsorber();
            absorber.Visit(doc);

            foreach (ImagePlacement placement in absorber.ImagePlacements)
            {
                // Each Replace call consumes the stream, so we provide a fresh one.
                using (MemoryStream logoStream = new MemoryStream(logoBytes))
                {
                    placement.Replace(logoStream);
                }
            }

            doc.Save(outputPdf);
        }

        Console.WriteLine($"Images replaced and saved to '{outputPdf}'.");
    }
}
