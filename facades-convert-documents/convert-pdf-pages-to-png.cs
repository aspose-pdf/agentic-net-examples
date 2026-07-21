using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using System.Drawing.Imaging;

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

        // Prefix and suffix for sequential file names
        string prefix = Path.Combine(outputFolder, "page_");
        const string suffix = ".png";

        // Load the PDF document inside a using block for proper disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Initialize the PdfConverter facade
            using (PdfConverter converter = new PdfConverter())
            {
                // Bind the loaded document to the converter
                converter.BindPdf(pdfDoc);
                // Prepare the converter for image extraction
                converter.DoConvert();

                int pageNumber = 1;
                // Iterate through all pages and save each as a PNG file
                while (converter.HasNextImage())
                {
                    string outputPath = $"{prefix}{pageNumber}{suffix}";
                    converter.GetNextImage(outputPath, ImageFormat.Png);
                    pageNumber++;
                }
            }
        }

        Console.WriteLine("PDF pages have been successfully converted to PNG images.");
    }
}