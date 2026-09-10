using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // ImagePlacementAbsorber resides in this namespace

class ReplaceImageExample
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";          // PDF containing the image to replace
        const string outputPdfPath = "output.pdf";         // Resulting PDF
        const string newImagePath  = "newImage.jpg";       // Image that will replace the existing one
        const int    targetPage    = 1;                    // Page number (1‑based) where the image resides

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(newImagePath))
        {
            Console.Error.WriteLine($"Replacement image not found: {newImagePath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Locate all image placements on the specified page
            ImagePlacementAbsorber imgAbsorber = new ImagePlacementAbsorber();
            pdfDoc.Pages[targetPage].Accept(imgAbsorber);

            // Replace each found image with the new image stream
            foreach (ImagePlacement placement in imgAbsorber.ImagePlacements)
            {
                using (FileStream newImgStream = File.OpenRead(newImagePath))
                {
                    placement.Replace(newImgStream);
                }
            }

            // Save the modified PDF
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Image replacement completed. Saved to '{outputPdfPath}'.");
    }
}