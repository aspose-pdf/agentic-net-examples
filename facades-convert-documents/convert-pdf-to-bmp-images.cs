using System;
using System.IO;
using System.Drawing.Imaging;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices; // for Resolution

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "BmpImages";

        // Verify source file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Source PDF not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdf))
        // PdfConverter also implements IDisposable, so wrap it as well
        using (PdfConverter converter = new PdfConverter())
        {
            // Bind the document to the converter facade
            converter.BindPdf(pdfDoc);

            // Configure conversion settings BEFORE DoConvert()
            // Resolution expects an Aspose.Pdf.Devices.Resolution object, not an int.
            converter.Resolution = new Resolution(300); // 300 DPI

            // The CoordinateType property was removed in recent versions; the default (CropBox) is used.
            // If a specific coordinate type is required, uncomment the line below and ensure the enum exists in your version.
            // converter.CoordinateType = Aspose.Pdf.CoordinateType.CropBox;

            // Prepare the converter for image extraction
            converter.DoConvert();

            int pageNumber = 1;
            // Extract each page as a BMP image
            while (converter.HasNextImage())
            {
                string bmpPath = Path.Combine(outputDir, $"page_{pageNumber}.bmp");
                converter.GetNextImage(bmpPath, ImageFormat.Bmp);
                pageNumber++;
            }
        }

        Console.WriteLine("PDF successfully converted to BMP images.");
    }
}
