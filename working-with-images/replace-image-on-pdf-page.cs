using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // ImagePlacementAbsorber resides in this namespace

class ReplaceImageExample
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";          // source PDF
        const string outputPdfPath = "output.pdf";         // result PDF
        const string newImagePath  = "newImage.jpg";       // replacement image
        const int    targetPage    = 1;                    // page number (1‑based)

        // Ensure files exist
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

        // Load the PDF document (lifecycle rule: use using for deterministic disposal)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Create an absorber to locate image placements on the specified page
            ImagePlacementAbsorber absorber = new ImagePlacementAbsorber();

            // Accept the absorber for the target page
            pdfDoc.Pages[targetPage].Accept(absorber);

            // Iterate over each found image placement and replace it
            foreach (ImagePlacement placement in absorber.ImagePlacements)
            {
                // Open the new image as a stream
                using (FileStream imgStream = File.OpenRead(newImagePath))
                {
                    // Replace the existing image with the new one (method defined on ImagePlacement)
                    placement.Replace(imgStream);
                }
            }

            // Save the modified PDF (lifecycle rule: use Document.Save)
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Image replacement completed. Saved to '{outputPdfPath}'.");
    }
}