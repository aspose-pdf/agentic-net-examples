using System;
using System.IO;
using System.Drawing.Imaging;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // source PDF
        const string outputDir = "Images";             // folder for PNG files

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the PDF document (lifecycle rule: wrap in using)
        using (Document doc = new Document(inputPdf))
        {
            int pageCount = doc.Pages.Count; // 1‑based page count

            // Create a PdfConverter facade (lifecycle rule: wrap in using)
            using (PdfConverter converter = new PdfConverter())
            {
                // Bind the PDF document to the converter
                converter.BindPdf(doc);

                // Optional: set resolution (default 150 DPI)
                // converter.Resolution = new Resolution(300);

                // Process pages in reverse order
                for (int i = pageCount; i >= 1; i--)
                {
                    // Convert a single page: set start and end to the same page
                    converter.StartPage = i;
                    converter.EndPage   = i;

                    // Initialise conversion for the selected page
                    converter.DoConvert();

                    // Build output file name (e.g., image5.png, image4.png, ...)
                    string outPath = Path.Combine(outputDir, $"image{i}.png");

                    // Save the page as PNG (using System.Drawing.Imaging.ImageFormat.Png)
                    converter.GetNextImage(outPath, ImageFormat.Png);
                }
            }
        }

        Console.WriteLine("PDF pages have been converted to PNG images in reverse order.");
    }
}
