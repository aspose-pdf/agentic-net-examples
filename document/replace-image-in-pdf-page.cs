using System;
using System.IO;
using Aspose.Pdf;

class ReplaceImageExample
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";      // source PDF
        const string newImagePath  = "newImage.jpg";   // replacement image (JPEG)
        const string outputPdfPath = "output.pdf";     // result PDF

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

        // Load the PDF document (using block ensures proper disposal)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Access the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = pdfDoc.Pages[1];

            // Find all image placements on the page
            ImagePlacementAbsorber imgAbsorber = new ImagePlacementAbsorber();
            page.Accept(imgAbsorber);

            // If there is at least one image, replace the first one
            if (imgAbsorber.ImagePlacements.Count > 0)
            {
                ImagePlacement placement = imgAbsorber.ImagePlacements[0];

                // Replace the image data while keeping its rectangle, rotation, etc.
                using (FileStream newImgStream = File.OpenRead(newImagePath))
                {
                    placement.Replace(newImgStream);
                }
            }
            else
            {
                Console.WriteLine("No images found on the first page.");
            }

            // Save the modified PDF (no SaveOptions needed for PDF output)
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Image replaced and saved to '{outputPdfPath}'.");
    }
}