using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices; // required for Resolution

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputTiff = "output.tiff";

        // Verify input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // PdfConverter implements IDisposable, so wrap it in a using block
        using (PdfConverter converter = new PdfConverter())
        {
            // Set high‑quality resolution (300 DPI) using a Resolution object
            converter.Resolution = new Resolution(300);

            // Bind the source PDF file
            converter.BindPdf(inputPdf);

            // Prepare the converter (required before saving)
            converter.DoConvert();

            // Save all pages as a single multi‑page TIFF file.
            // Default CoordinateType (CropBox) is used automatically.
            converter.SaveAsTIFF(outputTiff);
        }

        Console.WriteLine($"TIFF image saved to '{outputTiff}'.");
    }
}
