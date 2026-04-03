using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // for ImagePlacementAbsorber

class ReplaceImageExample
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";          // existing PDF
        const string newImagePath  = "newImage.jpg";       // image to replace with
        const string outputPdfPath = "output.pdf";         // result PDF

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
            // Create an absorber to locate image placements on the first page
            ImagePlacementAbsorber absorber = new ImagePlacementAbsorber();

            // Accept the absorber for the desired page (1‑based indexing)
            pdfDoc.Pages[1].Accept(absorber);

            // If no images are found, inform the user
            if (absorber.ImagePlacements.Count == 0)
            {
                Console.WriteLine("No images found on page 1.");
            }
            else
            {
                // Replace each found image with the new image stream
                foreach (ImagePlacement placement in absorber.ImagePlacements)
                {
                    // Open the replacement image as a stream (disposed after Replace)
                    using (FileStream imgStream = File.OpenRead(newImagePath))
                    {
                        placement.Replace(imgStream);
                    }
                }

                Console.WriteLine($"Replaced {absorber.ImagePlacements.Count} image(s) on page 1.");
            }

            // Save the modified document
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPdfPath}'.");
    }
}