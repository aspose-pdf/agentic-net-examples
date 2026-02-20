using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main(string[] args)
    {
        // Input PDF file path (change as needed)
        const string inputPdfPath = "input.pdf";

        // Output directory where extracted images will be saved
        const string outputDir = "ExtractedImages";

        // Validate input file
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found at '{inputPdfPath}'.");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        try
        {
            // Load the PDF document
            Document pdfDocument = new Document(inputPdfPath);

            // Iterate through all pages (Aspose.Pdf uses 1‑based indexing)
            for (int pageNumber = 1; pageNumber <= pdfDocument.Pages.Count; pageNumber++)
            {
                // Create an absorber that will find image placements on the current page
                var absorber = new ImagePlacementAbsorber();

                // Perform the search on the page
                pdfDocument.Pages[pageNumber].Accept(absorber);

                // Process each found image placement
                int imageIndex = 1;
                foreach (var imagePlacement in absorber.ImagePlacements)
                {
                    // Build a unique file name for each extracted image
                    string imageFileName = $"page_{pageNumber}_img_{imageIndex}.png";
                    string imagePath = Path.Combine(outputDir, imageFileName);

                    // Save the image resource to a memory stream. XImage.Save(Stream) writes the raw image data.
                    using (var memoryStream = new MemoryStream())
                    {
                        imagePlacement.Image.Save(memoryStream);
                        memoryStream.Position = 0; // Reset stream position before writing to file

                        // Write the stream contents to the output file
                        using (var fileStream = new FileStream(imagePath, FileMode.Create, FileAccess.Write))
                        {
                            memoryStream.CopyTo(fileStream);
                        }
                    }

                    Console.WriteLine($"Extracted image saved to: {imagePath}");
                    imageIndex++;
                }
            }

            Console.WriteLine("Image extraction completed successfully.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred during extraction: {ex.Message}");
        }
    }
}