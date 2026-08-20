using System;
using System.IO;
using Aspose.Pdf.Facades;   // PdfConverter resides here

class Program
{
    static void Main()
    {
        // Input PDF file
        const string inputPdf = "input.pdf";

        // Directory where JPEG images will be saved
        const string outputDir = "Images";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // PdfConverter implements IDisposable – wrap it in a using block
        using (PdfConverter converter = new PdfConverter())
        {
            // Bind the PDF file to the converter
            converter.BindPdf(inputPdf);

            // Perform any required initialization before extracting images
            converter.DoConvert();

            int pageNumber = 1;

            // Iterate over all pages; GetNextImage preserves original page size
            while (converter.HasNextImage())
            {
                string outputPath = Path.Combine(outputDir, $"page_{pageNumber}.jpg");

                // Save the current page as a JPEG image.
                // This overload uses the default image format (JPEG) and the original page dimensions.
                converter.GetNextImage(outputPath);

                pageNumber++;
            }
        }

        Console.WriteLine("PDF has been converted to JPEG images successfully.");
    }
}