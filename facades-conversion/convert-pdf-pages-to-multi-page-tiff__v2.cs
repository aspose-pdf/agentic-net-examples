using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputTiff = "output_page1-2.tiff";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // The FormPresentationMode property is not available in older Aspose.Pdf versions.
            // It can be omitted because the default behavior is suitable for conversion.

            // Create a PdfConverter, bind the document, and set the resolution (300 DPI)
            using (PdfConverter converter = new PdfConverter())
            {
                converter.BindPdf(doc);
                converter.Resolution = new Resolution(300); // 300 DPI

                // Convert pages 1 and 2 to a multi‑page TIFF image
                converter.SaveAsTIFF(outputTiff, 1, 2);
            }
        }

        Console.WriteLine($"Pages 1‑2 saved as TIFF to '{outputTiff}'.");
    }
}
