using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class PdfToJpegConverter
{
    static void Main()
    {
        // Input PDF file
        const string inputPdfPath = "input.pdf";

        // Output folder for JPEG images
        const string outputFolder = "Images";

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Validate input file existence
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPdfPath}");
            return;
        }

        // Use PdfConverter (Facade) to convert each page to a JPEG image
        using (PdfConverter converter = new PdfConverter())
        {
            // Bind the source PDF file
            converter.BindPdf(inputPdfPath);

            // Prepare the converter (initial work)
            converter.DoConvert();

            int pageIndex = 1;
            // Iterate over all pages; GetNextImage saves the current page as JPEG
            while (converter.HasNextImage())
            {
                string outputFile = Path.Combine(outputFolder, $"page_{pageIndex}.jpg");
                // Saves the current page as JPEG using default settings (preserves page size and color depth)
                converter.GetNextImage(outputFile);
                pageIndex++;
            }
        }

        Console.WriteLine($"Conversion completed. JPEG images saved to '{outputFolder}'.");
    }
}