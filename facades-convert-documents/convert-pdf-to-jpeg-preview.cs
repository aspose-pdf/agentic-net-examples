using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using System.Drawing.Imaging;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "output_images";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the PDF document inside a using block for proper disposal
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Initialize the PdfConverter facade with the loaded document
            using (PdfConverter converter = new PdfConverter(pdfDoc))
            {
                // Set the page range for conversion (pages 1 to 3)
                converter.StartPage = 1;
                converter.EndPage = 3;

                // Perform initial conversion setup
                converter.DoConvert();

                int pageNumber = 1;
                // Iterate through the generated images
                while (converter.HasNextImage())
                {
                    string outputPath = Path.Combine(outputDir, $"page_{pageNumber}.jpg");
                    // Save the current page as a JPEG image
                    converter.GetNextImage(outputPath, ImageFormat.Jpeg);
                    pageNumber++;
                }
            }
        }

        Console.WriteLine("PDF to JPEG conversion completed.");
    }
}