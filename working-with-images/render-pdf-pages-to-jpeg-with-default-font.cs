using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class RenderPdfToJpeg
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdfPath = "input.pdf";

        // Output directory for JPEG images
        const string outputDir = "JpegPages";

        // Default font name to use when a glyph is missing
        const string defaultFontName = "Arial";

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // If the input PDF does not exist, create a simple placeholder PDF so the program can continue.
        if (!File.Exists(inputPdfPath))
        {
            Console.WriteLine($"Input file '{inputPdfPath}' not found. Creating a placeholder PDF.");
            using (Document placeholder = new Document())
            {
                // Add a single blank page (or you could add some sample text).
                placeholder.Pages.Add();
                placeholder.Save(inputPdfPath);
            }
        }

        // Load the PDF document
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Create a resolution (e.g., 300 DPI) for high‑quality images
            Resolution resolution = new Resolution(300);

            // Initialize the JPEG device with the desired resolution
            JpegDevice jpegDevice = new JpegDevice(resolution);

            // Configure rendering options to substitute missing glyphs with the default font
            jpegDevice.RenderingOptions.DefaultFontName = defaultFontName;
            // Optionally enable font analysis to improve substitution
            jpegDevice.RenderingOptions.AnalyzeFonts = true;

            // Iterate over all pages (1‑based indexing)
            for (int pageNumber = 1; pageNumber <= pdfDocument.Pages.Count; pageNumber++)
            {
                // Build the output file name for the current page
                string outputFile = Path.Combine(outputDir, $"page_{pageNumber}.jpeg");

                // Convert the page to JPEG and write to the file stream
                using (FileStream jpegStream = new FileStream(outputFile, FileMode.Create))
                {
                    jpegDevice.Process(pdfDocument.Pages[pageNumber], jpegStream);
                }

                Console.WriteLine($"Page {pageNumber} saved as {outputFile}");
            }
        }
    }
}
