using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

namespace PdfConversionFacade
{
    /// <summary>
    /// Facade that hides the complexity of converting a PDF document to BMP images.
    /// </summary>
    public static class PdfToBmpFacade
    {
        /// <summary>
        /// Converts each page of the specified PDF file to a BMP image.
        /// </summary>
        /// <param name="pdfPath">Full path to the source PDF file.</param>
        /// <param name="outputDir">Directory where the BMP files will be written.</param>
        /// <param name="dpi">Resolution in dots‑per‑inch for the generated images. Default is 150.</param>
        public static void Convert(string pdfPath, string outputDir, int dpi = 150)
        {
            // Validate input PDF
            if (!File.Exists(pdfPath))
            {
                Console.WriteLine($"Error: PDF file '{pdfPath}' not found.");
                return;
            }

            // Ensure the output directory exists
            Directory.CreateDirectory(outputDir);

            // Load the PDF document
            Document pdfDocument = new Document(pdfPath);

            // Prepare the resolution object used by BmpDevice
            var resolution = new Resolution(dpi);

            // Iterate through each page and convert it to BMP
            for (int pageNumber = 1; pageNumber <= pdfDocument.Pages.Count; pageNumber++)
            {
                string bmpPath = Path.Combine(outputDir, $"page_{pageNumber}.bmp");

                // BmpDevice does NOT implement IDisposable, so we instantiate it without a using block
                BmpDevice bmpDevice = new BmpDevice(resolution);
                bmpDevice.Process(pdfDocument.Pages[pageNumber], bmpPath);
            }

            Console.WriteLine("PDF pages have been converted to BMP images.");
        }
    }

    class Program
    {
        static void Main()
        {
            const string pdfPath = "input.pdf";          // Input PDF file path
            const string outputDir = "BmpImages";        // Output directory for BMP files

            // Use the facade to perform the conversion
            PdfToBmpFacade.Convert(pdfPath, outputDir);
        }
    }
}
