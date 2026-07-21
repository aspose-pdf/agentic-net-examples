using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using System.Drawing.Imaging;

class Program
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

        // Load the PDF document (wrapped in using for deterministic disposal)
        using (Document doc = new Document(inputPdf))
        {
            int pageCount = doc.Pages.Count; // 1‑based page count

            // Process pages from last to first
            for (int pageIndex = pageCount; pageIndex >= 1; pageIndex--)
            {
                // Create a PdfConverter for the current page
                using (PdfConverter converter = new PdfConverter())
                {
                    // Bind the already loaded document
                    converter.BindPdf(doc);

                    // Restrict conversion to a single page (the current one)
                    converter.StartPage = pageIndex;
                    converter.EndPage   = pageIndex;

                    // Prepare the converter
                    converter.DoConvert();

                    // Build output file name (e.g., page_5.png)
                    string outputPath = Path.Combine(outputDir, $"page_{pageIndex}.png");

                    // Save the page as PNG
                    converter.GetNextImage(outputPath, ImageFormat.Png);
                }
            }
        }

        Console.WriteLine("PDF pages have been converted to PNG images in reverse order.");
    }
}