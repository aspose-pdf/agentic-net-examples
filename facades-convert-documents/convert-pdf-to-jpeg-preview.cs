using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using System.Drawing.Imaging;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdf = "input.pdf";

        // Directory where JPEG preview images will be saved
        const string outputDir = "output_images";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the PDF document (lifecycle rule: using block)
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Initialize the PDF converter with the loaded document
            using (PdfConverter converter = new PdfConverter(pdfDoc))
            {
                // Set the page range for the preview (pages 1 to 3)
                converter.StartPage = 1;
                converter.EndPage   = 3;

                // Prepare the converter for image extraction
                converter.DoConvert();

                // Counter to name the output files sequentially
                int pageNumber = converter.StartPage;

                // Extract each page as a JPEG image
                while (converter.HasNextImage())
                {
                    string outPath = Path.Combine(outputDir, $"page_{pageNumber}.jpg");
                    converter.GetNextImage(outPath, ImageFormat.Jpeg);
                    pageNumber++;
                }
            }
        }

        Console.WriteLine("JPEG preview images saved successfully.");
    }
}