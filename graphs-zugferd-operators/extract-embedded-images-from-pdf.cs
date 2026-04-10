using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices; // ImagePlacementAbsorber and ImagePlacement are defined here
using System.Drawing.Imaging; // Needed for ImageFormat (used only for saving images)

// Extract all embedded images from a PDF and save each one preserving its original resolution.
// The images are saved in PNG format (lossless) while keeping the original dimensions.
class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";          // source PDF
        const string outputFolder  = "ExtractedImages";   // folder to store extracted images

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load the PDF document (lifecycle rule: use Document constructor)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Iterate through all pages (Aspose.Pdf uses 1‑based indexing)
            for (int pageNum = 1; pageNum <= pdfDoc.Pages.Count; pageNum++)
            {
                Page page = pdfDoc.Pages[pageNum];

                // Absorb image placements on the current page
                ImagePlacementAbsorber absorber = new ImagePlacementAbsorber();
                page.Accept(absorber);

                int imageIndex = 1;
                foreach (ImagePlacement imgPlacement in absorber.ImagePlacements)
                {
                    // Build a unique file name for each extracted image
                    string outFile = Path.Combine(
                        outputFolder,
                        $"page{pageNum}_img{imageIndex}.png"); // PNG preserves original resolution

                    // Save the image with its original dimensions and resolution
                    using (FileStream outStream = new FileStream(outFile, FileMode.Create))
                    {
                        // ImagePlacement.Save respects scaling, rotation and resolution
                        imgPlacement.Save(outStream, ImageFormat.Png);
                    }

                    Console.WriteLine($"Saved image: {outFile}");
                    imageIndex++;
                }
            }
        }

        Console.WriteLine("Image extraction completed.");
    }
}