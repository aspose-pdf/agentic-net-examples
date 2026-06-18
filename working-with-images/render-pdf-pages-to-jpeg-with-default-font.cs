using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";          // source PDF
        const string outputDir  = "Images";            // folder for JPEGs
        const string defaultFont = "Arial";            // fallback font for missing glyphs

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        // Load the PDF document (lifecycle rule: using block for disposal)
        using (Document pdfDocument = new Document(inputPdf))
        {
            // Define image resolution (e.g., 300 DPI)
            Resolution resolution = new Resolution(300);

            // Create a JPEG device with the specified resolution
            JpegDevice jpegDevice = new JpegDevice(resolution);

            // Set rendering options to substitute missing glyphs with the default font
            jpegDevice.RenderingOptions = new RenderingOptions
            {
                DefaultFontName = defaultFont,
                AnalyzeFonts    = true   // enable font analysis/substitution
            };

            // Iterate over all pages (Aspose.Pdf uses 1‑based indexing)
            for (int pageNumber = 1; pageNumber <= pdfDocument.Pages.Count; pageNumber++)
            {
                string jpegPath = Path.Combine(outputDir, $"page_{pageNumber}.jpeg");
                using (FileStream jpegStream = new FileStream(jpegPath, FileMode.Create))
                {
                    // Render the current page to JPEG and write to the stream
                    jpegDevice.Process(pdfDocument.Pages[pageNumber], jpegStream);
                }
                Console.WriteLine($"Page {pageNumber} saved as JPEG to '{jpegPath}'.");
            }
        }
    }
}