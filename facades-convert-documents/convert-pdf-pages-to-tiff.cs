using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Path to the source PDF file
        const string inputPdf = "input.pdf";

        // Directory where individual TIFF files will be saved
        const string outputDir = "TiffPages";

        // Verify that the input PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the PDF document to obtain the page count
        Document pdfDocument = new Document(inputPdf);
        int totalPages = pdfDocument.Pages.Count;

        // Create a PdfConverter facade instance (re‑used for each page)
        PdfConverter converter = new PdfConverter();
        converter.BindPdf(pdfDocument);
        // No explicit resolution is set – default (96 DPI) is used.

        // Convert each page individually and save it as a separate TIFF file
        for (int page = 1; page <= totalPages; page++)
        {
            // Configure the converter to work on a single page
            converter.StartPage = page;
            converter.EndPage   = page;

            // Perform the conversion for the selected page
            converter.DoConvert();

            // Build the output file name for the current page
            string tiffPath = Path.Combine(outputDir, $"page_{page}.tiff");

            // Save the current page as a TIFF image using default settings
            converter.SaveAsTIFF(tiffPath);
        }

        // Release resources held by the converter
        converter.Close();

        Console.WriteLine($"Conversion complete: {totalPages} TIFF files saved to '{outputDir}'.");
    }
}
