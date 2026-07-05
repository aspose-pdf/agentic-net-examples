using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;
using Aspose.Pdf.Text; // Added for TextFragment

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdf = "input.pdf";

        // Output directory for JPEG images
        const string outputDir = "JpegPages";

        // Default font to use when a glyph is missing
        const string defaultFont = "Arial";

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // If the input PDF does not exist, create a minimal sample PDF so the example can run without external files.
        if (!File.Exists(inputPdf))
        {
            CreateSamplePdf(inputPdf);
            Console.WriteLine($"Sample PDF created at '{inputPdf}'.");
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document pdfDocument = new Document(inputPdf))
        {
            // Create a JpegDevice with desired resolution (e.g., 300 DPI)
            Resolution resolution = new Resolution(300);
            JpegDevice jpegDevice = new JpegDevice(resolution);

            // Configure rendering options to substitute missing glyphs with the default font
            jpegDevice.RenderingOptions.DefaultFontName = defaultFont;
            // Optional: enable font analysis to improve substitution accuracy
            jpegDevice.RenderingOptions.AnalyzeFonts = true;

            // Iterate over all pages (1‑based indexing)
            for (int pageNumber = 1; pageNumber <= pdfDocument.Pages.Count; pageNumber++)
            {
                // Build the output file name for the current page
                string outputPath = Path.Combine(outputDir, $"page_{pageNumber}.jpeg");

                // Convert the page to JPEG and write to the file stream
                using (FileStream jpegStream = new FileStream(outputPath, FileMode.Create))
                {
                    jpegDevice.Process(pdfDocument.Pages[pageNumber], jpegStream);
                }

                Console.WriteLine($"Page {pageNumber} saved as JPEG: {outputPath}");
            }
        }
    }

    /// <summary>
    /// Creates a very small PDF containing a single page with some sample text.
    /// This method is used only when the expected input file is missing, allowing the
    /// example to run without external resources.
    /// </summary>
    /// <param name="path">The file path where the PDF will be saved.</param>
    private static void CreateSamplePdf(string path)
    {
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();
            // Add a simple paragraph with a few characters that may require fallback fonts.
            page.Paragraphs.Add(new TextFragment("Sample text with Unicode: 測試, тест, اختبار"));
            doc.Save(path);
        }
    }
}
