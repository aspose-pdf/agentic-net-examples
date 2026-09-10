using System;
using System.IO;
using Aspose.Pdf.Facades;   // Facade API for PDF conversion
using Aspose.Pdf;          // Needed for ImageFormat enum (if used)

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // Source PDF file
        const string outputDir = "output_images";      // Folder for JPEG files

        // Verify that the source PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // PdfConverter is a Facade that extracts each page as an image.
        // It implements IDisposable, so wrap it in a using block.
        using (PdfConverter converter = new PdfConverter())
        {
            // Bind the PDF file to the converter.
            converter.BindPdf(inputPdf);

            // Prepare the converter for image extraction.
            converter.DoConvert();

            int pageNumber = 1;
            // Iterate over all pages; GetNextImage saves each page as JPEG by default.
            while (converter.HasNextImage())
            {
                string outputPath = Path.Combine(outputDir, $"page_{pageNumber}.jpg");
                converter.GetNextImage(outputPath);   // Default format = JPEG
                pageNumber++;
            }
        }

        Console.WriteLine($"Conversion complete. JPEG images saved to '{outputDir}'.");
    }
}