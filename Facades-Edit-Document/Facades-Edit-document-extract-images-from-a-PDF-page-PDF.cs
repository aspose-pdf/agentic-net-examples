using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Expected arguments: <pdfPath> <pageNumber> <outputFolder>
        if (args.Length < 3)
        {
            Console.WriteLine("Usage: <exe> <pdfPath> <pageNumber> <outputFolder>");
            return;
        }

        string pdfPath = args[0];
        if (!File.Exists(pdfPath))
        {
            Console.WriteLine($"Error: PDF file not found: {pdfPath}");
            return;
        }

        if (!int.TryParse(args[1], out int pageNumber) || pageNumber < 1)
        {
            Console.WriteLine("Error: Invalid page number.");
            return;
        }

        string outputFolder = args[2];
        if (!Directory.Exists(outputFolder))
            Directory.CreateDirectory(outputFolder);

        try
        {
            // Load the PDF document
            Document pdfDocument = new Document(pdfPath);

            if (pageNumber > pdfDocument.Pages.Count)
            {
                Console.WriteLine($"Error: Page number {pageNumber} exceeds document page count {pdfDocument.Pages.Count}.");
                return;
            }

            // Absorber to collect image placements from the specified page
            ImagePlacementAbsorber absorber = new ImagePlacementAbsorber();

            // Extract images from the given page (1‑based index)
            pdfDocument.Pages[pageNumber].Accept(absorber);

            int imageIndex = 1;
            foreach (ImagePlacement placement in absorber.ImagePlacements)
            {
                // XImage holds the actual image data
                XImage xImage = placement.Image;

                // Determine a suitable file extension. The ImageInfo property is not available in all
                // Aspose.Pdf versions, so we fall back to a generic ".png" extension which works for most
                // common image types. If a more precise extension is required, additional logic can be
                // added based on the image's raw bytes.
                string extension = GetExtension(xImage);
                string outPath = Path.Combine(outputFolder,
                    $"Page{pageNumber}_Image{imageIndex}{extension}");

                // Save the image stream to a file
                using (FileStream fs = new FileStream(outPath, FileMode.Create, FileAccess.Write))
                {
                    xImage.Save(fs);
                }

                Console.WriteLine($"Extracted image saved to: {outPath}");
                imageIndex++;
            }

            if (imageIndex == 1)
                Console.WriteLine("No images found on the specified page.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error during extraction: {ex.Message}");
        }
    }

    // Helper to map image data to a file extension.
    // The original implementation relied on XImage.ImageInfo.MimeType, which is not present in
    // older Aspose.Pdf builds. To keep the code portable across versions, we simply return a
    // generic ".png" extension. PNG is widely supported and works for JPEG/PNG/GIF/TIFF images when
    // saved via XImage.Save().
    static string GetExtension(XImage img)
    {
        // If the image data is null, return a generic binary extension.
        if (img == null)
            return ".bin";

        // Default to PNG – works for most raster images extracted via Aspose.Pdf.
        return ".png";
    }
}