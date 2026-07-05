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
        const string outputPath = "output.tiff";

        // Verify the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // PdfConverter is a Facade that handles PDF‑to‑image conversion
        using (PdfConverter converter = new PdfConverter())
        {
            // Bind the PDF file to the converter
            converter.BindPdf(inputPath);

            // Set the resolution to 300 dpi for high‑quality output
            // PdfConverter.Resolution expects a Resolution object, not an int
            converter.Resolution = new Resolution(300);

            // Prepare the converter (required before saving)
            converter.DoConvert();

            // Save all pages as a single multi‑page TIFF file
            converter.SaveAsTIFF(outputPath);
        }

        Console.WriteLine($"TIFF image saved to '{outputPath}'.");
    }
}
