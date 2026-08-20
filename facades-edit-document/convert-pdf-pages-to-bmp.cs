using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class PdfToBmpConverter
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdfPath = "input.pdf";

        // Output directory for BMP images
        const string outputDir = "BmpImages";

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // ---------------------------------------------------------------------
        // Create a minimal PDF if it does not already exist. This makes the
        // example self‑contained and prevents FileNotFoundException in the sandbox.
        // ---------------------------------------------------------------------
        if (!File.Exists(inputPdfPath))
        {
            using (var placeholder = new Document())
            {
                placeholder.Pages.Add(); // add a single blank page
                placeholder.Save(inputPdfPath);
            }
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Define the desired resolution (e.g., 300 DPI) – higher DPI yields larger images
            Resolution resolution = new Resolution(300);

            // Initialise the BMP device with the specified resolution.
            // The constructor that accepts only a Resolution object creates a 24‑bit BMP by default.
            BmpDevice bmpDevice = new BmpDevice(resolution);

            // Iterate over all pages (Aspose.Pdf uses 1‑based indexing)
            for (int pageNumber = 1; pageNumber <= pdfDocument.Pages.Count; pageNumber++)
            {
                // Build the output file name for the current page
                string outputFile = Path.Combine(outputDir, $"page_{pageNumber}.bmp");

                // Create a file stream to write the BMP image
                using (FileStream bmpStream = new FileStream(outputFile, FileMode.Create))
                {
                    // Convert the current page to BMP and write it to the stream
                    bmpDevice.Process(pdfDocument.Pages[pageNumber], bmpStream);
                }

                Console.WriteLine($"Page {pageNumber} saved as BMP: {outputFile}");
            }
        }

        Console.WriteLine("All pages have been converted to BMP images.");
    }
}
