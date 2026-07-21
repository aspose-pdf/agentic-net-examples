using System;
using System.IO;
using Aspose.Pdf;                     // Core API for accessing page count
using Aspose.Pdf.Facades;            // Facade API for PDF‑to‑image conversion
using System.Drawing.Imaging;        // ImageFormat enum for PNG output

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // Source PDF file
        const string outputDir = "OddPageImages";      // Folder for PNG files

        // Verify the input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Use Document (wrapped in using) to obtain the total page count
        using (Document doc = new Document(inputPdf))
        {
            int totalPages = doc.Pages.Count;   // 1‑based indexing

            // Iterate over odd‑numbered pages only
            for (int pageNumber = 1; pageNumber <= totalPages; pageNumber += 2)
            {
                // Each conversion uses a fresh PdfConverter instance (IDisposable)
                using (PdfConverter converter = new PdfConverter())
                {
                    // Bind the source PDF file
                    converter.BindPdf(inputPdf);

                    // Restrict conversion to the current odd page
                    converter.StartPage = pageNumber;
                    converter.EndPage   = pageNumber;

                    // Prepare the converter
                    converter.DoConvert();

                    // Build the output file name (e.g., page_1.png, page_3.png, …)
                    string outputPath = Path.Combine(outputDir, $"page_{pageNumber}.png");

                    // Save the page as PNG
                    converter.GetNextImage(outputPath, ImageFormat.Png);
                }
            }
        }

        Console.WriteLine("Odd‑page PNG conversion completed.");
    }
}