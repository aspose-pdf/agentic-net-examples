using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        // Path to the source PDF file
        const string pdfPath = "input.pdf";

        // Directory where JPEG images will be saved
        const string outputDir = "Images";

        // Default font to use when a glyph is missing in the original PDF
        const string defaultFontName = "Arial";

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // ---------------------------------------------------------------------
        // Create a minimal PDF file if it does not already exist. This makes the
        // example self‑contained and prevents a FileNotFoundException in the sandbox.
        // ---------------------------------------------------------------------
        if (!File.Exists(pdfPath))
        {
            using (Document seed = new Document())
            {
                // Add a single blank page (you can add any content you like here).
                seed.Pages.Add();
                seed.Save(pdfPath);
            }
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document pdfDocument = new Document(pdfPath))
        {
            // Create a resolution object (e.g., 300 DPI) for high‑quality images
            Resolution resolution = new Resolution(300);

            // Initialize the JPEG device with the desired resolution
            JpegDevice jpegDevice = new JpegDevice(resolution);

            // Configure rendering options to substitute missing glyphs with the specified font
            jpegDevice.RenderingOptions.DefaultFontName = defaultFontName;
            // Optional: enable font analysis to improve substitution accuracy
            jpegDevice.RenderingOptions.AnalyzeFonts = true;

            // Iterate over all pages (1‑based indexing)
            for (int pageNumber = 1; pageNumber <= pdfDocument.Pages.Count; pageNumber++)
            {
                // Build the output file name for the current page
                string outputPath = Path.Combine(outputDir, $"page_{pageNumber}.jpeg");

                // Convert the page to JPEG and write it to a file stream
                using (FileStream jpegStream = new FileStream(outputPath, FileMode.Create))
                {
                    jpegDevice.Process(pdfDocument.Pages[pageNumber], jpegStream);
                }

                Console.WriteLine($"Saved page {pageNumber} as JPEG to '{outputPath}'.");
            }
        }
    }
}
