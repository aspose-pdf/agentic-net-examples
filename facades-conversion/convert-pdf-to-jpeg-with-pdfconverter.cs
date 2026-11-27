using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices;
using System.Drawing.Imaging;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFolder = "JpegPages";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load the PDF document
        Document pdfDoc = new Document(inputPdfPath);

        // Configure form presentation mode before setting page range (if needed)
        // Uncomment and set the desired mode when using a version that supports it.
        // pdfDoc.FormPresentationMode = FormPresentationMode.FormFields;

        // Initialize the PDF converter (facade API)
        using (PdfConverter converter = new PdfConverter())
        {
            // Bind the PDF document to the converter
            converter.BindPdf(pdfDoc);

            // Define the page range to convert – must be set BEFORE DoConvert()
            converter.StartPage = 1;
            converter.EndPage   = pdfDoc.Pages.Count;

            // Set the resolution – higher values give better quality JPEGs
            converter.Resolution = new Resolution(150);

            // Prepare the conversion (required before extracting images)
            converter.DoConvert();

            // Extract each page as a JPEG image using the HasNextImage/GetNextImage pattern
            int pageNumber = converter.StartPage;
            while (converter.HasNextImage())
            {
                string outputPath = Path.Combine(outputFolder, $"page_{pageNumber}.jpg");
                // GetNextImage writes the current page to the specified path using the chosen ImageFormat
                converter.GetNextImage(outputPath, ImageFormat.Jpeg);
                Console.WriteLine($"Saved page {pageNumber} as JPEG to '{outputPath}'.");
                pageNumber++;
            }

            // Release resources held by the converter
            converter.Close();
        }

        Console.WriteLine("PDF to JPEG conversion completed.");
    }
}
