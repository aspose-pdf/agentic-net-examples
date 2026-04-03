using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputDir = "Thumbnails";

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the PDF document (wrapped in using for proper disposal)
        using (Document pdfDoc = new Document(inputPath))
        {
            // Create a ThumbnailDevice with the required dimensions (150x200 pixels)
            Aspose.Pdf.Devices.ThumbnailDevice thumbDevice = new Aspose.Pdf.Devices.ThumbnailDevice(150, 200);

            // Iterate through all pages (Aspose.Pdf uses 1‑based indexing)
            for (int pageNumber = 1; pageNumber <= pdfDoc.Pages.Count; pageNumber++)
            {
                // Build the output file path for the current page thumbnail
                string outputPath = Path.Combine(outputDir, $"page_{pageNumber}.png");

                // Create a file stream for the PNG output and process the page
                using (FileStream outStream = new FileStream(outputPath, FileMode.Create))
                {
                    // The ThumbnailDevice writes a PNG image to the provided stream
                    thumbDevice.Process(pdfDoc.Pages[pageNumber], outStream);
                }

                Console.WriteLine($"Thumbnail saved: {outputPath}");
            }
        }
    }
}