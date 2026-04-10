using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices; // Added for Resolution class

class PdfToJpegConverter
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdf = "input.pdf";

        // Output directory for JPEG images
        const string outputDir = "Images";

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Verify the input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF document and set up the converter
        using (Document pdfDoc = new Document(inputPdf))
        using (PdfConverter converter = new PdfConverter())
        {
            // Bind the PDF document to the converter
            converter.BindPdf(pdfDoc);

            // Set the resolution to 200 DPI (Resolution class is in Aspose.Pdf.Devices)
            converter.Resolution = new Resolution(200);

            // Limit conversion to the first five pages (1‑based indexing)
            converter.StartPage = 1;
            converter.EndPage   = Math.Min(5, pdfDoc.Pages.Count);

            // Prepare the converter (initial work before extracting images)
            converter.DoConvert();

            int pageIndex = 1;
            // Iterate through the available images/pages
            while (converter.HasNextImage())
            {
                // Build the output file name (e.g., Images/page1.jpg)
                string outputPath = Path.Combine(outputDir, $"page{pageIndex}.jpg");

                // Save the current page as a JPEG image
                converter.GetNextImage(outputPath);

                pageIndex++;
            }
        }

        Console.WriteLine("Conversion completed.");
    }
}
