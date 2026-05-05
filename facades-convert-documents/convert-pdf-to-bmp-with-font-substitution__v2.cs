using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Input PDF file
        const string inputPdf = "input.pdf";

        // Directory where BMP images will be saved
        const string outputDir = "BmpOutput";

        // Verify input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // Global font substitution: replace missing fonts with a known font (e.g., Arial)
        // Add more substitutions as required for your documents
        FontRepository.Substitutions.Add(new SimpleFontSubstitution("Helvetica", "Arial"));
        FontRepository.Substitutions.Add(new SimpleFontSubstitution("TimesNewRomanPSMT", "Arial"));

        // Load the PDF document inside a using block for proper disposal
        using (Document pdfDocument = new Document(inputPdf))
        {
            // Create a resolution for the output images (300 DPI is a common choice)
            Resolution resolution = new Resolution(300);

            // Initialize the BMP device with the desired resolution
            BmpDevice bmpDevice = new BmpDevice(resolution);

            // Set rendering options – specify a default fallback font name
            bmpDevice.RenderingOptions = new RenderingOptions
            {
                DefaultFontName = "Arial"
            };

            // Iterate through all pages (Aspose.Pdf uses 1‑based indexing)
            for (int pageNumber = 1; pageNumber <= pdfDocument.Pages.Count; pageNumber++)
            {
                // Build the output file name for each page
                string outputPath = Path.Combine(outputDir, $"page_{pageNumber}.bmp");

                // Convert the current page to BMP and write it to a file stream
                using (FileStream bmpStream = new FileStream(outputPath, FileMode.Create))
                {
                    bmpDevice.Process(pdfDocument.Pages[pageNumber], bmpStream);
                }
            }
        }

        Console.WriteLine("PDF successfully converted to BMP images.");
    }
}