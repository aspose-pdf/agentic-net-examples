using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices; // Resolution struct

class Program
{
    static void Main()
    {
        const string pdfPath   = "input.pdf";
        const string outputDir = "BmpPages";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        // PdfConverter implements IDisposable, so wrap it in a using block
        using (PdfConverter converter = new PdfConverter())
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(pdfPath))
            {
                // Bind the document to the converter
                converter.BindPdf(doc);

                // Determine how many pages we will convert (max 20)
                int maxPage = Math.Min(20, doc.Pages.Count);

                // Configure the conversion range and resolution
                converter.StartPage = 1;
                converter.EndPage   = maxPage;
                converter.Resolution = new Resolution(150); // 150 DPI

                // Perform the conversion; after this call GetNextImage() will return each page image
                converter.DoConvert();

                // Retrieve each page image as BMP (format inferred from file extension)
                for (int pageNum = 1; pageNum <= maxPage; pageNum++)
                {
                    string outPath = Path.Combine(outputDir, $"Page_{pageNum}.bmp");
                    converter.GetNextImage(outPath); // BMP format because of .bmp extension
                }
            }
        }

        Console.WriteLine("PDF pages 1‑20 have been converted to BMP images.");
    }
}
