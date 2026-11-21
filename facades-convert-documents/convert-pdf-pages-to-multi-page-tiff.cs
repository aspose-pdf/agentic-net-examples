using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputTiff = "output.tiff";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        try
        {
            // Use PdfConverter with the modern workflow: BindPdf -> set page range -> optional resolution -> DoConvert -> SaveAsTIFF
            using (PdfConverter converter = new PdfConverter())
            {
                converter.BindPdf(inputPdf);
                converter.StartPage = 4;   // first page to convert (1‑based)
                converter.EndPage   = 9;   // last page to convert
                // Define the resolution of the rasterized images (dpi)
                converter.Resolution = new Resolution(150);

                // Prepare the conversion
                converter.DoConvert();

                // Save the selected pages as a multipage TIFF file
                converter.SaveAsTIFF(outputTiff);
            }

            Console.WriteLine($"Pages 4‑9 have been saved to TIFF file: {outputTiff}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
