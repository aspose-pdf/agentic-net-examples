using System;
using System.IO;
using Aspose.Pdf; // Core PDF classes and ImagePlacementAbsorber

class ExtractImages
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";          // Source PDF
        const string outputFolder = "ExtractedImages";    // Folder to store extracted images

        // Verify input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load the PDF document (lifecycle rule: use Document constructor)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Iterate through all pages (Aspose.Pdf uses 1‑based indexing)
            for (int pageNum = 1; pageNum <= pdfDoc.Pages.Count; pageNum++)
            {
                // Create an absorber that will collect image placements on the current page
                var absorber = new ImagePlacementAbsorber();

                // Run the absorber on the page
                pdfDoc.Pages[pageNum].Accept(absorber);

                // Export each found image to a separate file
                int imageIndex = 1;
                foreach (ImagePlacement imgPlacement in absorber.ImagePlacements)
                {
                    // Build a unique file name per page and image
                    string imageFileName = $"Page_{pageNum}_Image_{imageIndex}.png"; // extension can be changed based on original format
                    string imagePath = Path.Combine(outputFolder, imageFileName);

                    // Save the image using its native bytes (cross‑platform, no System.Drawing dependency)
                    using (FileStream outStream = new FileStream(imagePath, FileMode.Create, FileAccess.Write))
                    {
                        imgPlacement.Image.Save(outStream);
                    }

                    Console.WriteLine($"Extracted: {imagePath}");
                    imageIndex++;
                }
            }
        }

        // The original PDF remains unchanged; only images have been extracted.
        Console.WriteLine("Image extraction completed.");
    }
}
