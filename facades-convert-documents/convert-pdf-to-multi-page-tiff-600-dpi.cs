using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputTiffPath = "output.tiff";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        try
        {
            using (PdfConverter converter = new PdfConverter())
            {
                // Load the PDF document.
                converter.BindPdf(inputPdfPath);

                // Set the desired resolution (600 DPI) using a Resolution object.
                converter.Resolution = new Resolution(600);

                // Convert the entire document.
                converter.DoConvert();

                // Save as a multi‑page TIFF.
                converter.SaveAsTIFF(outputTiffPath);
            }

            Console.WriteLine($"PDF successfully converted to TIFF at 600 DPI: {outputTiffPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}
