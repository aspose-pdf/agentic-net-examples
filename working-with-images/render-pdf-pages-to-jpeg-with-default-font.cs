using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices; // JpegDevice resides here

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputDir = "output_images";
        const string defaultFont = "Arial"; // Font used when glyphs are missing

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the PDF document (wrapped in using for deterministic disposal)
        using (Document pdfDocument = new Document(inputPath))
        {
            // Define the resolution for the JPEG images (e.g., 300 DPI)
            Resolution resolution = new Resolution(300);

            // Initialize the JPEG device with the chosen resolution
            JpegDevice jpegDevice = new JpegDevice(resolution);

            // Set the default font name for glyph substitution during rendering
            jpegDevice.RenderingOptions.DefaultFontName = defaultFont;

            // Iterate over all pages (Aspose.Pdf uses 1‑based indexing)
            for (int pageNumber = 1; pageNumber <= pdfDocument.Pages.Count; pageNumber++)
            {
                string outputPath = Path.Combine(outputDir, $"page_{pageNumber}.jpeg");

                // Create a file stream for the output JPEG image
                using (FileStream imageStream = new FileStream(outputPath, FileMode.Create))
                {
                    // Render the current page to JPEG and write it to the stream
                    jpegDevice.Process(pdfDocument.Pages[pageNumber], imageStream);
                }

                Console.WriteLine($"Saved page {pageNumber} as JPEG to '{outputPath}'.");
            }
        }
    }
}