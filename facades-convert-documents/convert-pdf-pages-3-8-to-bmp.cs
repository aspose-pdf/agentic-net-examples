using System;
using System.IO;
using System.Drawing.Imaging;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputFolder = "BmpImages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load the PDF document (wrapped in using for proper disposal)
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Initialize the PdfConverter facade (also disposable)
            using (PdfConverter converter = new PdfConverter())
            {
                // Bind the loaded document to the converter
                converter.BindPdf(pdfDoc);

                // Set the desired page range (pages are 1‑based)
                converter.StartPage = 3;
                converter.EndPage   = 8;

                // Prepare the conversion process
                converter.DoConvert();

                // Export each page in the range as a BMP image
                int pageNumber = converter.StartPage;
                while (converter.HasNextImage())
                {
                    string outputPath = Path.Combine(outputFolder, $"page_{pageNumber}_out.bmp");
                    converter.GetNextImage(outputPath, ImageFormat.Bmp);
                    pageNumber++;
                }
            }
        }

        Console.WriteLine("PDF pages 3‑8 have been converted to BMP images.");
    }
}