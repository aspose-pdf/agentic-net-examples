using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf;
using Aspose.Pdf.Devices;          // Resolution type
using System.Drawing.Imaging;    // ImageFormat enum

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputDir = "ConvertedImages";

        // Verify source file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {inputPdfPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // ------------------------------------------------------------
        // Use PdfConverter (a Facade) to convert PDF pages to images.
        // ------------------------------------------------------------
        using (PdfConverter converter = new PdfConverter())
        {
            // Bind the PDF document to the converter
            converter.BindPdf(inputPdfPath);

            // ---- Conversion parameters --------------------------------
            // Page range (1‑based indexing). Convert pages 2 through 5.
            converter.StartPage = 2;
            converter.EndPage   = 5;

            // Image resolution in DPI. Higher DPI yields higher quality images.
            converter.Resolution = new Resolution(300); // <-- correct type

            // Optional: additional rendering options can be set via
            // converter.RenderingOptions if needed (e.g., anti‑aliasing).

            // Prepare the converter for processing
            converter.DoConvert();

            // Iterate over the selected pages and save each as a JPEG.
            // The overload GetNextImage(string, ImageFormat, int) allows
            // specifying JPEG quality (0‑100). Here we use 80 as a good balance.
            int pageIndex = converter.StartPage;
            while (converter.HasNextImage())
            {
                string outputFile = Path.Combine(outputDir, $"Page_{pageIndex}.jpg");
                converter.GetNextImage(outputFile, ImageFormat.Jpeg, 80); // <-- ImageFormat from System.Drawing.Imaging
                pageIndex++;
            }
        }

        Console.WriteLine("PDF conversion completed successfully.");
    }
}
