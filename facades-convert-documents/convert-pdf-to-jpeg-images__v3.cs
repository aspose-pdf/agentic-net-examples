using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFolder = "output_images";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load the PDF document (wrapped in using for proper disposal)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Initialize PdfConverter with the loaded document
            using (PdfConverter converter = new PdfConverter(pdfDoc))
            {
                // Prepare the converter
                converter.DoConvert();

                int pageNumber = 1;
                // Iterate through each page image
                while (converter.HasNextImage())
                {
                    string outputPath = Path.Combine(outputFolder, $"page_{pageNumber}.jpg");
                    // Save the current page as JPEG
                    converter.GetNextImage(outputPath);
                    pageNumber++;
                }
            }
        }

        Console.WriteLine("PDF has been converted to JPEG images.");
    }
}