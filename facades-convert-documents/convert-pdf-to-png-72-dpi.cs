using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices;

class PdfToPngConverter
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputDir = "PngPages";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        using (Document pdfDoc = new Document(inputPdfPath))
        using (PdfConverter converter = new PdfConverter())
        {
            // Bind the PDF document to the converter
            converter.BindPdf(pdfDoc);

            // Set the desired resolution (72 DPI for quick preview)
            converter.Resolution = new Resolution(72);

            // Prepare the converter for image extraction
            converter.DoConvert();

            int pageIndex = 1;
            // Iterate over all pages and save each as a PNG image
            while (converter.HasNextImage())
            {
                string outputPath = Path.Combine(outputDir, $"page_{pageIndex}.png");
                // The overload infers the image format from the file extension, avoiding System.Drawing dependencies
                converter.GetNextImage(outputPath);
                pageIndex++;
            }
        }

        Console.WriteLine("PDF to PNG conversion completed.");
    }
}
