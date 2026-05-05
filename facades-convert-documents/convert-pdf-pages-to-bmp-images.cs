using System;
using System.IO;
using Aspose.Pdf.Facades;
using System.Drawing.Imaging; // ImageFormat for BMP

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputFolder = "BmpPages";

        // Verify source file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Source PDF not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Initialize the PDF‑to‑image converter (facade)
        PdfConverter converter = new PdfConverter();
        converter.BindPdf(inputPdf);          // load PDF
        converter.StartPage = 3;              // first page to convert (1‑based)
        converter.EndPage   = 8;              // last page to convert
        converter.DoConvert();                // prepare conversion

        int currentPage = converter.StartPage;
        while (converter.HasNextImage())
        {
            // Build output file name for each page
            string outPath = Path.Combine(outputFolder, $"page_{currentPage}.bmp");

            // Save the current page as BMP
            converter.GetNextImage(outPath, ImageFormat.Bmp);

            currentPage++;
        }

        // Release resources held by the converter
        converter.Close();
    }
}