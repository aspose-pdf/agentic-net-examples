using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using System.Drawing.Imaging; // ImageFormat for PNG

class PdfToPngReverse
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputDir = "PngPages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the PDF document (disposal handled by using)
        using (Document doc = new Document(inputPdf))
        {
            int pageCount = doc.Pages.Count; // 1‑based page count

            // Initialize the converter facade
            PdfConverter converter = new PdfConverter();

            // Bind the PDF file path (alternatively BindPdf(doc))
            converter.BindPdf(inputPdf);

            // Process pages in reverse order
            for (int i = pageCount; i >= 1; i--)
            {
                // Convert a single page at a time
                converter.StartPage = i;
                converter.EndPage   = i;
                converter.DoConvert();

                string outPath = Path.Combine(outputDir, $"page_{i}.png");

                // Save the current page as PNG
                converter.GetNextImage(outPath, ImageFormat.Png);
            }

            // Release resources held by the converter
            converter.Close();
        }

        Console.WriteLine("PDF pages have been converted to PNG in reverse order.");
    }
}