using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices;               // for Resolution
using System.Drawing.Imaging;          // for ImageFormat

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputDir = "output_images";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the PDF document (lifecycle rule: use using)
        using (Document pdfDoc = new Document(inputPath))
        {
            // Initialize the PdfConverter facade (disposable)
            using (PdfConverter converter = new PdfConverter())
            {
                // Bind the loaded document to the converter
                converter.BindPdf(pdfDoc);

                // Set the desired resolution (200 DPI) – Resolution is a struct, not an int
                converter.Resolution = new Resolution(200);

                // Limit conversion to the first five pages (or fewer if the document has less)
                converter.StartPage = 1;
                converter.EndPage = Math.Min(5, pdfDoc.Pages.Count);

                // Prepare the conversion process
                converter.DoConvert();

                int pageNumber = 1;
                // Iterate through the generated images
                while (converter.HasNextImage())
                {
                    string outputPath = Path.Combine(outputDir, $"page_{pageNumber}.jpg");
                    // Save each page as a JPEG image using System.Drawing.Imaging.ImageFormat
                    converter.GetNextImage(outputPath, ImageFormat.Jpeg);
                    pageNumber++;
                }
            }
        }

        Console.WriteLine("PDF to JPEG conversion completed.");
    }
}
