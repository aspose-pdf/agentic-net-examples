using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF, output PDF and the new image file
        string inputPdfPath = "input.pdf";
        string outputPdfPath = "output.pdf";
        string newImagePath = "newImage.jpg";

        // Page number (1‑based) and image index on that page (1‑based)
        int pageNumber = 1;
        int imageIndex = 1;

        // Verify that required files exist
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

        try
        {
            // Load the PDF document
            Document pdfDocument = new Document(inputPdfPath);

            // Search for image placements on the specified page
            ImagePlacementAbsorber absorber = new ImagePlacementAbsorber();
            pdfDocument.Pages[pageNumber].Accept(absorber);

            // Ensure the requested image exists
            if (imageIndex < 1 || imageIndex > absorber.ImagePlacements.Count)
            {
                Console.Error.WriteLine("Specified image index is out of range.");
                return;
            }

            // Get the target image placement
            ImagePlacement targetPlacement = absorber.ImagePlacements[imageIndex - 1];

            // Replace the image with the new one
            using (FileStream newImageStream = new FileStream(newImagePath, FileMode.Open, FileAccess.Read))
            {
                targetPlacement.Replace(newImageStream);
            }

            // Save the modified PDF (uses the provided document-save rule)
            pdfDocument.Save(outputPdfPath);

            Console.WriteLine($"Image successfully replaced. Output saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during processing: {ex.Message}");
        }
    }
}