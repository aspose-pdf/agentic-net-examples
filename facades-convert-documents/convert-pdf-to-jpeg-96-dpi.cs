using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices; // Added for Resolution class

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // source PDF
        const string outputDir = "Images";             // folder for JPEGs

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        try
        {
            // PdfConverter is a Facade class; it implements IDisposable.
            using (PdfConverter converter = new PdfConverter())
            {
                // Bind the PDF file to the converter.
                converter.BindPdf(inputPdf);

                // Set desired resolution (96 DPI) for web‑friendly images.
                converter.Resolution = new Resolution(96);

                // Prepare the converter for image extraction.
                converter.DoConvert();

                int pageIndex = 1;
                // Iterate over all pages; HasNextImage indicates remaining pages.
                while (converter.HasNextImage())
                {
                    string outputPath = Path.Combine(outputDir, $"page_{pageIndex}.jpg");
                    // GetNextImage(string) saves the current page as JPEG by default.
                    converter.GetNextImage(outputPath);
                    pageIndex++;
                }
            }

            Console.WriteLine($"PDF pages have been converted to JPEG images in '{outputDir}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}
