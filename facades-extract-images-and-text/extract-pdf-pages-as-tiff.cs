using System;
using System.IO;
using Aspose.Pdf;                     // Document class for page count
using Aspose.Pdf.Facades;             // PdfConverter class
using Aspose.Pdf.Devices;             // Resolution class for image conversion

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputDir = "ExtractedImages";

        // Verify source PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the PDF to obtain the total number of pages
        Document pdfDocument = new Document(inputPdf);
        int pageCount = pdfDocument.Pages.Count;

        // Bind the converter once (avoids re‑reading the file for each page)
        using (PdfConverter converter = new PdfConverter())
        {
            converter.BindPdf(pdfDocument);

            // Convert each page to a separate TIFF image (lossless archival)
            for (int pageNumber = 1; pageNumber <= pageCount; pageNumber++)
            {
                // Convert only the current page
                converter.StartPage = pageNumber;
                converter.EndPage   = pageNumber;

                // High‑resolution for archival quality (use Aspose.Pdf.Devices.Resolution)
                converter.Resolution = new Resolution(300);

                // Perform the conversion
                converter.DoConvert();

                // Build the output file name (TIFF format)
                string outPath = Path.Combine(outputDir, $"image-{pageNumber}.tiff");

                // Save the page as a TIFF image
                converter.SaveAsTIFF(outPath);
            }
        }

        Console.WriteLine("Page conversion to TIFF completed.");
    }
}
