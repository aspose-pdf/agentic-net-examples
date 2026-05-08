using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        // Input PDF file
        const string inputPath = "input.pdf";
        // Directory where JPEG images will be saved
        const string outputDir = "Images";
        // Font to use when a glyph is missing in the original PDF
        const string defaultFont = "Arial";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the PDF document (lifecycle rule: using for disposal)
        using (Document pdfDocument = new Document(inputPath))
        {
            // Create a resolution object (e.g., 300 DPI)
            Resolution resolution = new Resolution(300);

            // Initialize JpegDevice with the specified resolution
            JpegDevice jpegDevice = new JpegDevice(resolution);

            // Set the default font name for missing glyphs
            jpegDevice.RenderingOptions.DefaultFontName = defaultFont;

            // Iterate over all pages (1‑based indexing)
            for (int pageNumber = 1; pageNumber <= pdfDocument.Pages.Count; pageNumber++)
            {
                // Build the output file name for the current page
                string outputPath = Path.Combine(outputDir, $"page_{pageNumber}.jpeg");

                // Save the page as a JPEG image (lifecycle rule: using for stream)
                using (FileStream imageStream = new FileStream(outputPath, FileMode.Create))
                {
                    jpegDevice.Process(pdfDocument.Pages[pageNumber], imageStream);
                }
            }
        }

        Console.WriteLine("All pages have been rendered to JPEG images.");
    }
}